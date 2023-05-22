using System.ComponentModel.DataAnnotations;

namespace cherrys_construction_mvc.ViewModels.Requests
{
    public class CallToActionSettingRequest
    {
        [Required]
        public string Title { get; set; }
        [Required]
        
        public string Description { get; set; }
       
        public string? FormTitle { get; set; }
       
        public string? FormDescription { get; set; }
        [Required]
       
        public string ButtonText { get; set; }
    }
}
