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
            var usuario = await _userRepository.GetUserAsync(username, password);

            if (usuario != null)
            {
                hasError = false;
                await Shell.Current.GoToAsync("//MainPage");
            }
            else
            {
                errorMessage = "Credenciales incorrectas";
                hasError = true;
            }
        }
    }
}
