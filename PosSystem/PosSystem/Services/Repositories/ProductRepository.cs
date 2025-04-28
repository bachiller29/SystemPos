using PosSystem.Models;
using PosSystem.Services.Interfaces;

namespace PosSystem.Services.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IApiService _apiService;
        private readonly IDatabaseService _dbService;

        public ProductRepository(IApiService apiService, IDatabaseService dbService)
        {
            _apiService = apiService;
            _dbService = dbService;
        }

        public async Task<List<Product>> GetProductsAsync(bool forceRefresh = false)
        {
            var connection = await _dbService.GetConnectionAsync();
            await connection.CreateTableAsync<Product>();

            if (!forceRefresh)
            {
                var localProducts = await connection.Table<Product>().ToListAsync();
                if (localProducts.Any()) return localProducts;
            }

            await SyncProductsAsync();
            return await connection.Table<Product>().ToListAsync();
        }

        public async Task SyncProductsAsync()
        {
            var apiProducts = await _apiService.GetProductsAsync();
            var connection = await _dbService.GetConnectionAsync();

            await connection.RunInTransactionAsync(async tx =>
            {         
                foreach (var product in apiProducts)
                {
                    await connection.InsertAsync(new Product
                    {
                        ApiId = product.ApiId,
                        Title = product.Title,
                        Price = product.Price,
                        Description = product.Description,
                        Category = product.Category,
                        ImageUrl = product.ImageUrl,
                        Rating = product.Rating,
                        RatingCount = product.RatingCount
                    });
                }
            });
        }
    }
}
