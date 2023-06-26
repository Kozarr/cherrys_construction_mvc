using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace cherrys_construction_mvc.Models
{
    public class BlogPostBlogCategory
    {
        public int Id { get; set; }
        [Key]
        [Column(Order = 0)]
        [ForeignKey("BlogCategory")]
        public int BlogCategoryId { get; set; }

        [Key]
        [Column(Order = 1)]
        [ForeignKey("BlogPost")]
        public int BlogPostId { get; set; }

        public virtual BlogPost? BlogPost { get; set; }
        public virtual BlogCategory? BlogCategory { get; set; }
    }
}
