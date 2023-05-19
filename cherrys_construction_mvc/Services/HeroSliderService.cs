using AutoMapper;
using cherrys_construction_mvc.EfRepository.Interfaces;
using cherrys_construction_mvc.Interfaces;
using cherrys_construction_mvc.Models;
using cherrys_construction_mvc.Specification.HeroSliderSpec;
using cherrys_construction_mvc.Utility;
using cherrys_construction_mvc.ViewModels.Requests;
using cherrys_construction_mvc.ViewModels.Responce;

namespace cherrys_construction_mvc.Services
{
    public class HeroSliderService : IHeroSliderService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IEfRepository<HeroSlider> _heroSliderRepository;
        private readonly IEfRepository<HeroSliderImage> _heroSliderImageRepository;
        private readonly IHeroSliderImageService _imageService;
        private readonly IMapper _mapper;
        private readonly ILogger<HeroSliderService> _logger;
        public HeroSliderService(IEfRepository<HeroSliderImage> heroSliderImageRepository,
            IEfRepository<HeroSlider> heroSliderRepository, 
            IMapper mapper, 
            IWebHostEnvironment webHostEnvironment, 
            IHeroSliderImageService imageService,
            ILogger<HeroSliderService> logger)
        {
            _heroSliderRepository = heroSliderRepository;
            _heroSliderImageRepository = heroSliderImageRepository;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _imageService = imageService;
            _logger = logger;
        }
        public async Task CreateHeroSliderAsync(HeroSliderRequest request)
        {
            var heroSlider = _mapper.Map<HeroSlider>(request);

            await _heroSliderRepository.AddAsync(heroSlider);
            await _heroSliderRepository.SaveChangesAsync();

            if (request.ListImages.Count > 0)
            {
                foreach (var item in request.ListImages)
                {
                    var imageRequest = new HeroSliderImageRequest()
                    {
                        PathImage = Helper.Helper.UploadImage(item, _webHostEnvironment, StaticDetails.LargeCoverImage).Result,
                        HeroSliderId = heroSlider.Id
                    };
                    await _imageService.CreateHeroSliderImageAsync(imageRequest);
                }
            }
        }

        public async Task DeleteHeroSliderAsync(int heroSliderId)
        {
            var slider = await _heroSliderRepository.GetByIdAsync(heroSliderId);
            if(slider == null)
            {
                _logger.LogWarning("Failed to find hero slide to delete");
            }
            else
            {
                await _heroSliderRepository.DeleteAsync(slider);
                await _heroSliderRepository.SaveChangesAsync();
            }

        }

        public async Task<HeroSliderResponce> GetHeroSliderByIdAsync(int heroSliderId)
        {
            var spec = new HeroSliderIncludeFullInfoSpecification(heroSliderId);
            var heroSlider = await _heroSliderRepository.GetBySpecAsync(spec);

            return _mapper.Map<HeroSliderResponce>(heroSlider);
        }

        public async Task<IEnumerable<HeroSliderResponce>> GetHeroSlidersAsync()
        {
            var spec = new HeroSliderIncludeFullInfoSpecification();
            var heroSliders = await _heroSliderRepository.ListAsync(spec);

            return _mapper.Map<IEnumerable<HeroSliderResponce>>(heroSliders);

        }

        public async Task UpdateHeroSliderAsync(int heroSliderId, HeroSliderRequest request)
        {
            if(request != null)
            {
                var slider = await _heroSliderRepository.GetByIdAsync(heroSliderId);
                if(slider == null)
                {
                    _logger.LogWarning("Could not find existing hero slider in - HeroSlider Service");
                }
                else
                {
                    _mapper.Map(request, slider);
                    await _heroSliderRepository.UpdateAsync(slider);
                    await _heroSliderRepository.SaveChangesAsync();

                    if (request.ListImages != null)
                    {
                        foreach (var item in request.ListImages)
                        {
                            var imageRequest = new HeroSliderImageRequest()
                            {
                                PathImage = Helper.Helper.UploadImage(item, _webHostEnvironment, StaticDetails.LargeCoverImage).Result,
                                HeroSliderId = heroSliderId
                            };
                            await _imageService.CreateHeroSliderImageAsync(imageRequest);
                        }
                    }
                    if (request.SelectedDeletePhoto != null)
                    {
                        foreach (var id in request.SelectedDeletePhoto)
                        {
                            var imageForDelete = await _heroSliderImageRepository.GetByIdAsync(id);
                            if(imageForDelete == null)
                            {
                                _logger.LogWarning("Could not find a selected photo to delete in HeroSlider Service");
                            }
                            else
                            {
                                var fullPathForDelete = _webHostEnvironment.WebRootPath + imageForDelete.PathImage;
                                Helper.Helper.DeleteImage(fullPathForDelete);
                                await _imageService.DeleteHeroSliderImageAsync(id);
                            }                          
                        }
                    }
                }              
            }
            else
            {
                _logger.LogWarning("Didnt receive request to update in - HerSlider Service");
            }
           
        }
    }
}
