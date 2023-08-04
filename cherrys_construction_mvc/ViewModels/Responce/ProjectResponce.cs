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
        public ServiceTypeResponce? ServiceType { get; set; }

        //public Category? Category { get; set; }
        public TestimonyResponce? Testimony { get; set; }

        public List<ImageResponce>? Images { get; set; }
        public List<ProjectTag>? ProjectTags { get; set; }
    }
}
