using System.ComponentModel.DataAnnotations;

namespace cherrys_construction_mvc.ViewModels.Responce
{
    public class CallToActionMessageResponce
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Email { get; set; }

        public string? Phone { get; set; }

        public string? Subject { get; set; }
        [Required]
        [StringLength(5000, ErrorMessage = "Message must be 10 and 5,000 characters",MinimumLength = 10)]

        public string? Body { get; set; }
    }
}
