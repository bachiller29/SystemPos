using Newtonsoft.Json;
using PosSystem.DTO;
using PosSystem.Models;
using PosSystem.Services.Interfaces;

namespace PosSystem.Services.Repositories
{
    public class ApiService : IApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<Client>> GetClientsAsync()
        {
            var response = await _httpClient.GetStringAsync("https://jsonplaceholder.typicode.com/users");            
            var clientDtos = JsonConvert.DeserializeObject<List<ClientDto>>(response);
            
            return clientDtos.Select(dto => new Client
            {
                ApiId = dto.Id,
                Name = dto.Name,
                Email = dto.Email,
                Phone = dto.Phone,
                Address = dto.Address?.ToString(),
                LoyaltyPoints = new Random().Next(0, 100)
            }).ToList();
        }
    }
}
