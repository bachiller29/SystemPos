using PosSystem.Views;

namespace PosSystem
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            GoToAsync("//LoginPage");
        }
    }
}
