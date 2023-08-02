using cherrys_construction_mvc.ViewModels.Responce;

namespace cherrys_construction_mvc.ViewModels.ComponentVMs
{
    public class QualitiesCVM
    {
        public CompanyQualitiySettingResponce? CompanyQualitiesSettings { get; set; }
        public IEnumerable<CompanyQualityResponce>? Qualities { get; set; }
    }
}
