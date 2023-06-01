using BiedaFilmy.Enums;

namespace BiedaFilmy.Models
{
    public class Employment
    {
        public int MovieId { get; set; }
        public Movie Movie { get; set; }

        public int PersonId { get; set; }
        public Person Person { get; set; }

        public Job Job { get; set; }
    }
}
