using cherrys_construction_mvc.EfRepository.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace cherrys_construction_mvc.Models
{
    public class BlogPost:IAggregateRoot
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; } = null;
        public string? ImageLink { get; set; }
        public string? Author { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public int? BlogCategoryId { get; set; }
       
    }
}
