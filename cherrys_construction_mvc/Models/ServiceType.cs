using cherrys_construction_mvc.EfRepository.Interfaces;

namespace cherrys_construction_mvc.Models
{
    public class ServiceType: IAggregateRoot
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? ImageLink { get; set; }
        public string? IconImage { get; set; }

        public string? PageTitle { get; set; }
        public string? PageDescription { get; set; }

        public List<Project> Projects { get; set; } = new();

    }
}
