using System.ComponentModel.DataAnnotations;

namespace cherrys_construction_mvc.ViewModels.Requests
{
    public class CompanyQualitySettingRequest
    {
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? Description { get; set; }
        public string? ImageLink { get; set; }
        public IFormFile? Image { get; set; }
    }
}
