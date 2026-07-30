using CoaxarApp.Views;
using Microsoft.Maui.Controls.Shapes;

namespace CoaxarApp;

public partial class MainPage : ContentPage
{
    //Fotos da roleta do banner, com o nome científico e autor/ano de cada sapo
    private static readonly (string ImagePath, string ScientificName, string DiscoveryDate)[] BannerFrames =
    [
        ("banner_rhinella_dypticha.png", "Rhinella dypticha", " (Cope, 1862)"),
        ("banner_rhinella_granulosa.png", "Rhinella granulosa", " (Spix, 1824)"),
        ("banner_rhinella_crucifer.png", "Rhinella crucifer", " (Wied-Neuwied, 1821)"),
        ("banner_rhinella_hoogmoedi.png", "Rhinella hoogmoedi", " Caramaschi and Pombal, 2006"),
    ];

    private static readonly TimeSpan BannerInterval = TimeSpan.FromSeconds(4);

    private Ellipse[] _bannerDots = [];
    private IDispatcherTimer? _bannerTimer;
    private int _bannerIndex;

    public MainPage()
    {
        InitializeComponent();

        _bannerDots = [BannerDot1, BannerDot2, BannerDot3, BannerDot4];
        ShowBannerFrame(0);
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        StartBannerRotation();
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        StopBannerRotation();
    }

    //Liga o timer que troca a foto do banner sozinho
    private void StartBannerRotation()
    {
        if (_bannerTimer is not null)
            return;

        _bannerTimer = Dispatcher.CreateTimer();
        _bannerTimer.Interval = BannerInterval;
        _bannerTimer.Tick += OnBannerTimerTick;
        _bannerTimer.Start();
    }

    //Desliga o timer, evitando trocar a foto com a tela fechada
    private void StopBannerRotation()
    {
        if (_bannerTimer is null)
            return;

        _bannerTimer.Tick -= OnBannerTimerTick;
        _bannerTimer.Stop();
        _bannerTimer = null;
    }

    private async void OnBannerTimerTick(object? sender, EventArgs e)
    {
        _bannerIndex = (_bannerIndex + 1) % BannerFrames.Length;

        await BannerImage.FadeTo(0, 200);
        ShowBannerFrame(_bannerIndex);
        await BannerImage.FadeTo(1, 200);
    }

    //Troca a foto e o nome do banner, e destaca o ponto correspondente
    private void ShowBannerFrame(int index)
    {
        var frame = BannerFrames[index];
        BannerImage.Source = frame.ImagePath;
        BannerSpeciesLabel.Text = $"{frame.ScientificName}{frame.DiscoveryDate}";

        for (var i = 0; i < _bannerDots.Length; i++)
            _bannerDots[i].Opacity = i == index ? 1 : 0.4;
    }

    //Abre a tela de famílias ao tocar no banner
    async void OnBannerTapped(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(FamiliasPage));
    }

    //Abre a tela de propostas psicopedagógicas
    async void OnPropostasPsicopedagogicasTapped(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(PropostasPsicopedagogicasPage));
    }

    //Abre a tela do guia de uso
    async void OnGuiaDeUsoTapped(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(GuiaDeUsoPage));
    }
}
