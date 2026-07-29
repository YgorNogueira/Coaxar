using CoaxarApp.Models;
using SQLite;

namespace CoaxarApp.Services;

public static class AnimalRepository
{
    //Trava para impedir a inicialização dupla do banco
    private static readonly SemaphoreSlim InitializationLock = new(1, 1);

    //Conexão com o banco SQLite
    private static SQLiteAsyncConnection? _database;
    private static bool _initialized;

    //Prepara o banco: copia o bundled, cria as tabelas e faz o seed
    public static async Task InitializeAsync()
    {
        if (_initialized)
            return;

        await InitializationLock.WaitAsync();
        try
        {
            if (_initialized)
                return;

            await CopyBundledDatabaseAsync();

            _database = new SQLiteAsyncConnection(
                DatabaseConstants.DatabasePath,
                DatabaseConstants.Flags);

            await _database.CreateTableAsync<FamiliaModel>();
            await _database.CreateTableAsync<AnimalModel>();

            await SeedAsync();
            _initialized = true;
        }
        finally
        {
            InitializationLock.Release();
        }
    }

    //Copia o banco bundled (Resources/Raw/coaxar.db3) para o diretório de dados
    //do app na primeira instalação ou quando o banco local estiver vazio/inválido
    private static async Task CopyBundledDatabaseAsync()
    {
        var destination = DatabaseConstants.DatabasePath;
        var bundledDatabaseTemp = Path.Combine(
            FileSystem.CacheDirectory,
            $"bundled-{DatabaseConstants.DatabaseFilename}");

        try
        {
            await using var source = await FileSystem.OpenAppPackageFileAsync(
                DatabaseConstants.DatabaseFilename);
            await using (var tempStream = File.Create(bundledDatabaseTemp))
            {
                await source.CopyToAsync(tempStream);
            }

            if (!File.Exists(destination) ||
                LocalDatabaseIsEmptyOrInvalid(destination, bundledDatabaseTemp))
            {
                File.Copy(bundledDatabaseTemp, destination, true);
            }
        }
        catch (FileNotFoundException)
        {
            //Banco bundled ausente — tabelas criadas vazias pelo CreateTableAsync
        }
        finally
        {
            if (File.Exists(bundledDatabaseTemp))
                File.Delete(bundledDatabaseTemp);
        }
    }

    //Verifica se o banco local está vazio ou inválido em relação ao bundled
    private static bool LocalDatabaseIsEmptyOrInvalid(
        string localDatabase,
        string bundledDatabase)
    {
        var bundledFamilies = CountFamilies(bundledDatabase);

        try
        {
            var localFamilies = CountFamilies(localDatabase);
            return localFamilies == 0 && bundledFamilies > 0;
        }
        catch (SQLiteException)
        {
            return bundledFamilies > 0;
        }
    }

    //Conta as famílias gravadas no banco informado
    private static int CountFamilies(string databasePath)
    {
        using var db = new SQLiteConnection(databasePath, DatabaseConstants.Flags);
        return db.ExecuteScalar<int>("SELECT COUNT(*) FROM Familias");
    }

    //Busca as famílias, com filtro opcional pelo nome
    public static async Task<List<FamiliaModel>> GetFamiliesAsync(string? search = null)
    {
        var db = await GetDatabaseAsync();
        var familias = await db.Table<FamiliaModel>()
            .OrderBy(f => f.Nome)
            .ToListAsync();

        if (string.IsNullOrWhiteSpace(search))
            return familias;

        return familias
            .Where(f => f.Nome.Contains(search.Trim(), StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    //Busca os animais de uma família, com filtro opcional pelo nome
    public static async Task<List<AnimalModel>> GetAnimalsByFamilyAsync(
        string familyName,
        string? search = null)
    {
        var db = await GetDatabaseAsync();

        var familia = await db.Table<FamiliaModel>()
            .Where(f => f.Nome == familyName)
            .FirstOrDefaultAsync();

        if (familia is null)
            return [];

        var animais = await db.Table<AnimalModel>()
            .Where(a => a.FamiliaId == familia.Id)
            .OrderBy(a => a.Genero)
            .ThenBy(a => a.ScientificName)
            .ToListAsync();

        if (string.IsNullOrWhiteSpace(search))
            return animais;

        var term = search.Trim();
        return animais
            .Where(a =>
                a.Name.Contains(term, StringComparison.OrdinalIgnoreCase) ||
                (a.ScientificName?.Contains(term, StringComparison.OrdinalIgnoreCase) ?? false))
            .ToList();
    }

    //Busca os animais da família agrupados por gênero
    public static async Task<List<GeneroGroup>> GetGeneraByFamilyAsync(
        string familyName,
        string? search = null)
    {
        var animais = await GetAnimalsByFamilyAsync(familyName, search);

        return animais
            .GroupBy(a => a.Genero ?? string.Empty)
            .Select(g => new GeneroGroup(g.Key, g))
            .ToList();
    }

    //Busca um animal pelo identificador
    public static async Task<AnimalModel?> GetAnimalByIdAsync(int id)
    {
        var db = await GetDatabaseAsync();
        return await db.Table<AnimalModel>()
            .Where(a => a.Id == id)
            .FirstOrDefaultAsync();
    }

    //Grava ou substitui uma família
    public static async Task<int> SaveFamilyAsync(FamiliaModel familia)
    {
        var db = await GetDatabaseAsync();
        return await db.InsertOrReplaceAsync(familia);
    }

    //Grava ou substitui um animal
    public static async Task<int> SaveAnimalAsync(AnimalModel animal)
    {
        var db = await GetDatabaseAsync();
        return await db.InsertOrReplaceAsync(animal);
    }

    //Remove um animal do banco
    public static async Task<int> DeleteAnimalAsync(AnimalModel animal)
    {
        var db = await GetDatabaseAsync();
        return await db.DeleteAsync(animal);
    }

    //Garante o banco inicializado antes de usar
    private static async Task<SQLiteAsyncConnection> GetDatabaseAsync()
    {
        await InitializeAsync();
        return _database!;
    }

    //Popula o banco com dados iniciais quando as tabelas estão vazias
    private static async Task SeedAsync()
    {
        if (_database is null)
            return;

        if (await _database.Table<FamiliaModel>().CountAsync() == 0)
            await _database.InsertAllAsync(CreateFamilies());

        if (await _database.Table<AnimalModel>().CountAsync() == 0)
            await _database.InsertAllAsync(CreateAnimals());
    }

    //Famílias iniciais do seed
    private static List<FamiliaModel> CreateFamilies() =>
    [
        new() { Id = 1,  Nome = "Aromobatidae",       ImagemPath = "familia_aromobatidae.png" },
        new() { Id = 2,  Nome = "Bufonidae",           ImagemPath = "familia_bufonidae.png" },
        new() { Id = 3,  Nome = "Ceratophryidae",      ImagemPath = "familia_ceratophryidae.png" },
        new() { Id = 4,  Nome = "Craugastoridae",      ImagemPath = "familia_craugastoridae.png" },
        new() { Id = 5,  Nome = "Eleutherodactylidae", ImagemPath = "familia_eleutherodactylidae.png" },
        new() { Id = 6,  Nome = "Hemiphracticidae",    ImagemPath = "familia_hemiphracticidae.png" },
        new() { Id = 7,  Nome = "Hylidae",             ImagemPath = "familia_hylidae.png" },
        new() { Id = 8,  Nome = "Leptodactylidae",     ImagemPath = "familia_leptodactylidae.png" },
        new() { Id = 9,  Nome = "Microhylidae",        ImagemPath = "familia_microhylidae.png" },
        new() { Id = 10, Nome = "Odontophrynidae",     ImagemPath = "familia_odontophrynidae.png" },
        new() { Id = 11, Nome = "Phyllomedusidae",     ImagemPath = "familia_phyllomedusidae.png" },
        new() { Id = 12, Nome = "Ranidae",             ImagemPath = "familia_ranidae.png" },
        new() { Id = 13, Nome = "Pipidae",             ImagemPath = "familia_pipidae.png" },
    ];

    //Animais iniciais do seed
    private static List<AnimalModel> CreateAnimals() =>
    [
        new()
        {
            Id = 5,
            Name = "Frostius sp.",
            ScientificName = "Frostius sp.",
            DiscoveryDate = "",
            FamiliaId = 2,
            Familia = "Bufonidae",
            Genero = "Frostius",
            ImagemPath = "frostius_sp.png",
            MorphDescription = "Informações morfológicas em atualização.",
        },
        new()
        {
            Id = 1,
            Name = "Sapo Cururu",
            ScientificName = "Rhinella dypticha",
            DiscoveryDate = " (Cope, 1862)",
            FamiliaId = 2,
            Familia = "Bufonidae",
            Genero = "Rhinella",
            MaleSize = 9.0f,
            FemaleSize = 15.0f,
            MorphDescription = "O dorso apresenta coloração castanho-escura, com manchas castanho-claras separadas por uma linha vertebral esbranquiçada. O ventre é branco-acastanhado, contendo pequenas manchas escuras na área abdominal. As glândulas paratóides são grandes, estendendo-se da parte superior do tímpano até a região axilar.",
            ImagemPath = "rhinella_dypticha.png",
            ImagemDetalhePath = "rhinella_dypticha_header.png",
            EspectrogramaPath = "espectrograma_dypticha.png",
            MapaPath = "mapa_dypticha.png",
            FotoCredito = "Foto: Negromonte, I. O.",
            CodigoSonoteca = "SCLEHP: 001 / 002 / 003 / 004 / 005",
            VocalizationPath = string.Empty,
            CantoDeSolturaPath = null,
            Habitat = "Ambientes abertos, áreas urbanas e rurais",
            Habit = "Terrícola",
            WayOfLife = "Noturno",
            Endangered = false,
        },
        new()
        {
            Id = 2,
            Name = "Sapo-cururu-pequeno",
            ScientificName = "Rhinella granulosa",
            DiscoveryDate = " (Spix, 1824)",
            FamiliaId = 2,
            Familia = "Bufonidae",
            Genero = "Rhinella",
            MaleSize = 5.5f,
            FemaleSize = 8.0f,
            MorphDescription = "Corpo robusto com pele granulosa. Coloração dorsal variável, geralmente com tons marrons e manchas escuras irregulares.",
            ImagemPath = "rhinella_granulosa.png",
            VocalizationPath = string.Empty,
            Habitat = "Ambientes abertos e savanas",
            Habit = "Terrícola",
            WayOfLife = "Noturno",
            Endangered = false,
        },
        new()
        {
            Id = 3,
            Name = "Sapo-cururu-da-caatinga",
            ScientificName = "Rhinella crucifer",
            DiscoveryDate = " (Wied-Neuwied, 1821)",
            FamiliaId = 2,
            Familia = "Bufonidae",
            Genero = "Rhinella",
            MaleSize = 8.0f,
            FemaleSize = 11.0f,
            MorphDescription = "Coloração dorsal castanho-acinzentada com manchas escuras. Ventre claro com manchas escuras dispersas.",
            ImagemPath = "rhinella_crucifer.png",
            VocalizationPath = string.Empty,
            Habitat = "Mata Atlântica e Caatinga",
            Habit = "Terrícola",
            WayOfLife = "Noturno",
            Endangered = false,
        },
        new()
        {
            Id = 4,
            Name = "Rhinella hoogmoedi",
            ScientificName = "Rhinella hoogmoedi",
            DiscoveryDate = " Caramaschi and Pombal, 2006",
            FamiliaId = 2,
            Familia = "Bufonidae",
            Genero = "Rhinella",
            MaleSize = 4.5f,
            FemaleSize = 6.0f,
            MorphDescription = "Espécie de pequeno porte com coloração dorsal marrom e manchas irregulares.",
            ImagemPath = "rhinella_hoogmoedi.png",
            VocalizationPath = string.Empty,
            Habitat = "Florestas de terra firme",
            Habit = "Terrícola",
            WayOfLife = "Noturno",
            Endangered = false,
        },
    ];
}
