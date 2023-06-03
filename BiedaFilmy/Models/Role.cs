namespace BiedaFilmy.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Person { get; set; }
        public string? CharacterName { get; set; }
        public string Position { get; set; }

        public int MovieId { get; set; }
        public Movie? Movie { get; set; }
    }
}
