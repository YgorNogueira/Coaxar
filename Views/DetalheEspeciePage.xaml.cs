using CoaxarApp.Models;
using CoaxarApp.Services;

namespace CoaxarApp.Views;

[QueryProperty(nameof(AnimalId), "animalId")]
public partial class DetalheEspeciePage : ContentPage
{
    private AnimalModel? _animal;

    public int AnimalId
    {
        set => _ = CarregarAnimalAsync(value);
    }

    public DetalheEspeciePage()
    {
        InitializeComponent();
    }

    private async Task CarregarAnimalAsync(int id)
    {
        _animal = await AnimalRepository.GetAnimalByIdAsync(id);
        if (_animal is null) return;

        HeaderImage.Source = _animal.ImagemDetalhePath ?? _animal.ImagemPath;
        FotoCreditoLabel.Text = _animal.FotoCredito;
        NomeCientificoSpan.Text = _animal.ScientificName;
        AutorAnoSpan.Text = _animal.DiscoveryDate;
        NomeComumLabel.Text = _animal.Name;
        TamanhoMachoLabel.Text = _animal.MaleSizeAsString;
        TamanhoFemeaLabel.Text = _animal.FemaleSizeAsString;
        MorfologiaSpan.Text = _animal.MorphDescription;
        EspectrogramaImage.Source = _animal.EspectrogramaPath;
        MapaImage.Source = _animal.MapaPath;
        SclehpLabel.Text = _animal.CodigoSonoteca;
    }

    // SQLite: implementar reprodução de áudio com plugin de mídia
    void OnPlayAnuncioTapped(object sender, TappedEventArgs e) { }

    void OnPlaySolturaTapped(object sender, TappedEventArgs e) { }
}
