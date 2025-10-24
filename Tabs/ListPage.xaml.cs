namespace CoaxarApp;

public partial class ListPage : ContentPage
{
	public ListPage(CoaxarViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
    }

	private void OnImageClicked(object sender, EventArgs e)
	{
		Shell.Current.GoToAsync(nameof(DetailsPage));
	}

}