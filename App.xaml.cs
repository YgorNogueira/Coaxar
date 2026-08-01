using CoaxarApp.Services;

namespace CoaxarApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //Aquece o banco em segundo plano, sem travar a abertura do app
            //(qualquer consulta garante a inicialização pelo GetDatabaseAsync)
            _ = AnimalRepository.InitializeAsync();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}
