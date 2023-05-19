using cherrys_construction_mvc.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace cherrys_construction_mvc.ViewModels.Responce
{
    public class ProjectResponce
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? ClientName { get; set; }
        public DateTime ProjectStartDate { get; set; }
        public DateTime ProjectEndDate { get; set; }
        public int ServiceTypeId { get; set; }
        [ForeignKey("ServiceTypeId")]
        public ServiceType? ProjectServiceType { get; set; }

        //public Category? Category { get; set; }
        public Testimony? ProjectTestimony { get; set; }

        public List<ImageResponce>? Images { get; set; }
        public List<ProjectTag>? ProjectTags { get; set; }
    }
}
