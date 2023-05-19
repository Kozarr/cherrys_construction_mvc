namespace cherrys_construction_mvc.ViewModels.Responce
{
    public class HeroSliderResponce
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? ButtonText { get; set; }

        public List<HeroSliderImageResponce>? Images { get; set; }
    }
}
