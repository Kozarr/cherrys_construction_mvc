using cherrys_construction_mvc.ViewModels.Responce;

namespace cherrys_construction_mvc.ViewModels.Home
{
    public class HomeViewModel
    {
        public HeroSliderResponce? HeroSlider { get; set; }
        public CallToActionSettingResponce? CallToActionSettings { get; set; }
        public IEnumerable<ServiceTypeResponce>? ServiceTypes { get; set; }
        public IEnumerable<ServiceResponce>? Services { get; set; }
        public CompanyInfoResponce? CompanyInfo { get; set; }
        public CallToActionMessageResponce? Message { get; set; }
        public IFormFile? Attachment { get; set; }

    }
}
