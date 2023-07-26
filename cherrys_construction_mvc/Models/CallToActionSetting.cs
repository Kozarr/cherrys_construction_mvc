using cherrys_construction_mvc.EfRepository.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace cherrys_construction_mvc.Models
{
    public class CallToActionSetting : IAggregateRoot
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Section Title")]
        public string? Title { get; set; }
        [Display(Name = "Section Description")]
        [Required]
        public string? Description { get; set; }
        [Display(Name = "Form Title")]
        public string? FormTitle { get; set; }
        [Display(Name = "Form Description")]
        public string? FormDescription { get; set; }
        [Display(Name = "Button Text")]
        [Required]
        public string? ButtonText { get; set; }
    }
}
