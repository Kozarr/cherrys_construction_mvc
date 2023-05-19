using cherrys_construction_mvc.ViewModels.Requests;
using cherrys_construction_mvc.ViewModels.Responce;

namespace cherrys_construction_mvc.Interfaces
{
    public interface IHeroSliderService
    {
        Task<IEnumerable<HeroSliderResponce>> GetHeroSlidersAsync();
        Task<HeroSliderResponce> GetHeroSliderByIdAsync(int heroSliderId);
        Task CreateHeroSliderAsync(HeroSliderRequest request);
        Task UpdateHeroSliderAsync(int heroSliderId, HeroSliderRequest request);
        Task DeleteHeroSliderAsync(int heroSliderId);
    }
}
