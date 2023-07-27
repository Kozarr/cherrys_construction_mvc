using System.ComponentModel.DataAnnotations;

namespace cherrys_construction_mvc.ViewModels.Requests
{
    public class LegalDocumentRequest
    {
        public int Id { get; set; }
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? Body { get; set; }
    }
}
