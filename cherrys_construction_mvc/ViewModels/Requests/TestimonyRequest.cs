using cherrys_construction_mvc.ViewModels.Responce;

namespace cherrys_construction_mvc.ViewModels.Requests
{
    public class TestimonyRequest
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int Stars { get; set; }
        public string? Position { get; set; }
        public string? ImageLink { get; set; }
        public int ProjectId { get; set; }
        public string? ProjectName { get; set; }
        public List<ProjectResponce>? Projects { get; set; }
        public IFormFile? Image { get; set; }

    }
}
