using cherrys_construction_mvc.EfRepository.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace cherrys_construction_mvc.Models
{
    public class CompanyQuality : IAggregateRoot
    {
        public int Id { get; set; }
        [Required]
        public string? Title  { get; set; }
        public string? Description { get; set; }
    }
}
