using cherrys_construction_mvc.ViewModels.Requests;
using cherrys_construction_mvc.ViewModels.Responce;

namespace cherrys_construction_mvc.Interfaces
{
    public interface IBlogPostService
    {
        Task<IEnumerable<BlogPostResponce>> GetBlogPostsAsync();
        Task<BlogPostResponce> GetBlogPostByIdAsync(int id);
        Task CreateBlogPostAsync(BlogPostRequest request);
        Task UpdateBlogPostAsync(int id, BlogPostRequest request);
        Task DeleteBlogPostAsync(int id);
        Task SaveChangesAsync();

    }
}
