using System.ComponentModel.DataAnnotations;

namespace cherrys_construction_mvc.ViewModels.Requests
{
    public class MemberRequest
    {
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "Role")]
        public string Role { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }
        public string ImageLink { get; set; }
        [Display(Name = "Instagram Link")]
        public string InstagramLink { get; set; }
        public IFormFile Image { get; set; }
    }
}
