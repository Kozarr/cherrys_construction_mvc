using cherrys_construction_mvc.Models;
using cherrys_construction_mvc.ViewModels.Responce;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
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
            
        public int? BlogCategoryId { get; set; }
        [ValidateNever]
        public List<BlogCategoryResponce>? BlogCategories { get; set; }

    }
}
