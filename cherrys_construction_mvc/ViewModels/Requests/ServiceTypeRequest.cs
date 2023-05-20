using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace cherrys_construction_mvc.ViewModels.Requests
{
    public class ServiceTypeRequest
    {
        [Required]
        public string Title { get; set; }
        [ValidateNever]
        public string? Description { get; set; }
        [ValidateNever]
        public string? ImageLink { get; set; }
        [ValidateNever]
        public IFormFile? Image { get; set; }
    }
}
