using cherrys_construction_mvc.ViewModels.Responce;

namespace cherrys_construction_mvc.ViewModels.Service
{
    public class ServiceViewModel
    {
        public IEnumerable<ServiceTypeResponce>? ServiceTypes { get; set; }
        public IEnumerable<ServiceResponce>? Services { get; set; }
        public IEnumerable<CompanyValueResponce>? CompanyValues { get; set; }
        public IEnumerable<TestimonyResponce>? Testimonies { get; set; }
        public CompanyInfoResponce? CompanyInfo { get; set; }

        public ServiceResponce? Service { get; set; }
    }
}
