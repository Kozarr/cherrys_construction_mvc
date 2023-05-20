using cherrys_construction_mvc.EfRepository.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace cherrys_construction_mvc.Models
{
    public class CallToActionSetting : IAggregateRoot
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Section Title")]
        public string? Title { get; set; }
        [Display(Name = "Section Description")]
        public string? Description { get; set; }
        [Display(Name = "Form Title")]
        [Required]
        public string FormTitle { get; set; }
        [Display(Name = "Form Description")]
        [Required]
        public string FormDescription { get; set; }
        [Display(Name = "Button Text")]
        [Required]
        public string ButtonText { get; set; }
    }
}
