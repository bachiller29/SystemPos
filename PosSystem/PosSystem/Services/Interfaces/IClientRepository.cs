using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PosSystem.Models;

namespace PosSystem.Services.Interfaces
{
    public interface IClientRepository
    {
        Task<List<Client>> GetClientsAsync();
        Task SyncClientsAsync(); // Sincroniza API con SQLite
    }
}
