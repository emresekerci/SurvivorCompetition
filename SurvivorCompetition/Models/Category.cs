namespace SurvivorCompetition.Models
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }

        // Navigation property for related competitors
        public ICollection<Competitor> Competitors { get; set; } = new List<Competitor>();
    }
}
