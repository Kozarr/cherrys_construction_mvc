using cherrys_construction_mvc.ViewModels.Responce;

namespace cherrys_construction_mvc.ViewModels.Blog
{
    public class BlogDetailsViewModel
    {
        public BlogPostResponce? Post { get; set; }
        public List<BlogPostResponce>? BlogList { get; set; }
        public CompanyInfoResponce? CompanyInfo { get; set; }
    }
}
