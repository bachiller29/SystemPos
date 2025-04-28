using PosSystem.Models;

namespace PosSystem.DTO
{
    public class ClientDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public AddressData Address { get; set; }

        public class AddressData
        {
            public string Street { get; set; }
            public string Suite { get; set; }
            public string City { get; set; }
            public string Zipcode { get; set; }

            public override string ToString() => $"{Street}, {Suite}, {City} ({Zipcode})";
        }
    }
}
