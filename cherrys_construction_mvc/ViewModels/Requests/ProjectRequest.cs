using cherrys_construction_mvc.Models;
using cherrys_construction_mvc.ViewModels.Responce;

namespace cherrys_construction_mvc.ViewModels.Requests
{
    public class ProjectRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ClientName { get; set; }
        public DateTime ProjectStartDate { get; set; }
        public DateTime ProjectEndDate { get; set; }
        public int ServiceTypeId { get; set; }
        public List<ServiceTypeResponce> ServiceTypes { get; set; }
        public List<IFormFile> Files { get; set; }

        public int TagId { get; set; }

        // TAGS
        public IEnumerable<int> TagIds { get; set; }
        public IEnumerable<int> SelectedDeletePhoto { get; set; }
        public IEnumerable<string> TagIdsString { get; set; }
        public List<TagItem> Tags { get; set; }
        public List<ImageResponce> Images { get; set; }

    }
}
