using PosSystem.Views;

namespace PosSystem
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
            Routing.RegisterRoute(nameof(ClientListPage), typeof(ClientListPage));

            GoToAsync("//LoginPage");
        }
    }
}
