using CoaxarApp.Models;
using SQLite;

namespace CoaxarApp.Services;

public static class AnimalRepository
{
    private static readonly SemaphoreSlim InitializationLock = new(1, 1);
    private static SQLiteAsyncConnection? _database;
    private static bool _initialized;

    public static async Task InitializeAsync()
    {
        if (_initialized)
            return;

        await InitializationLock.WaitAsync();
        try
        {
            if (_initialized)
                return;

            await CopiarBancoBundledAsync();

            _database = new SQLiteAsyncConnection(
                DatabaseConstants.DatabasePath,
                DatabaseConstants.Flags);

            await _database.CreateTableAsync<FamiliaModel>();
            await _database.CreateTableAsync<AnimalModel>();

            _initialized = true;
        }
        finally
        {
            InitializationLock.Release();
        }
    }

    // Copia o banco bundled (Resources/Raw/coaxar.db3) para o diretório de dados
    // do app apenas na primeira instalação ou quando o bundled for mais recente.
    // Copia o banco bundled (Resources/Raw/coaxar.db3) para o diretório de dados
    // do app apenas na primeira instalação. Se não existir bundled, inicia vazio.
    private static async Task CopiarBancoBundledAsync()
    {
        var destino = DatabaseConstants.DatabasePath;

        if (File.Exists(destino))
            return;

        try
        {
            await using var origem = await FileSystem.OpenAppPackageFileAsync(
                DatabaseConstants.DatabaseFilename);
            await using var destStream = File.Create(destino);
            await origem.CopyToAsync(destStream);
        }
        catch (FileNotFoundException)
        {
            // banco bundled ausente — tabelas criadas vazias pelo CreateTableAsync
        }
    }

    public static async Task<List<FamiliaModel>> GetFamiliasAsync(string? busca = null)
    {
        var db = await GetDatabaseAsync();
        var familias = await db.Table<FamiliaModel>()
            .OrderBy(f => f.Nome)
            .ToListAsync();

        if (string.IsNullOrWhiteSpace(busca))
            return familias;

        return familias
            .Where(f => f.Nome.Contains(busca.Trim(), StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    public static async Task<List<AnimalModel>> GetAnimaisDaFamiliaAsync(
        string familiaNome,
        string? busca = null)
    {
        var db = await GetDatabaseAsync();

        var familia = await db.Table<FamiliaModel>()
            .Where(f => f.Nome == familiaNome)
            .FirstOrDefaultAsync();

        if (familia is null)
            return [];

        var animais = await db.Table<AnimalModel>()
            .Where(a => a.FamiliaId == familia.Id)
            .OrderBy(a => a.Genero)
            .ThenBy(a => a.ScientificName)
            .ToListAsync();

        if (string.IsNullOrWhiteSpace(busca))
            return animais;

        var termo = busca.Trim();
        return animais
            .Where(a =>
                a.Name.Contains(termo, StringComparison.OrdinalIgnoreCase) ||
                (a.ScientificName?.Contains(termo, StringComparison.OrdinalIgnoreCase) ?? false))
            .ToList();
    }

    public static async Task<List<GeneroGroup>> GetGenerosDaFamiliaAsync(
        string familiaNome,
        string? busca = null)
    {
        var animais = await GetAnimaisDaFamiliaAsync(familiaNome, busca);

        return animais
            .GroupBy(a => a.Genero ?? string.Empty)
            .Select(g => new GeneroGroup(g.Key, g))
            .ToList();
    }

    public static async Task<AnimalModel?> GetAnimalByIdAsync(int id)
    {
        var db = await GetDatabaseAsync();
        return await db.Table<AnimalModel>()
            .Where(a => a.Id == id)
            .FirstOrDefaultAsync();
    }

    public static async Task<int> SaveFamiliaAsync(FamiliaModel familia)
    {
        var db = await GetDatabaseAsync();
        return await db.InsertOrReplaceAsync(familia);
    }

    public static async Task<int> SaveAnimalAsync(AnimalModel animal)
    {
        var db = await GetDatabaseAsync();
        return await db.InsertOrReplaceAsync(animal);
    }

    public static async Task<int> DeleteAnimalAsync(AnimalModel animal)
    {
        var db = await GetDatabaseAsync();
        return await db.DeleteAsync(animal);
    }

    private static async Task<SQLiteAsyncConnection> GetDatabaseAsync()
    {
        await InitializeAsync();
        return _database!;
    }
}
