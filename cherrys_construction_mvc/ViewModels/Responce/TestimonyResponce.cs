namespace cherrys_construction_mvc.ViewModels.Responce
{
    public class TestimonyResponce
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int Stars { get; set; }
        public string? Position { get; set; }
        public string? ImageLink { get; set; }
        public int ProjectId { get; set; }
        public ProjectResponce? Project { get; set; }

    }
}
