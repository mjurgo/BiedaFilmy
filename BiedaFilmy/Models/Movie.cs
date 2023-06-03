using Microsoft.AspNetCore.Components;

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

        public float Score { get; set; } = 0.0f;

        public ICollection<Employment> Employments { get; } = new List<Employment>();
        public ICollection<Collection> Collections { get; } = new List<Collection>();
        public ICollection<MovieComment> MovieComments { get; } = new List<MovieComment>();
    }
}
