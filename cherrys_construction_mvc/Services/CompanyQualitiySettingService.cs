using AutoMapper;
using cherrys_construction_mvc.EfRepository.Interfaces;
using cherrys_construction_mvc.Interfaces;
using cherrys_construction_mvc.Models;
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

        public CompanyQualitiySettingService(IEfRepository<CompanyQualitySetting> companyQualitySetting,
            IMapper mapper,
            IWebHostEnvironment webHostEnvironment,
            ILogger<CompanyQualitiySettingService> logger)
        {
            _companyQualitySettingRepository = companyQualitySetting;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
        }

        public async Task CreateCompanyQualitiySettingAsync(CompanyQualitySettingRequest request)
        {
            if (request.Image != null)
            {
                request.ImageLink = await Helper.Helper.UploadImage(request.Image, _webHostEnvironment, StaticDetails.StandardImage);
            }
            var compQuality = _mapper.Map<CompanyQualitySetting>(request);
            await _companyQualitySettingRepository.AddAsync(compQuality);
            await _companyQualitySettingRepository.SaveChangesAsync();
        }

        public async Task DeleteCompanyQualitiySettingAsync(int id)
        {
            var compQuality = await _companyQualitySettingRepository.GetByIdAsync(id);
            if(compQuality != null)
            {
                await _companyQualitySettingRepository.DeleteAsync(compQuality);
                await _companyQualitySettingRepository.SaveChangesAsync();
            }
            else
            {
                _logger.LogWarning("Could not find existing company quality setting in - CompanyQualitySetting Service");
            }
        }

        public async Task<CompanyQualitiySettingResponce> GetCompanyQualitiySettingByIdAsync(int id)
        {
            var compQuality = await _companyQualitySettingRepository.GetByIdAsync(id);

            return _mapper.Map<CompanyQualitiySettingResponce>(compQuality);
        }

        public async Task<IEnumerable<CompanyQualitiySettingResponce>> GetCompanyQualitiySettingsAsync()
        {
            var listCompquality = await _companyQualitySettingRepository.ListAsync();
            return _mapper.Map<IEnumerable<CompanyQualitiySettingResponce>>(listCompquality);
        }

        public async Task UpdateCompanyQualitiySettingAsync(int id, CompanyQualitySettingRequest request)
        {
            var compQuality = await _companyQualitySettingRepository.GetByIdAsync(id);
            if(compQuality  != null)
            {
                if (request.Image != null)
                {
                    if (!string.IsNullOrWhiteSpace(compQuality.ImageLink))
                    {
                        string wwwRootPath = _webHostEnvironment.WebRootPath;
                        var oldImagePath = Path.Combine(wwwRootPath, compQuality.ImageLink.TrimStart('\\'));
                        if (File.Exists(oldImagePath))
                        {
                            File.Delete(oldImagePath);
                        }
                    }              
                    request.ImageLink = await Helper.Helper.UploadImage(request.Image, _webHostEnvironment, StaticDetails.StandardImage);
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
