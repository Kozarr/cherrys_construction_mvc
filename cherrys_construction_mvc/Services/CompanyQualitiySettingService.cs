using AutoMapper;
using cherrys_construction_mvc.EfRepository.Interfaces;
using cherrys_construction_mvc.Interfaces;
using cherrys_construction_mvc.Models;
using cherrys_construction_mvc.Services.ImageSharp.Interface;
using cherrys_construction_mvc.Utility;
using cherrys_construction_mvc.ViewModels.Requests;
using cherrys_construction_mvc.ViewModels.Responce;

namespace cherrys_construction_mvc.Services
{
    public class CompanyQualitiySettingService : ICompanyQualitiySettingService
    {
        private readonly IEfRepository<CompanyQualitySetting> _companyQualitySettingRepository;
        private readonly IMapper _mapper;
        private IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<CompanyQualitiySettingService> _logger;
        private readonly IImageProcessorService _imageProcessor;
        public CompanyQualitiySettingService(
            IEfRepository<CompanyQualitySetting> companyQualitySetting,
            IMapper mapper,
            IWebHostEnvironment webHostEnvironment,
            ILogger<CompanyQualitiySettingService> logger,
            IImageProcessorService imageProcessor)
        {
            _companyQualitySettingRepository = companyQualitySetting;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
            _imageProcessor = imageProcessor;
        }

        public async Task CreateCompanyQualitiesSettingAsync(CompanyQualitySettingRequest request)
        {
            if (request.Image != null)
            {
                request.ImageLink = await _imageProcessor.ProcessImageAsync(request.Image, _webHostEnvironment, StaticDetails.StandardImage);
            }
            var compQuality = _mapper.Map<CompanyQualitySetting>(request);
            await _companyQualitySettingRepository.AddAsync(compQuality);
            await _companyQualitySettingRepository.SaveChangesAsync();
        }

        public async Task DeleteCompanyQualitiesSettingAsync(int id)
        {
            var compQuality = await _companyQualitySettingRepository.GetByIdAsync(id);
            if (compQuality != null)
            {
                await _companyQualitySettingRepository.DeleteAsync(compQuality);
                await _companyQualitySettingRepository.SaveChangesAsync();
            }
            else
            {
                _logger.LogWarning("Could not find existing company quality setting in - CompanyQualitySetting Service");
            }
        }

        public async Task<CompanyQualitiySettingResponce> GetCompanyQualitiesSettingByIdAsync(int id)
        {
            var compQuality = await _companyQualitySettingRepository.GetByIdAsync(id);

            return _mapper.Map<CompanyQualitiySettingResponce>(compQuality);
        }

        public async Task<IEnumerable<CompanyQualitiySettingResponce>> GetCompanyQualitiesSettingsAsync()
        {
            var listCompQuality = await _companyQualitySettingRepository.ListAsync();
            return _mapper.Map<IEnumerable<CompanyQualitiySettingResponce>>(listCompQuality);
        }

        public async Task UpdateCompanyQualitiesSettingAsync(int id, CompanyQualitySettingRequest request)
        {
            var compQuality = await _companyQualitySettingRepository.GetByIdAsync(id);
            if (compQuality != null)
            {
                if (request.Image != null)
                {
                    if (!string.IsNullOrWhiteSpace(compQuality.ImageLink))
                    {
                        _imageProcessor.DeleteImage(_webHostEnvironment.WebRootPath, compQuality.ImageLink);
                    }
                    request.ImageLink = await _imageProcessor.ProcessImageAsync(request.Image, _webHostEnvironment, StaticDetails.StandardImage);
                }
                else
                {
                    request.ImageLink = compQuality.ImageLink;
                }
                _mapper.Map(request, compQuality);
                await _companyQualitySettingRepository.UpdateAsync(compQuality);
                await _companyQualitySettingRepository.SaveChangesAsync();
            }
            else
            {
                _logger.LogWarning("Could not find existing company quality setting in - CompanyQualitySetting Service");
            }
        }
    }
}
