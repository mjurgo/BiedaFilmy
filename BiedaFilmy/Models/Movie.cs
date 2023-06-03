using Microsoft.AspNetCore.Components;
using System.ComponentModel.DataAnnotations;

namespace BiedaFilmy.Models
{
    public class Movie
    {
        public int Id { get; set; }
        [Display(Name = "Tytuł")]
        public string Title { get; set; }
        [Display(Name = "Opis")]
        public string Description { get; set; }
        [Display(Name = "Długość (min.)")]
        public int Duration { get; set; }
        [Display(Name = "Premiera")]
        public DateTime Release { get; set; }

        public int GenreId { get; set; }
        [Display(Name = "Gatunek")]
        public Genre? Genre { get; set; } = null!;

        [Display(Name = "Średnia ocen")]
        public float Score { get; set; } = 0.0f;

        public ICollection<Employment> Employments { get; } = new List<Employment>();
        public ICollection<Collection> Collections { get; } = new List<Collection>();
        public ICollection<MovieComment> MovieComments { get; } = new List<MovieComment>();
    }
}
