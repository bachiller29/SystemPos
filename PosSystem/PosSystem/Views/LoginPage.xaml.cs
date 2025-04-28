using PosSystem.ViewModels;

namespace PosSystem.Views;

public partial class LoginPage : ContentPage
{ 
    public LoginPage(LoginViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;       
    }
}