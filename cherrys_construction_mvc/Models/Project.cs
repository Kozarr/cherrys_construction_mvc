using cherrys_construction_mvc.EfRepository.Interfaces;

namespace cherrys_construction_mvc.Models
{
    public class Project: IAggregateRoot
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }

        public string? ClientName { get; set; }
        public DateTime ProjectStartDate { get; set; }
        public DateTime ProjectEndDate { get; set; }

        public int ServiceTypeId { get; set; }    
        
        public Testimony? Testimony { get; set; }

        public List<ImageModel>? Images { get; set; }

        public virtual ICollection<ProjectTag>? ProjectTags { get; set; }
    }
}
