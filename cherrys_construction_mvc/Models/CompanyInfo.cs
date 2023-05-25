using cherrys_construction_mvc.EfRepository.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace cherrys_construction_mvc.Models
{
    public class CompanyInfo: IAggregateRoot
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? CompanyName { get; set; }
        [Phone]
        public string? CompanyPhoneNumber { get; set; } = null;
        [EmailAddress]
        [Required]
        public string? CompanyEmail { get; set; }

        public string? ServiceArea { get; set; }
        public string? NavigationImageURL { get; set; }
        public string? FooterImageURL { get; set; }

        [Required]
        public string? SendButton { get; set; }

        // Social Links
        public string? FaceBookLink { get; set; }
        public string? InstagramLink { get; set; }
        public string? YoutubeLink { get; set; }
        public string? LinkedInLink { get; set; }
        public string? TwitterLink { get; set; }
    }
}
