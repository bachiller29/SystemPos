using PosSystem.Models;
using PosSystem.Services.Interfaces;

namespace PosSystem.Services.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDatabaseService _databaseService;

        public UserRepository(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public async Task<User> GetUserAsync(string username, string password)
        {
            var db = await _databaseService.GetConnectionAsync();
            return await db.Table<User>()
                           .FirstOrDefaultAsync(u => u.Username == username && u.Password == password);
        }

        public async Task SeedUsersAsync()
        {
            var db = await _databaseService.GetConnectionAsync();
            var count = await db.Table<User>().CountAsync();

            if (count == 0)
            {
                await db.InsertAsync(new User { Username = "admin", Password = "admin123" });
                await db.InsertAsync(new User { Username = "demo", Password = "demo123" });
            }
        }
    }
}
