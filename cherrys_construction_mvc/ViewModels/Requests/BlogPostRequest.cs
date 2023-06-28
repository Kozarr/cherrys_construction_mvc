using cherrys_construction_mvc.Models;
using System.ComponentModel.DataAnnotations;

namespace cherrys_construction_mvc.ViewModels.Requests
{
    public class BlogPostRequest
    {
        public int Id { get; set; }
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? Description { get; set; } = null;
        public string? ImageLink { get; set; }
        public string? Author { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public IFormFile? Image { get; set; }

        public virtual ICollection<BlogPostBlogCategory>? BlogPostBlogCategories { get; set; }
    }
}
