using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using PosSystem.Models;
using PosSystem.Services.Interfaces;
using PosSystem.Views;

namespace PosSystem.ViewModels
{
    public class ClientListViewModel : INotifyPropertyChanged
    {
        private readonly IClientRepository _clientRepository;

        public ObservableCollection<Client> Clients { get; set; } = new ObservableCollection<Client>();

        public ICommand LoadClientsCommand { get; }
        public ICommand NavigateToProductsCommand { get; }

        public bool IsBusy { get; private set; }

        public ClientListViewModel(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;

            LoadClientsCommand = new Command(async () => await LoadClientsAsync());
            NavigateToProductsCommand = new Command<Client>(async (client) => await NavigateToProductsAsync(client));
        }

        private async Task LoadClientsAsync()
        {
            if (IsBusy) return;

            try
            {
                IsBusy = true;
                OnPropertyChanged(nameof(IsBusy));

                var clients = await _clientRepository.GetClientsAsync();
                Clients.Clear();
                foreach (var client in clients)
                {
                    Clients.Add(client);
                }
            }
            finally
            {
                IsBusy = false;
                OnPropertyChanged(nameof(IsBusy));
            }
        }

        private async Task NavigateToProductsAsync(Client client)
        {
            if (client == null)
                return;
            
            await Shell.Current.GoToAsync($"products?clientId={client.Id}");      
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
