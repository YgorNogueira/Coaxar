using System.Collections.ObjectModel;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CoaxarApp;

public partial class HomePage : ContentPage
{
    // elementos criados em code-behind
    private Grid _rootGrid = default!;
    private ActivityIndicator _spinner = default!;
    private Label _loadingLabel = default!;
    private Layout _loadingOverlay = default!;

    // ViewModel com a coleção
    public HomePage(CoaxarViewModel viewModel, AnimalImporter importer)
    {
        InitializeComponent();

        BindingContext = viewModel;

        _rootGrid = new Grid();
        var originalContent = this.Content;            // Image
        this.Content = _rootGrid;
        _rootGrid.Children.Add((View)originalContent); // reparent do HomeImage


        // Overlay "Carregando..."
        _spinner = new ActivityIndicator
        {
            IsRunning = false,
            WidthRequest = 32,
            HeightRequest = 32,
            Color = Color.FromArgb("#16A34A"), // verde
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center
        };

        _loadingLabel = new Label
        {
            Text = "Carregando...",
            FontSize = 14,
            FontFamily = "POPS",
            TextColor = Color.FromArgb("#14532D"),
            HorizontalTextAlignment = TextAlignment.Center
        };

        _loadingOverlay = new VerticalStackLayout
        {
            Spacing = 8,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            IsVisible = false,
            InputTransparent = false
        };
        _loadingOverlay.Children.Add(_spinner);
        _loadingOverlay.Children.Add(_loadingLabel);

        _rootGrid.Children.Add(_loadingOverlay);

        HomeLoaded(viewModel);

    }

    private async void HomeLoaded(CoaxarViewModel viewModel)
    {
        try
        {
            ShowLoading(true);

            // Lê o JSON de Resources/Raw e popula a coleção da ViewModel
            await ImportAnimalsFromRawAsync(viewModel.Animals, "anuros.json", clearFirst: true);

            await Task.Delay(300);
            await HomeImage.FadeTo(1, 350);
            await Task.Delay(300);

            await Shell.Current.GoToAsync(nameof(MainPage));
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", $"Falha ao carregar dados: {ex.Message}", "OK");
        }
        finally
        {
            ShowLoading(false);
        }
    }

    private void ShowLoading(bool show)
    {
        _loadingOverlay.IsVisible = show;
        _spinner.IsRunning = show;
    }

    // =============================== //
    // DTO + Importação + Mapeamento   //
    // =============================== //

    // DTO normal (não estática), mapeando 1:1 o JSON
    private class AnimalDto
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
    }

    // Lê o arquivo em Resources/Raw e ADICIONA os modelos na coleção informada
    private async Task ImportAnimalsFromRawAsync(ObservableCollection<AnimalModel> target,
                                                string fileName,
                                                bool clearFirst = true)
    {
        // 1) Ler JSON de Resources/Raw
        using var stream = await FileSystem.OpenAppPackageFileAsync(fileName);
        using var reader = new StreamReader(stream, Encoding.UTF8);
        var json = await reader.ReadToEndAsync();

        // 2) Desserializar em DTOs
        var options = new JsonSerializerOptions
        {
            ReadCommentHandling = JsonCommentHandling.Skip,
            AllowTrailingCommas = true
        };
        var dtos = JsonSerializer.Deserialize<List<AnimalDto>>(json, options) ?? new();

        if (clearFirst)
            await MainThread.InvokeOnMainThreadAsync(() => target.Clear());

        foreach (var dto in dtos)
        {
            var model = MapToAnimalModel(dto);
            await MainThread.InvokeOnMainThreadAsync(() => target.Add(model));
        }
    }

    // Helpers de mapeamento
    private static AnimalModel MapToAnimalModel(AnimalDto r)
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
            VocalizationPath = null
        };
    }
}
