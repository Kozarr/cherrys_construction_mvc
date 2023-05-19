using cherrys_construction_mvc.Models;
using cherrys_construction_mvc.ViewModels.Contact;
using cherrys_construction_mvc.ViewModels.Responce;

namespace cherrys_construction_mvc.ViewModels.Home
{
    public class HomeViewModel
    {
        public HeroSliderResponce? HeroSlider { get; set; }
        public CallToActionSettingResponce? CallToActionSettings { get; set; }
        public IEnumerable<ServiceTypeResponce>? ServiceTypes { get; set; }
        public IEnumerable<ServiceResponce>? Services { get; set; }
        public IEnumerable<CompanyCertificateResponce>? CompanyCertificates { get; set; }
        public IEnumerable<CompanyValueResponce>? CompanyValues { get; set; }
        public IEnumerable<TestimonyResponce>? Testimonies { get; set; }
        public CompanyCertificateSettingResponce? CertSettings { get; set; }
        public CompanyInfoResponce? CompanyInfo { get; set; }
        public CallToActionMessageResponce? Message { get; set; }
        public IFormFile? Attachment { get; set; }

    }
}
