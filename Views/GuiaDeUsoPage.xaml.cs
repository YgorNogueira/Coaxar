using CoaxarApp.Services;

namespace CoaxarApp.Views;

public partial class GuiaDeUsoPage : ContentPage
{
    public GuiaDeUsoPage()
    {
        InitializeComponent();
    }

    //Anima a entrada da tela
    protected override void OnAppearing()
    {
        base.OnAppearing();
        _ = this.AnimateEntranceAsync();
    }
}
