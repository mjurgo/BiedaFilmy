using BiedaFilmy.Enums;
using BiedaFilmy.Utils;
using Microsoft.AspNetCore.Identity;

namespace BiedaFilmy.Models
{
    public class Collection
    {
        public int Id { get; set; }

        public int MovieId { get; set; }
        public Movie? Movie { get; set; }

        public IdentityUser User { get; set; }

        public string? Comment { get; set; }
        public int? Mark { get; set; }
        [CollectionStatus(CollectionStatus.NotSeen)]
        public CollectionStatus Status { get; set; }

    }
}
