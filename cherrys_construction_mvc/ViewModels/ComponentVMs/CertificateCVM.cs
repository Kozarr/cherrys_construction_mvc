using cherrys_construction_mvc.ViewModels.Responce;

namespace cherrys_construction_mvc.ViewModels.ComponentVMs
{
    public class CertificateCVM
    {
        public CompanyCertificateSettingResponce? CertSettings { get; set; }
        public IEnumerable<CompanyCertificateResponce>? CompanyCertificates { get; set; }
    }
}
