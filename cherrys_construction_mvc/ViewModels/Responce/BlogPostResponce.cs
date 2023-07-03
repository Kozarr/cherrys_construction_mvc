using cherrys_construction_mvc.Models;

namespace cherrys_construction_mvc.ViewModels.Responce
{
    public class BlogPostResponce
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; } = null;
        public string? ShortDescription { get; set; }
        public string? ImageLink { get; set; }
        public string? Author { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? CreatedDateString { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedDateString { get; set; }

     }
}
