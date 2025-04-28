using PosSystem.Models;
using PosSystem.Services.Interfaces;

namespace PosSystem.Services.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly IApiService _apiService;
        private readonly IDatabaseService _dbService;

        public ClientRepository(IApiService apiService, IDatabaseService dbService)
        {
            _apiService = apiService;
            _dbService = dbService;
        }

        public async Task SyncClientsAsync()
        {
            var apiClients = await _apiService.GetClientsAsync();
            var connection = await _dbService.GetConnectionAsync();

            await connection.DeleteAllAsync<Client>();
            await connection.InsertAllAsync(apiClients);
        }

        public async Task<List<Client>> GetClientsAsync()
        {
            var connection = await _dbService.GetConnectionAsync();
            await connection.CreateTableAsync<Client>();

            var localClients = await connection.Table<Client>().ToListAsync();
            if (localClients.Any()) return localClients;

            await SyncClientsAsync();
            return await connection.Table<Client>().ToListAsync();
        }

     
    }
}
