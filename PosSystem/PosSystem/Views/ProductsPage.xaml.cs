using PosSystem.Models;
using PosSystem.Services.Interfaces;
using PosSystem.ViewModels;

namespace PosSystem.Views;

[QueryProperty(nameof(ClientId), "clientId")]
public partial class ProductsPage : ContentPage
{
    private readonly ProductsViewModel _viewModel;
    private readonly IClientRepository _clientRepository;

    private int _clientId;
    public int ClientId
    {
        get => _clientId;
        set
        {
            _clientId = value;
            LoadClientDetailsAsync();
        }
    }

    public ProductsPage(ProductsViewModel viewModel, IClientRepository clientRepository)
    {
        InitializeComponent();
        _clientRepository = clientRepository;
        BindingContext = _viewModel = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.InitializeAsync();
    }

    private async void LoadClientDetailsAsync()
    {
        if (_clientId > 0)
        {
            var clients = await _clientRepository.GetClientsAsync();
            var client = clients.FirstOrDefault(c => c.Id == _clientId);

            if (client != null)
            {
                _viewModel.InitializeForClient(client);
            }
        }
    }

    public void OnSearchTextChanged(object sender, TextChangedEventArgs e)
    {
        _viewModel.FilterProducts(e.NewTextValue);
    }
}