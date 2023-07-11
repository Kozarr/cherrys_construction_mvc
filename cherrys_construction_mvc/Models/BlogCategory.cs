using cherrys_construction_mvc.EfRepository.Interfaces;

namespace cherrys_construction_mvc.Models
{
    public class BlogCategory:IAggregateRoot
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }
}
