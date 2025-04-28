using PosSystem.Models;
using PosSystem.Services.Interfaces;
using SQLite;

namespace PosSystem.Services.Repositories
{
    public class DatabaseService : IDatabaseService
    {
        private SQLiteAsyncConnection _connection;

        public async Task<SQLiteAsyncConnection> GetConnectionAsync()
        {
            if (_connection == null)
            {
                var dbPath = Path.Combine(FileSystem.AppDataDirectory, "systempost.db3");
                _connection = new SQLiteAsyncConnection(dbPath);
                await InitAsync();
            }

            return _connection;
        }

        public async Task InitAsync()
        {
            var conn = await GetConnectionAsync();
            await conn.CreateTableAsync<Models.User>();
            await conn.CreateTableAsync<Client>();
        }
    }
}
