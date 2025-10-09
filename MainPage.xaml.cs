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

    }

}
