using CommunityToolkit.Maui.Views;

namespace CoaxarApp;

public partial class DetailsPage : ContentPage
{
	public DetailsPage(CoaxarViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
    }

}