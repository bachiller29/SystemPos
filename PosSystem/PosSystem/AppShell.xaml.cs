using PosSystem.Views;

namespace PosSystem
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("login", typeof(LoginPage));
            Routing.RegisterRoute("appmain", typeof(MainPage));
            Routing.RegisterRoute("clients", typeof(ClientListPage));
            Routing.RegisterRoute("products", typeof(ProductsPage));

            // Manejo seguro de navegación inicial
            this.Navigated += (sender, e) =>
            {
                if (e.Current?.Location.ToString() == "//login")
                {
                    Shell.Current.Dispatcher.Dispatch(async () =>
                    {
                        await Shell.Current.GoToAsync("//login");
                    });
                }
            };
        }
    }
}
