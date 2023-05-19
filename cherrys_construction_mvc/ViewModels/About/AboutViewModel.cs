using cherrys_construction_mvc.ViewModels.Responce;

namespace cherrys_construction_mvc.ViewModels.About
{
    public class AboutViewModel
    {
        public IEnumerable<MemberResponce>? Members { get; set; }
        public IEnumerable<TestimonyResponce>? Testimonies { get; set; }
        public IEnumerable<CompanyCertificateResponce>? CompanyCertificates { get; set; }
        public IEnumerable<CompanyQualityResponce>? Qualities { get; set; }
        public IEnumerable<CompanyStoryResponce>? Story { get; set; } 
        public CompanyInfoResponce? CompanyInfo { get; set; }
        public CompanyQualitiySettingResponce? CompanyQualitiesSettings { get; set; }
        public CompanyCertificateSettingResponce? CertSettings { get; set; }
    }
}
