namespace CoaxarApp;

public partial class ListPage : ContentPage
{
	public ListPage(CoaxarViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
    }
}