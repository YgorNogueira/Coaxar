using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CoaxarApp;

public class AnimalDto
{
    [JsonPropertyName("Família")] public string? Familia { get; set; }
    [JsonPropertyName("Espécie")] public string? Especie { get; set; }
    [JsonPropertyName("Descritor e ano")] public string? DescritorEAno { get; set; }
    [JsonPropertyName("Nome comum")] public string? NomeComum { get; set; }
    [JsonPropertyName("Foto")] public string? Foto { get; set; }
    [JsonPropertyName("Descrição morfológica")] public string? DescricaoMorfologica { get; set; }
    [JsonPropertyName("Tamanho")] public string? Tamanho { get; set; }
    [JsonPropertyName("Hábito")] public string? Habito { get; set; }
    [JsonPropertyName("Microhabitat")] public string? Microhabitat { get; set; }
    [JsonPropertyName("Alimentação")] public string? Alimentacao { get; set; }
    [JsonPropertyName("Estado de conservação internacional")] public string? EstadoIntl { get; set; }
    [JsonPropertyName("Estado de conservação nacional")] public string? EstadoNac { get; set; }
    [JsonPropertyName("Estado de conservação estadual")] public string? EstadoEst { get; set; }
    [JsonPropertyName("Vocalização")] public string? Vocalizacao { get; set; }
}

// Importador instanciável que popula a coleção da ViewModel
public class AnimalImporter
{
    private readonly CoaxarViewModel _vm; 

    public AnimalImporter(CoaxarViewModel vm)
    {
        _vm = vm;
    }

    public async Task ImportFromRawAsync(string rawFileName = "anuros.json", bool clearFirst = true)
    {
        // 1) Ler JSON de Resources/Raw
        using var stream = await FileSystem.OpenAppPackageFileAsync(rawFileName);
        using var reader = new StreamReader(stream, Encoding.UTF8);
        var json = await reader.ReadToEndAsync();

        // 2) Desserializar
        var options = new JsonSerializerOptions
        {
            ReadCommentHandling = JsonCommentHandling.Skip,
            AllowTrailingCommas = true
        };
        var dtos = JsonSerializer.Deserialize<List<AnimalDto>>(json, options) ?? new();

        // 3) (opcional) limpar coleção na UI thread
        if (clearFirst)
            await MainThread.InvokeOnMainThreadAsync(() => _vm.Animals.Clear());

        // 4) Iterar pelos DTOs, mapear e adicionar à coleção na UI thread
        foreach (var dto in dtos)
        {
            var model = Map(dto);
            await MainThread.InvokeOnMainThreadAsync(() => _vm.Animals.Add(model));
        }
    }

    private static AnimalModel Map(AnimalDto r)
    {
        static string Clean(string? s) =>
            string.IsNullOrWhiteSpace(s) ? string.Empty : s.Replace("\n", " ").Replace("\r", "").Trim();

        static bool IsThreatened(string? status)
        {
            var s = (status ?? "").ToUpperInvariant();
            if (string.IsNullOrWhiteSpace(s)) return false;
            if (s.Contains("LC") || s.Contains("NE") || s.Contains("DD")) return false;
            return s.Contains("NT") || s.Contains("VU") || s.Contains("EN") || s.Contains("CR") || s.Contains("EW") || s.Contains("EX");
        }

        return new AnimalModel
        {
            CommonName = string.IsNullOrWhiteSpace(r.NomeComum) ? Clean(r.Especie) : Clean(r.NomeComum),
            Family = Clean(r.Familia),
            Species = Clean(r.Especie),
            MorphDescription = Clean(r.DescricaoMorfologica),
            Feeding = Clean(r.Alimentacao),
            MicroHabitat = Clean(r.Microhabitat),
            DiscoveryDate = Clean(r.DescritorEAno),
            Habit = Clean(r.Habito),
            EndangeredInternational = Clean(r.EstadoIntl),
            EndangeredNational = Clean(r.EstadoNac),
            EndangeredState = Clean(r.EstadoEst),
            Size = Clean(r.Tamanho),
            VocalizationPath = Clean(r.Vocalizacao),
        };
    }
}
