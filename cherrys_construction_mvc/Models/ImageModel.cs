using cherrys_construction_mvc.EfRepository.Interfaces;

namespace cherrys_construction_mvc.Models
{
    public class ImageModel: IAggregateRoot
    {
        public int Id { get; set; }
        public string? PathImage { get; set; }

        public int ProjectId { get; set; }
        public Project? Project { get; set; }


    }
}
