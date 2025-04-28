using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PosSystem.Services.Interfaces;

namespace PosSystem.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        private readonly IUserRepository _userRepository;

        [ObservableProperty] private string username;
        [ObservableProperty] private string password;
        [ObservableProperty] private bool hasError;
        [ObservableProperty] private string errorMessage;

        public LoginViewModel(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [RelayCommand]
        private async Task Login()
        {
            HasError = false;
            ErrorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            {
                ErrorMessage = "Por favor, ingresa usuario y contraseña.";
                HasError = true;
                return;
            }

            var usuario = await _userRepository.GetUserAsync(username, password);

            if (usuario != null)
            {
                Username = string.Empty;
                Password = string.Empty;
                await Shell.Current.GoToAsync("//clients");
            }
            else
            {
                ErrorMessage = "Credenciales incorrectas.";
                HasError = true;
                Password = string.Empty;
            }
        }
    }
}
