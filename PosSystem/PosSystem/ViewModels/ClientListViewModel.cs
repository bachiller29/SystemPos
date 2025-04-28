using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PosSystem.Models;
using PosSystem.Services.Interfaces;

namespace PosSystem.ViewModels
{
    public class ClientListViewModel : INotifyPropertyChanged
    {
        private readonly IClientRepository _clientRepository;
        public ObservableCollection<Client> Clients { get; set; } = new ObservableCollection<Client>();

        public ClientListViewModel(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
            LoadClientsAsync();
        }

        private async void LoadClientsAsync()
        {
            var clients = await _clientRepository.GetClientsAsync();
            Clients.Clear();
            foreach (var client in clients)
            {
                Clients.Add(client);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
