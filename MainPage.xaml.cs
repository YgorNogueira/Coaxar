
namespace CoaxarApp
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage(CoaxarViewModel viewModel)
        {
            this.BindingContext = viewModel;
            InitializeComponent();
        }

        private async void OnGoListClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(ListPage));
        }
    }

}
