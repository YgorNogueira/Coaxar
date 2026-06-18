using CoaxarApp.Models;
using SQLite;

namespace CoaxarApp.Services;

public static class AnimalRepository
{
    private static readonly SemaphoreSlim InitializationLock = new(1, 1);
    private static SQLiteAsyncConnection? _database;
    private static bool _initialized;

    public static string DatabasePath => DatabaseConstants.DatabasePath;

    public static async Task InitializeAsync()
    {
        if (_initialized)
            return;

        await InitializationLock.WaitAsync();
        try
        {
            if (_initialized)
                return;

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

    public static async Task<List<FamiliaModel>> GetFamiliasAsync(string? busca = null)
    {
        var database = await GetDatabaseAsync();
        var familias = await database.Table<FamiliaModel>()
            .OrderBy(familia => familia.Nome)
            .ToListAsync();

        if (string.IsNullOrWhiteSpace(busca))
            return familias;

        return familias
            .Where(familia =>
                familia.Nome.Contains(busca.Trim(), StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    public static async Task<List<AnimalModel>> GetAnimaisDaFamiliaAsync(
        string familiaNome,
        string? busca = null)
    {
        var database = await GetDatabaseAsync();
        var familia = await database.Table<FamiliaModel>()
            .Where(item => item.Nome == familiaNome)
            .FirstOrDefaultAsync();

        if (familia is null)
            return [];

        var animais = await database.Table<AnimalModel>()
            .Where(animal => animal.FamiliaId == familia.Id)
            .OrderBy(animal => animal.Genero)
            .ThenBy(animal => animal.ScientificName)
            .ToListAsync();

        if (string.IsNullOrWhiteSpace(busca))
            return animais;

        var termo = busca.Trim();
        return animais
            .Where(animal =>
                animal.Name.Contains(termo, StringComparison.OrdinalIgnoreCase) ||
                (animal.ScientificName?.Contains(
                    termo,
                    StringComparison.OrdinalIgnoreCase) ?? false))
            .ToList();
    }

    public static async Task<List<GeneroGroup>> GetGenerosDaFamiliaAsync(
        string familiaNome,
        string? busca = null)
    {
        var animais = await GetAnimaisDaFamiliaAsync(familiaNome, busca);

        return animais
            .GroupBy(animal => animal.Genero ?? string.Empty)
            .Select(grupo => new GeneroGroup(grupo.Key, grupo))
            .ToList();
    }

    public static async Task<AnimalModel?> GetAnimalByIdAsync(int id)
    {
        var database = await GetDatabaseAsync();
        return await database.Table<AnimalModel>()
            .Where(animal => animal.Id == id)
            .FirstOrDefaultAsync();
    }

    public static async Task<int> SaveFamiliaAsync(FamiliaModel familia)
    {
        var database = await GetDatabaseAsync();
        return await database.InsertOrReplaceAsync(familia);
    }

    public static async Task<int> SaveAnimalAsync(AnimalModel animal)
    {
        var database = await GetDatabaseAsync();
        return await database.InsertOrReplaceAsync(animal);
    }

    public static async Task<int> DeleteAnimalAsync(AnimalModel animal)
    {
        var database = await GetDatabaseAsync();
        return await database.DeleteAsync(animal);
    }

    private static async Task<SQLiteAsyncConnection> GetDatabaseAsync()
    {
        await InitializeAsync();
        return _database!;
    }

    private static async Task SeedAsync()
    {
        if (_database is null)
            return;

        if (await _database.Table<FamiliaModel>().CountAsync() == 0)
            await _database.InsertAllAsync(CreateFamilias());

        if (await _database.Table<AnimalModel>().CountAsync() == 0)
            await _database.InsertAllAsync(CreateAnimais());
    }

    private static List<FamiliaModel> CreateFamilias() =>
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

    private static List<AnimalModel> CreateAnimais() =>
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
