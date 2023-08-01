using AutoMapper;
using cherrys_construction_mvc.EfRepository.Interfaces;
using cherrys_construction_mvc.Interfaces;
using cherrys_construction_mvc.Models;
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
        public TestimonyService(IEfRepository<Testimony> testimonyRepository,
            IMapper mapper,
            IWebHostEnvironment webHostEnvironment,
            ILogger<TestimonyService> logger)
        {
            _testimonyRepository = testimonyRepository;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
        }

        public async Task CreateTestimonyAsync(TestimonyRequest request)
        {
            if (request.Image != null)
            {
                var imageLink = await Helper.Helper.UploadImage(request.Image, _webHostEnvironment, StaticDetails.SquareImage);
                request.ImageLink = imageLink;
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
                        string wwwRootPath = _webHostEnvironment.WebRootPath;
                        var oldImagePath = Path.Combine(wwwRootPath, testimony.ImageLink.TrimStart('\\'));
                        if (File.Exists(oldImagePath))
                        {
                            File.Delete(oldImagePath);
                        }
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
                        string wwwRootPath = _webHostEnvironment.WebRootPath;
                        var oldImagePath = Path.Combine(wwwRootPath, testimony.ImageLink.TrimStart('\\'));
                        if (File.Exists(oldImagePath))
                        {
                            File.Delete(oldImagePath);
                        }
                    }
                    request.ImageLink = await Helper.Helper.UploadImage(request.Image, _webHostEnvironment, StaticDetails.SquareImage);
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
