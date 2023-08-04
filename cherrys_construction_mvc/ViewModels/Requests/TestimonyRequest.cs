using cherrys_construction_mvc.ViewModels.Responce;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace cherrys_construction_mvc.ViewModels.Requests
{
    public class TestimonyRequest
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Description { get; set; }
        [Range(0,5, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int Stars { get; set; }
        [ValidateNever]
        public string? Position { get; set; }
        [ValidateNever]
        public string? ImageLink { get; set; }
        [ValidateNever]
        public int ProjectId { get; set; }
        [ValidateNever]
        public string? ProjectName { get; set; }
        [ValidateNever]
        public List<ProjectResponce>? Projects { get; set; }
        [ValidateNever]
        public IFormFile? Image { get; set; }
        public ProjectResponce? CurrentProject { get; set; }
    }
}
