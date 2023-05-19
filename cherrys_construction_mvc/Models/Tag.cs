using cherrys_construction_mvc.EfRepository.Interfaces;

namespace cherrys_construction_mvc.Models
{
    public class Tag: IAggregateRoot
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<ProjectTag> ProjectTags { get; set; }

    }
}
