using cherrys_construction_mvc.EfRepository.Interfaces;

namespace cherrys_construction_mvc.Models
{
    public class Member: IAggregateRoot
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Role { get; set; }
        public string? Description { get; set; }
        public string? ImageLink { get; set; }
        public string? InstagramLink { get; set; }
    }
}
