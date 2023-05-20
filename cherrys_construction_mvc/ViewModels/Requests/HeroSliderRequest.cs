using cherrys_construction_mvc.ViewModels.Responce;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace cherrys_construction_mvc.ViewModels.Requests
{
    public class HeroSliderRequest
    {
        [Required]
        public string Title { get; set; }
        [ValidateNever]
        public string? Description { get; set; }
        [ValidateNever]
        public string? ButtonText { get; set; }
        [ValidateNever]
        public List<HeroSliderImageResponce>? ActiveImages { get; set; }
        [ValidateNever]
        public List<IFormFile>? ListImages { get; set; }
        [ValidateNever]
        public List<int>? SelectedDeletePhoto { get; set; }


    }
}
