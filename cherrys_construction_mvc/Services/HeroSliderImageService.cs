using AutoMapper;
using cherrys_construction_mvc.EfRepository.Interfaces;
using cherrys_construction_mvc.Interfaces;
using cherrys_construction_mvc.Models;
using cherrys_construction_mvc.ViewModels.Requests;

namespace cherrys_construction_mvc.Services
{
    public class HeroSliderImageService: IHeroSliderImageService
    {

        private readonly IEfRepository<HeroSliderImage> _heroSliderImageRepository;
        private readonly IMapper _mapper;
        public HeroSliderImageService(IEfRepository<HeroSliderImage> heroSliderImageRepository, IMapper mapper)
        {
           _heroSliderImageRepository = heroSliderImageRepository;
            _mapper = mapper;
        }

        public async Task CreateHeroSliderImageAsync(HeroSliderImageRequest request)
        {
            var image = _mapper.Map<HeroSliderImage>(request);

            await _heroSliderImageRepository.AddAsync(image);
            await _heroSliderImageRepository.SaveChangesAsync();
        }

        public async Task DeleteHeroSliderImageAsync(int id)
        {
            var image = await _heroSliderImageRepository.GetByIdAsync(id);
            if (image != null)
            {               
                await _heroSliderImageRepository.DeleteAsync(image);
                await _heroSliderImageRepository.SaveChangesAsync();
            }
        }
    }
}
