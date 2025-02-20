using System.Text.Json.Serialization;

namespace VideoshopAPIV3.Model
{
    public class RentalHeader
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime RentalDate { get; set; }
        public DateTime ReturnDate { get; set; }
        [JsonIgnore]
        public Customer? Customers { get; set; }
        public ICollection<Movie> Movies { get; set; } = new List<Movie>();
        public ICollection<RentalDetail>? RentalDetails { get; set; }
    }
}
