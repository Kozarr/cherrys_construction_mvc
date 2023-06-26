namespace cherrys_construction_mvc.Models
{
    public class BlogCategory
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<BlogPostBlogCategory>? BlogPostBlogCategories { get; set; }
    }
}
