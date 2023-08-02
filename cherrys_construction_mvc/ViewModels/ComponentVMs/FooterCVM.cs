using cherrys_construction_mvc.ViewModels.Responce;

namespace cherrys_construction_mvc.ViewModels.FooterComponentVM
{
    public class FooterCVM
    {
        public CompanyInfoResponce? CompanyInfo { get; set; }
        public IEnumerable<LegalDocumentResponce>? LegalDocuments { get; set; }
    }
}
