using CoaxarApp.Models;
using CoaxarApp.Services;

namespace CoaxarApp.Views;

[QueryProperty(nameof(AnimalId), "animalId")]
public partial class DetalheEspeciePage : ContentPage
{
    //Animal exibido na tela
    private AnimalModel? _animal;

    //Identificador do animal recebido pela navegação
    public int AnimalId
    {
        set => _ = LoadAnimalAsync(value);
    }

    public DetalheEspeciePage()
    {
        InitializeComponent();
    }

    //Anima a entrada da tela
    protected override void OnAppearing()
    {
        base.OnAppearing();
        _ = this.AnimateEntranceAsync();
    }

    //Busca o animal no banco e preenche os campos da tela
    private async Task LoadAnimalAsync(int id)
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

    //Toca o canto de anúncio
    async void OnPlayAdvertisementCallTapped(object sender, TappedEventArgs e) =>
        await ((View)sender).AnimatePressAsync();

    //Toca o canto de soltura
    async void OnPlayReleaseCallTapped(object sender, TappedEventArgs e) =>
        await ((View)sender).AnimatePressAsync();

    //Abre o visualizador com a foto da espécie em tela cheia
    async void OnHeaderImageTapped(object sender, TappedEventArgs e)
    {
        if (_animal is null)
            return;

        ZoomImage.Source = _animal.ImagemDetalhePath ?? _animal.ImagemPath;
        ZoomContainer.Reset();

        ImageViewerOverlay.Opacity = 0;
        ImageViewerOverlay.IsVisible = true;
        await ImageViewerOverlay.FadeTo(1, 200, Easing.CubicOut);
    }

    //Fecha o visualizador de imagem
    async void OnCloseImageViewerTapped(object sender, TappedEventArgs e) =>
        await CloseImageViewerAsync();

    private async Task CloseImageViewerAsync()
    {
        await ImageViewerOverlay.FadeTo(0, 150, Easing.CubicIn);
        ImageViewerOverlay.IsVisible = false;
        ZoomContainer.Reset();
    }

    //Botão voltar do Android fecha o visualizador antes de sair da tela
    protected override bool OnBackButtonPressed()
    {
        if (ImageViewerOverlay.IsVisible)
        {
            Dispatcher.Dispatch(async () => await CloseImageViewerAsync());
            return true;
        }

        return base.OnBackButtonPressed();
    }
}
