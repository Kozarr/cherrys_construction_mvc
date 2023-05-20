using cherrys_construction_mvc.ViewModels.Responce;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace cherrys_construction_mvc.ViewModels.Requests
{
    public class TestimonyRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [ValidateNever]
        public int Stars { get; set; }
        [ValidateNever]
        public string? Position { get; set; }
        [ValidateNever]
        public string? ImageLink { get; set; }
        public int ProjectId { get; set; }
        [ValidateNever]
        public string? ProjectName { get; set; }
        [ValidateNever]
        public List<ProjectResponce>? Projects { get; set; }
        [ValidateNever]
        public IFormFile? Image { get; set; }

    }
}
