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

            // Aquí registras tus servicios
            builder.Services.AddSingleton<IDatabaseService, DatabaseService>();
            builder.Services.AddSingleton<IUserRepository, UserRepository>();
            builder.Services.AddSingleton<IApiService, ApiService>();
            builder.Services.AddSingleton<IClientRepository, ClientRepository>();

            // ViewModels
            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddSingleton<ClientListViewModel>();

            // Páginas (para navegación)
            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddSingleton<ClientListPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
