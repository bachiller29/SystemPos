using PosSystem.ViewModels;

namespace PosSystem.Views;

public partial class ClientListPage : ContentPage
{
    public ClientListPage(ClientListViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}