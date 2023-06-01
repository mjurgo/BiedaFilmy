namespace BiedaFilmy.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime Birthday { get; set; }

        public ICollection<Employment> Employments { get; }
    }
}
