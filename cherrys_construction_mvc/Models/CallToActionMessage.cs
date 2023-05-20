using cherrys_construction_mvc.EfRepository.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Configuration;

namespace cherrys_construction_mvc.Models
{
    public class CallToActionMessage: IAggregateRoot
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string? Phone { get; set; }
        [Required]
        [StringLength(140, ErrorMessage = "The subject must be less then 140 characters long.")]
        [StringValidator]
        public string Subject { get; set; }
        [Required]
        [StringLength(500000, ErrorMessage = "Maximum character size: 500,000")]
        public string Body { get; set; }
        

    }
}
