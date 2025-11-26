namespace CoaxarApp;

public partial class DetailsPage : ContentPage
{
	public DetailsPage(CoaxarViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
    }

    private void OnPlayClicked(object sender, EventArgs e)
    {
        try
        {
            VocPlayer.Play();
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}