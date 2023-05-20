using cherrys_construction_mvc.Models;
using cherrys_construction_mvc.ViewModels.Responce;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace cherrys_construction_mvc.ViewModels.Requests
{
    public class ProjectRequest
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [ValidateNever]
        public string? ClientName { get; set; }
        public DateTime ProjectStartDate { get; set; }
        public DateTime ProjectEndDate { get; set; }
        public int ServiceTypeId { get; set; }
        [ValidateNever]
        public List<ServiceTypeResponce>? ServiceTypes { get; set; }
        [ValidateNever]
        public List<IFormFile>? Files { get; set; }

        public int TagId { get; set; }

        // TAGS
        [ValidateNever]
        public IEnumerable<int>? TagIds { get; set; }
        [ValidateNever]
        public IEnumerable<int>? SelectedDeletePhoto { get; set; }
        [ValidateNever]
        public IEnumerable<string>? TagIdsString { get; set; }
        [ValidateNever]
        public List<TagItem>? Tags { get; set; }
        [ValidateNever]
        public List<ImageResponce>? Images { get; set; }

    }
}
