namespace CoaxarApp;

public partial class HomePage : ContentPage
{
	public HomePage()
	{
		InitializeComponent();
	}

	private async void HomeLoaded(object sender, EventArgs e)
    {
        // Define o título da página
        this.Title = "Coaxar";

		await Task.Delay(500);

		await HomeImage.FadeTo(1, 350);

		await Task.Delay(500);

		await Shell.Current.GoToAsync(nameof(MainPage));
    }
}