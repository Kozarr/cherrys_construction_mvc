using cherrys_construction_mvc.EfRepository.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace cherrys_construction_mvc.Models
{
    public class LegalDocument: IAggregateRoot
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
    }
}
