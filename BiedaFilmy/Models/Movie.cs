namespace BiedaFilmy.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
        public DateTime Release { get; set; }

        public int GenreId { get; set; }
        public Genre? Genre { get; set; } = null!;

        public ICollection<Employment> Employments { get; } = new List<Employment>();
    }
}
