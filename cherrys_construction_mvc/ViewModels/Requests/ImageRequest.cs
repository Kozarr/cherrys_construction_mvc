using System.ComponentModel.DataAnnotations;

namespace cherrys_construction_mvc.ViewModels.Requests
{
    public class ImageRequest
    {
        [Required]
        public string? PathImage { get; set; }
        public int ProjectId { get; set; }
    }
}
