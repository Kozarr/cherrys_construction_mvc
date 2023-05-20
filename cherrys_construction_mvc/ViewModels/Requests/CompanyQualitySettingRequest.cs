namespace cherrys_construction_mvc.ViewModels.Requests
{
    public class CompanyQualitySettingRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string? ImageLink { get; set; }
        public IFormFile? Image { get; set; }
    }
}
