using cherrys_construction_mvc.ViewModels.Requests;

namespace cherrys_construction_mvc.Interfaces
{
    public interface IHeroSliderImageService
    {
        Task CreateHeroSliderImageAsync(HeroSliderImageRequest request);
        Task DeleteHeroSliderImageAsync(int id);
    }
}
