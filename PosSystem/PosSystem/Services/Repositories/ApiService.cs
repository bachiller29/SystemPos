using Newtonsoft.Json;
using PosSystem.DTO;
using PosSystem.Models;
using PosSystem.Models.DTO;
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

        public async Task<List<Product>> GetProductsAsync()
        {
            try
            {
                var response = await _httpClient.GetStringAsync("https://fakestoreapi.com/products");
                var productDtos = JsonConvert.DeserializeObject<List<ProductDto>>(response);

                var products = new List<Product>();

                foreach (var dto in productDtos)
                {
                    products.Add(new Product
                    {
                        ApiId = dto.Id,
                        Title = dto.Title,
                        Price = dto.Price,
                        Description = dto.Description,
                        Category = dto.Category,
                        ImageUrl = dto.ImageUrl,
                        Rating = dto.Rating?.Rate ?? 0,
                        RatingCount = dto.Rating?.Count ?? 0,
                        ImageSource = ImageSource.FromUri(new Uri(dto.ImageUrl))
                    });
                }

                return products;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching products: {ex.Message}");
                return new List<Product>();
            }
        }
    }
}
