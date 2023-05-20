using cherrys_construction_mvc.EfRepository.Interfaces;

namespace cherrys_construction_mvc.Models
{
    public class CompanyStory : IAggregateRoot
    {
        public int Id { get; set; }
        public string? Title { get; set; }

        public string? ArticleTitle { get; set; }
        public string? ArticleDescription { get; set; }
        public string? ArticleSmallText { get; set; }
        public string? ImageLink { get; set; }
    }
}
