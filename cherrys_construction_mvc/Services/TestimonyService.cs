using AutoMapper;
using cherrys_construction_mvc.EfRepository.Interfaces;
using cherrys_construction_mvc.Interfaces;
using cherrys_construction_mvc.Models;
using cherrys_construction_mvc.Services.ImageSharp.Interface;
using cherrys_construction_mvc.Specification.TestimonySpec;
using cherrys_construction_mvc.Utility;
using cherrys_construction_mvc.ViewModels.Requests;
using cherrys_construction_mvc.ViewModels.Responce;

namespace cherrys_construction_mvc.Services
{
    public class TestimonyService : ITestimonyService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IEfRepository<Testimony> _testimonyRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<TestimonyService> _logger;
        private readonly IImageProcessorService _imageProcessor;
        public TestimonyService(
            IEfRepository<Testimony> testimonyRepository,
            IMapper mapper,
            IWebHostEnvironment webHostEnvironment,
            ILogger<TestimonyService> logger,
            IImageProcessorService imageProcessor)
        {
            _testimonyRepository = testimonyRepository;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
            _imageProcessor = imageProcessor;
        }

        public async Task CreateTestimonyAsync(TestimonyRequest request)
        {
            if (request.Image != null)
            {
                request.ImageLink = await _imageProcessor.ProcessImageAsync(request.Image, _webHostEnvironment, StaticDetails.PortraitImage, true);
            }
            var testimony = _mapper.Map<Testimony>(request);
            await _testimonyRepository.AddAsync(testimony);
            await _testimonyRepository.SaveChangesAsync();
        }

        public async Task DeleteTestimonyAsync(int testimonyId)
        {
            var testimony = await _testimonyRepository.GetByIdAsync(testimonyId);
            if (testimony != null)
            {
                if (!string.IsNullOrWhiteSpace(testimony.ImageLink))
                {
                    if (testimony.ImageLink != "\\assets\\img\\user-circle.png")
                    {
                        _imageProcessor.DeleteImage(_webHostEnvironment.WebRootPath, testimony.ImageLink);
                    }
                }
                await _testimonyRepository.DeleteAsync(testimony);
                await _testimonyRepository.SaveChangesAsync();
            }
            else
            {
                _logger.LogWarning("Could not find existing testimony to delete");
            }
        }

        public async Task<TestimonyResponce> GetTestimonyByIdAsync(int testimonyId)
        {
            var spec = new TestimonyIncludeFullInfoSpecification(testimonyId);
            var testimony = await _testimonyRepository.GetBySpecAsync(spec);
            return _mapper.Map<TestimonyResponce>(testimony);
        }

        public async Task<IEnumerable<TestimonyResponce>> GetTestimoniesAsync()
        {
            var spec = new TestimonyIncludeFullInfoSpecification();
            var testimonies = await _testimonyRepository.ListAsync(spec);
            return _mapper.Map<IEnumerable<TestimonyResponce>>(testimonies);
        }

        public async Task UpdateTestimonyAsync(int testimonyId, TestimonyRequest request)
        {
            var testimony = await _testimonyRepository.GetByIdAsync(testimonyId);
            if (testimony != null)
            {
                if (request.Image != null)
                {
                    if (!string.IsNullOrWhiteSpace(testimony.ImageLink))
                    {
                        _imageProcessor.DeleteImage(_webHostEnvironment.WebRootPath, testimony.ImageLink);
                    }
                    request.ImageLink = await _imageProcessor.ProcessImageAsync(request.Image, _webHostEnvironment, StaticDetails.PortraitImage, true);
                }
                else
                {
                    request.ImageLink = testimony.ImageLink;
                }
                _mapper.Map(request, testimony);
                await _testimonyRepository.UpdateAsync(testimony);
                await _testimonyRepository.SaveChangesAsync();
            }
            else
            {
                _logger.LogWarning("Could not find existing testimony to update - Testimony Service");
            }
        }
    }
}
