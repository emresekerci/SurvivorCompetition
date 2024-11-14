namespace SurvivorCompetition.Models
{
    public class Competitor : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int CategoryId { get; set; }

        // Navigation property
        public Category Category { get; set; }
    }
}
