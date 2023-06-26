namespace cherrys_construction_mvc.Models
{
    public class BlogPost
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; } = null;
        public string? ImageLink { get; set; }
        public string? Author { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual ICollection<BlogPostBlogCategory>? BlogPostBlogCategories { get; set; }

    }
}
