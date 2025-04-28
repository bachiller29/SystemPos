using Newtonsoft.Json;
using SQLite;

namespace PosSystem.Models
{
    [Table("Clients")]
    public class Client
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int ApiId { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public int LoyaltyPoints { get; set; } // Puntos de fidelización

        public string Phone { get; set; }

        public string Address { get; set; }
    }
}