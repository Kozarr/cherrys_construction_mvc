using cherrys_construction_mvc.EfRepository.Interfaces;

namespace cherrys_construction_mvc.Models
{
    public class Testimony: IAggregateRoot
    {
        public int Id { get; set; }

        public string? Name { get; set; }
        public string? Description { get; set; }
        public int Stars { get; set; }

        public string?  Position { get; set; }
        public string? ImageLink { get; set; }

        public int ProjectId { get; set; }
        public Project? Project { get; set; }

    }
}
