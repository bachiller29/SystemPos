using PosSystem.ViewModels;

namespace PosSystem.Views;

public partial class ClientListPage : ContentPage
{
    public ClientListPage(ClientListViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is ClientListViewModel vm && vm.LoadClientsCommand?.CanExecute(null) == true)
        {
            vm.LoadClientsCommand.Execute(null);
        }
    }
}