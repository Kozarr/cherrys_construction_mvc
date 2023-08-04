using System.ComponentModel.DataAnnotations;

namespace cherrys_construction_mvc.ViewModels.Requests
{
    public class CompanyCertificateRequest
    {
        [Required]
        public string? Title { get; set; }
        public string? Description { get; set; }
    }
}
