namespace CoaxarApp;

public partial class HomePage : ContentPage
{
	public HomePage()
	{
		InitializeComponent();
	}

	//Mostra a logo com fade e navega para a tela principal
	private async void HomeLoaded(object sender, EventArgs e)
    {
        this.Title = "Coaxar";

		await Task.Delay(500);

		await HomeImage.FadeTo(1, 350);

		await Task.Delay(500);

		await Shell.Current.GoToAsync(nameof(MainPage));
    }
}
