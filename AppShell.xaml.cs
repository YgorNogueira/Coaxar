using CoaxarApp.Views;

namespace CoaxarApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            //Registra as rotas de navegação do app
            Routing.RegisterRoute(nameof(FamiliasPage), typeof(FamiliasPage));
            Routing.RegisterRoute(nameof(EspeciesDaFamiliaPage), typeof(EspeciesDaFamiliaPage));
            Routing.RegisterRoute(nameof(DetalheEspeciePage), typeof(DetalheEspeciePage));
            Routing.RegisterRoute(nameof(GuiaDeUsoPage), typeof(GuiaDeUsoPage));
        }
    }
}
