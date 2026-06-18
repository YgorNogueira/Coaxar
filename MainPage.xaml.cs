using CoaxarApp.Views;

namespace CoaxarApp;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    async void OnBannerTapped(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(FamiliasPage));
    }

    async void OnFamiliasTapped(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(FamiliasPage));
    }
}
