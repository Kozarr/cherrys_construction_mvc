using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace cherrys_construction_mvc.ViewModels.Requests
{
    public class MemberRequest
    {
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "Role")]
        [ValidateNever]
        public string? Role { get; set; }
        [Display(Name = "Description")]
        [Required]
        public string Description { get; set; }
        [ValidateNever]
        public string? ImageLink { get; set; }
        [Display(Name = "Instagram Link")]
        [ValidateNever]
        public string? InstagramLink { get; set; }
        [ValidateNever]
        public IFormFile? Image { get; set; }
    }
}
