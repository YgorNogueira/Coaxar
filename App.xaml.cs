using CoaxarApp.Services;

namespace CoaxarApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            Task.Run(AnimalRepository.InitializeAsync).GetAwaiter().GetResult();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}
