using Microsoft.Extensions.Logging;
using PosSystem.Services.Interfaces;
using PosSystem.Services.Repositories;
using PosSystem.ViewModels;
using PosSystem.Views;

namespace PosSystem
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // Servicios Singleton (globales)
            builder.Services.AddSingleton<IDatabaseService, DatabaseService>();
            builder.Services.AddSingleton<IApiService, ApiService>();

            // Repositorios (Transient si tienen estado)
            builder.Services.AddTransient<IUserRepository, UserRepository>();
            builder.Services.AddTransient<IClientRepository, ClientRepository>();
            builder.Services.AddTransient<IProductRepository, ProductRepository>();

            // ViewModels (Transient para recibir parámetros frescos)
            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddTransient<ClientListViewModel>();
            builder.Services.AddTransient<ProductsViewModel>();

            // Páginas (Transient para navegación limpia)
            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<ClientListPage>();
            builder.Services.AddTransient<ProductsPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
