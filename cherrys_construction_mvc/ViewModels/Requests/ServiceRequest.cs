using System.ComponentModel.DataAnnotations;

namespace cherrys_construction_mvc.ViewModels.Requests
{
    public class ServiceRequest
    {
        public  int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        public string Icon { get; set; }

        public string ArticleTitle { get; set; }
        public string ArticleDescription { get; set; }
        public string ImageLink { get; set; }
        public IFormFile Image { get; set; }
    }
}
