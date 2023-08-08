using cherrys_construction_mvc.EfRepository.Interfaces;

namespace cherrys_construction_mvc.Models
{
    public class HeroSliderImage : IAggregateRoot
    {

        public int Id { get; set; }
        public string? PathImage { get; set; }

        public int HeroSliderId { get; set; }
        public HeroSlider? HeroSlider { get; set; }


    }
}
