using BiedaFilmy.Enums;
using Microsoft.AspNetCore.Identity;

namespace BiedaFilmy.Models
{
    public class Collection
    {
        public int MovieId { get; set; }
        public Movie Movie { get; set; }

        public int UserId { get; set; }
        public IdentityUser? Person { get; set; }

        public string? Comment { get; set; }
        public int? Mark { get; set; }
        public CollectionStatus Status { get; set; }

    }
}
