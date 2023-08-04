using cherrys_construction_mvc.ViewModels.Responce;

namespace cherrys_construction_mvc.ViewModels.Service
{
    public class ServiceDetailsViewModel
    {
        public IEnumerable<ServiceResponce>? Services { get; set; }
        public CompanyInfoResponce? CompanyInfo { get; set; }
        public ServiceResponce? Service { get; set; }
    }
}
