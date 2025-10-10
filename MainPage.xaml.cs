using CoaxarApp.ViewModels;

namespace CoaxarApp
{
    public partial class MainPage : ContentPage
    {
        public CoaxarViewModel ViewModel = new CoaxarViewModel();
        int count = 0;

        public MainPage()
        {
            this.BindingContext = ViewModel;
            InitializeComponent();
        }

        private async void OnGoListCLicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(ListPage));
        }
    }

}
