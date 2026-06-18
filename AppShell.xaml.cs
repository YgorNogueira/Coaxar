using CoaxarApp.Views;

namespace CoaxarApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
            Routing.RegisterRoute(nameof(FamiliasPage), typeof(FamiliasPage));
            Routing.RegisterRoute(nameof(EspeciesDaFamiliaPage), typeof(EspeciesDaFamiliaPage));
            Routing.RegisterRoute(nameof(DetalheEspeciePage), typeof(DetalheEspeciePage));
        }
    }
}
