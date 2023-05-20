using cherrys_construction_mvc.EfRepository.Interfaces;

namespace cherrys_construction_mvc.Models
{
    public class Service: IAggregateRoot
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Icon { get; set; }

        // Detail Props
        public string? ArticleTitle { get; set; }
        public string? ArticleDescription { get; set; }
        public string? ImageLink { get; set; }
    }
}
