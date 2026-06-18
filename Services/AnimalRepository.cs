using CoaxarApp.Models;

namespace CoaxarApp.Services;

public static class AnimalRepository
{
    private static readonly List<FamiliaModel> Familias =
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

    private static readonly List<AnimalModel> Animais =
    [
        new()
        {
            Id = 5,
            Name = "Frostius sp.",
            ScientificName = "Frostius sp.",
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
            Habitat = "Ambientes abertos, áreas urbanas e rurais",
            Habit = "Terrícola",
            WayOfLife = "Noturno",
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
            Habitat = "Ambientes abertos e savanas",
            Habit = "Terrícola",
            WayOfLife = "Noturno",
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
            Habitat = "Mata Atlântica e Caatinga",
            Habit = "Terrícola",
            WayOfLife = "Noturno",
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
            Habitat = "Florestas de terra firme",
            Habit = "Terrícola",
            WayOfLife = "Noturno",
        },
    ];

    public static List<FamiliaModel> GetFamilias(string? busca = null) =>
        Familias
            .Where(familia =>
                string.IsNullOrWhiteSpace(busca) ||
                familia.Nome.Contains(busca.Trim(), StringComparison.OrdinalIgnoreCase))
            .OrderBy(familia => familia.Nome)
            .ToList();

    public static List<AnimalModel> GetAnimaisDaFamilia(
        string familiaNome,
        string? busca = null) =>
        Animais
            .Where(animal => animal.Familia == familiaNome)
            .Where(animal =>
                string.IsNullOrWhiteSpace(busca) ||
                animal.Name.Contains(busca.Trim(), StringComparison.OrdinalIgnoreCase) ||
                (animal.ScientificName?.Contains(
                    busca.Trim(),
                    StringComparison.OrdinalIgnoreCase) ?? false))
            .ToList();

    public static List<GeneroGroup> GetGenerosDaFamilia(
        string familiaNome,
        string? busca = null) =>
        GetAnimaisDaFamilia(familiaNome, busca)
            .GroupBy(animal => animal.Genero ?? string.Empty)
            .Select(grupo => new GeneroGroup(grupo.Key, grupo))
            .ToList();

    public static AnimalModel? GetAnimalById(int id) =>
        Animais.FirstOrDefault(animal => animal.Id == id);
}
