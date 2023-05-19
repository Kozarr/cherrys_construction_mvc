using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace cherrys_construction_mvc.ViewModels.Requests
{
    public class CompanyInfoRequest
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please add the company name to display across the website")]
        public string CompanyName { get; set; }
       
        public string CompanyPhoneNumber { get; set; }
        [Required(ErrorMessage = "Please add email to be able to send and recieve messages from the website")]
        public string CompanyEmail { get; set; }

        public string ServiceArea { get; set; }
        public string NavigationImageURL { get; set; }
        public string FooterImageURL { get; set; }

        [Required(ErrorMessage = "Please add text to the send button")]
        public string SendButton { get; set; }

        
        public string FaceBookLink { get; set; }
        public string InstagramLink { get; set; }
        public string YoutubeLink { get; set; }
        public string LinkedInLink { get; set; }
        public string TwitterLink { get; set; }

    }
}
