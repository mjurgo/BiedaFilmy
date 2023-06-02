using Microsoft.AspNetCore.Identity;

namespace BiedaFilmy.Models
{
    public class MovieComment
    {
        public int Id { get; set; }

        public int MovieId { get; set; }
        public Movie? Movie { get; set; } = null!;

        public IdentityUser User { get; set; } = null!;

        public string Content { get; set; }
        public DateTime Created { get; set; }
    }
}
