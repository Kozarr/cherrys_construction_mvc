using cherrys_construction_mvc.EfRepository.Interfaces;

namespace cherrys_construction_mvc.Models
{
    public class HeroSlider : IAggregateRoot
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? ButtonText { get; set; }
        public  List<HeroSliderImage>? Images { get; set; }
    }
}
