using PosSystem.Models;

namespace PosSystem.Services.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetProductsAsync(bool forceRefresh = false);
        Task SyncProductsAsync();
    }
}
