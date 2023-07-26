using cherrys_construction_mvc.Helper;
using cherrys_construction_mvc.ViewModels.Responce;

namespace cherrys_construction_mvc.ViewModels.Blog
{
    public class BlogVM
    {
        public PaginatedList<BlogPostResponce>? Posts { get; set; }
        public IEnumerable<BlogCategoryResponce>? Categories { get; set; }
    }
}
