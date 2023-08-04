using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace cherrys_construction_mvc.ViewModels.Requests
{
    public class CompanyInfoRequest
    {
        [Required(ErrorMessage = "Please add the company name to display across the website")]
        public string? CompanyName { get; set; }
       
        public string? CompanyPhoneNumber { get; set; }
        [Required(ErrorMessage = "Please add email to be able to send and receive messages from the website")]
        public string? CompanyEmail { get; set; }

        public string? ServiceArea { get; set; }
        public string? NavigationImageURL { get; set; }
        public string? FooterImageURL { get; set; }

        [Required(ErrorMessage = "Please add text to the send button")]
        public string? SendButton { get; set; }

        [ValidateNever]
        public string? FaceBookLink { get; set; }
        [ValidateNever]
        public string? InstagramLink { get; set; }
        [ValidateNever]
        public string? YoutubeLink { get; set; }
        [ValidateNever]
        public string? LinkedInLink { get; set; }
        [ValidateNever]
        public string? TwitterLink { get; set; }

    }
}
