using PosSystem.Models;

namespace PosSystem.Services.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserAsync(string username, string password);
        Task SeedUsersAsync();
    }
}
