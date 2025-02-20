using System.Text.Json.Serialization;

namespace VideoshopAPIV3.Model
{
    public class RentalDetail
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public int RentalHeaderId { get; set; }

        [JsonIgnore]
        public Movie? Movie { get; set; }
        [JsonIgnore]
        public RentalHeader? RentalHeader { get; set; }

    }
}
