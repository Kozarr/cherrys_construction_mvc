using cherrys_construction_mvc.ViewModels.Responce;

namespace cherrys_construction_mvc.ViewModels.Blog
{
    public class BlogViewModel
    {
        public IEnumerable<BlogPostResponce>? Posts { get; set; }
        public IEnumerable<BlogCategoryResponce>? Categories { get; set; }
        public CompanyInfoResponce? CompanyInfo { get; set; }
    }
}
