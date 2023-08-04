using cherrys_construction_mvc.ViewModels.Responce;

namespace cherrys_construction_mvc.ViewModels.Home
{
    public class HomeViewModel
    {
        public CallToActionSettingResponce? CallToActionSettings { get; set; }
        public CompanyInfoResponce? CompanyInfo { get; set; }
        public CallToActionMessageResponce? Message { get; set; }
        public IFormFile? Attachment { get; set; }

    }
}
