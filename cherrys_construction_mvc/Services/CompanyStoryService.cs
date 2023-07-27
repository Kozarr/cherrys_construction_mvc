using AutoMapper;
using cherrys_construction_mvc.EfRepository.Interfaces;
using cherrys_construction_mvc.Interfaces;
using cherrys_construction_mvc.Models;
using cherrys_construction_mvc.Utility;
using cherrys_construction_mvc.ViewModels.Requests;
using cherrys_construction_mvc.ViewModels.Responce;

namespace cherrys_construction_mvc.Services
{
    public class CompanyStoryService : ICompanyStoryService
    {
        private readonly IEfRepository<CompanyStory> _companyStoryRepository;
        private readonly IMapper _mapper;
        private IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<CompanyStoryService> _logger;

        public CompanyStoryService(IEfRepository<CompanyStory> companyStoryRepository, 
            IMapper mapper, 
            IWebHostEnvironment webHostEnvironment,
            ILogger<CompanyStoryService> logger)
        {
             _companyStoryRepository = companyStoryRepository;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
        }
        public async Task CreateCompanyStoryAsync(CompanyStoryRequest request)
        {
            if (request.Image != null)
            {
                request.ImageLink = await Helper.Helper.UploadImage(request.Image, _webHostEnvironment, StaticDetails.StandardImage);
            }
            var companyStory = _mapper.Map<CompanyStory>(request);
            await _companyStoryRepository.AddAsync(companyStory);
            await _companyStoryRepository.SaveChangesAsync();

        }

        public async Task DeleteCompanyStoryAsync(int companyStoryId)
        {
            var companyStory = await _companyStoryRepository.GetByIdAsync(companyStoryId);
            if (companyStory != null)
            {
                await _companyStoryRepository.DeleteAsync(companyStory);
                await _companyStoryRepository.SaveChangesAsync();
            }
        }
        public async Task<IEnumerable<CompanyStoryResponce>> GetCompanyStoriesAsync()
        {
            var companyStories = await _companyStoryRepository.ListAsync();
            return _mapper.Map<IEnumerable<CompanyStoryResponce>>(companyStories);
        }

        public async Task<CompanyStoryResponce> GetCompanyStoryByIdAsync(int companyStoryId)
        {
            var companyValue = await _companyStoryRepository.GetByIdAsync(companyStoryId);
            return _mapper.Map<CompanyStoryResponce>(companyValue);
        }

        public async Task UpdateCompanyStoryAsync(int companyStoryId, CompanyStoryRequest request)
        {
            var companyStory = await _companyStoryRepository.GetByIdAsync(companyStoryId);
            if (companyStory != null)
            {
                if (request.Image != null)
                {
                    if (!string.IsNullOrWhiteSpace(companyStory.ImageLink))
                    {
                        string wwwRootPath = _webHostEnvironment.WebRootPath;
                        var oldImagePath = Path.Combine(wwwRootPath, companyStory.ImageLink.TrimStart('\\'));
                        if (File.Exists(oldImagePath))
                        {
                            File.Delete(oldImagePath);
                        }
                    }
                    request.ImageLink = await Helper.Helper.UploadImage(request.Image, _webHostEnvironment, StaticDetails.StandardImage);
                }
                else
                {
                    request.ImageLink = companyStory.ImageLink;
                }

                _mapper.Map(request, companyStory);
                await _companyStoryRepository.UpdateAsync(companyStory);
                await _companyStoryRepository.SaveChangesAsync();
            }
            else
            {
                _logger.LogWarning("Could not find existing company story to update in - CompanyStory Service");
            }
        }
    }
}
