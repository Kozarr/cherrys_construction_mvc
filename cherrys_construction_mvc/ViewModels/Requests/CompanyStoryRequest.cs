using System.ComponentModel.DataAnnotations;

namespace cherrys_construction_mvc.ViewModels.Requests
{
    public class CompanyStoryRequest
    {
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? ArticleTitle { get; set; }
        [Required]
        public string? ArticleDescription { get; set; }
        public string? ArticleSmallText { get; set; }
        public string? ImageLink { get; set; }
        public IFormFile? Image { get; set; }
    }
}
