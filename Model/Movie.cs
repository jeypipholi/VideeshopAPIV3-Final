namespace VideoshopAPIV3.Model
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public string ReleaseYear { get; set; }
        public decimal Price { get; set; }
        public ICollection<RentalDetail>? RentalDetails { get; set; }
    }
}
