using PosSystem.Models;

namespace PosSystem.Services.Interfaces
{
    public interface IApiService
    {
        Task<List<Client>> GetClientsAsync();
    }
}
