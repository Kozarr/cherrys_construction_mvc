using System.ComponentModel.DataAnnotations;

namespace cherrys_construction_mvc.ViewModels.Requests
{
    public class TagRequest
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Filter Name")]
        public string Name { get; set; }
    }
}
