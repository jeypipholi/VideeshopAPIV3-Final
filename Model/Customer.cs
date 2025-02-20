using System.Text.Json.Serialization;

namespace VideoshopAPIV3.Model
{
    public class Customer
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        
        public ICollection<RentalHeader>? RentalHeaders { get; set; }
        public RentalDetail? RentalDetail { get; set; }
    }
}
