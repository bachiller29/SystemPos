using PosSystem.Services.Interfaces;
using PosSystem.Views;

namespace PosSystem
{
    public partial class App : Application
    {
        public App(IDatabaseService dbService, IUserRepository userRepo)
        {
            InitializeComponent();

            // Solo inicializa la base de datos
            _ = dbService.InitAsync();

            // Usa AppShell como la única instancia de la interfaz principal
            MainPage = new AppShell();
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
