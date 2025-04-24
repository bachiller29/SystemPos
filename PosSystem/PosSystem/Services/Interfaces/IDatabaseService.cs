using SQLite;

namespace PosSystem.Services.Interfaces
{
    public interface IDatabaseService
    {
        Task<SQLiteAsyncConnection> GetConnectionAsync();
        Task InitAsync();
    }
}
