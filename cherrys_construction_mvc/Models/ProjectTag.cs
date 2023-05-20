using cherrys_construction_mvc.EfRepository.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cherrys_construction_mvc.Models
{
    public class ProjectTag:IAggregateRoot
    {
        public int Id { get; set; }
        [Key]
        [Column(Order = 0)]
        [ForeignKey("Tag")]
        public int TagId { get; set; }

        [Key]
        [Column(Order = 1)]
        [ForeignKey("Project")]
        public int ProjectId { get; set; }

        public virtual Project? Project { get; set; }
        public virtual Tag? Tag { get; set; }


    }
}
