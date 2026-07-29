using CoaxarApp.Views;

namespace CoaxarApp;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    //Abre a tela de famílias ao tocar no banner
    async void OnBannerTapped(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(FamiliasPage));
    }

    //Abre a tela de famílias
    async void OnFamiliasTapped(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(FamiliasPage));
    }

    //Abre a tela do guia de uso
    async void OnGuiaDeUsoTapped(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(GuiaDeUsoPage));
    }
}
