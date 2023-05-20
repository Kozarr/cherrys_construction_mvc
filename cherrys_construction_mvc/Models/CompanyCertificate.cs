using cherrys_construction_mvc.EfRepository.Interfaces;

namespace cherrys_construction_mvc.Models
{
    public class CompanyCertificate : IAggregateRoot
    {
        public int Id { get; set; }
        public string? Icon { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
    }
}
