using cherrys_construction_mvc.ViewModels.Requests;
using cherrys_construction_mvc.ViewModels.Responce;

namespace cherrys_construction_mvc.Interfaces
{
    public interface IBlogCategoryService
    {
        Task<IEnumerable<BlogCategoryResponce>> GetBlogCategoriesAsync();
        Task<BlogCategoryResponce> GetBlogCategoryByIdAsync(int id);
        Task CreateBlogCategoryAsync(BlogCategoryRequest request);
        Task UpdateBlogCategoryAsync(int id, BlogCategoryRequest request);
        Task DeleteBlogCategoryAsync(int id);


    }
}
