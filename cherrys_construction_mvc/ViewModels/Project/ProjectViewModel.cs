using cherrys_construction_mvc.ViewModels.Responce;

namespace cherrys_construction_mvc.ViewModels.Project
{
    public class ProjectViewModel
    {
        public IEnumerable<ProjectResponce>? Projects { get; set; }
        public IEnumerable<TagResponce>? Tags { get; set; }
        public CompanyInfoResponce? CompanyInfo { get; set; }
    }
}
