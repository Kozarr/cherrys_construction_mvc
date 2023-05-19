using AutoMapper;
using cherrys_construction_mvc.EfRepository.Interfaces;
using cherrys_construction_mvc.Interfaces;
using cherrys_construction_mvc.Models;
using cherrys_construction_mvc.Utility;
using cherrys_construction_mvc.ViewModels.Requests;
using cherrys_construction_mvc.ViewModels.Responce;

namespace cherrys_construction_mvc.Services
{
    public class CompanyCertificateSettingService: ICompanyCertificateSettingService
    {
        private readonly IEfRepository<CompanyCertificateSetting> _companyCertificateSettingRepository;
        private readonly IMapper _mapper;
        private IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<CompanyCertificateSetting> _logger;
        public CompanyCertificateSettingService(IEfRepository<CompanyCertificateSetting> companyCertificateSetting,
            IMapper mapper,
            IWebHostEnvironment webHostEnvironment,
            ILogger<CompanyCertificateSetting> logger)
        {
            _companyCertificateSettingRepository = companyCertificateSetting;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
        }

        public async Task CreateCompanyCertificateSettingAsync(CompanyCertificateSettingRequest request)
        {
            if (request.Image != null)
            {
                request.ImageLink = await Helper.Helper.UploadImage(request.Image, _webHostEnvironment, StaticDetails.StandardImage);
            }
            var compSet = _mapper.Map<CompanyCertificateSetting>(request);
            await _companyCertificateSettingRepository.AddAsync(compSet);
            await _companyCertificateSettingRepository.SaveChangesAsync();
        }

        public async Task DeleteCompanyCertificateSettingAsync(int id)
        {
            var compSet = await _companyCertificateSettingRepository.GetByIdAsync(id);
            if(compSet != null)
            {
                await _companyCertificateSettingRepository.DeleteAsync(compSet);
                await _companyCertificateSettingRepository.SaveChangesAsync();
            }
            else
            {
                _logger.LogWarning("Could not find existing company certificate settings in - CompanyCertificateSetting Service");
            }
         
        }

        public async Task<CompanyCertificateSettingResponce> GetCompanyCertificateSettingByIdAsync(int id)
        {
            var compSet = await _companyCertificateSettingRepository.GetByIdAsync(id);

            return _mapper.Map<CompanyCertificateSettingResponce>(compSet);
        }

        public async Task<IEnumerable<CompanyCertificateSettingResponce>> GetCompanyCertificateSettingsAsync()
        {
            var listCompSet = await _companyCertificateSettingRepository.ListAsync();
            return _mapper.Map<IEnumerable<CompanyCertificateSettingResponce>>(listCompSet);
        }

        public async  Task UpdateCompanyCertificateSettingAsync(int id, CompanyCertificateSettingRequest request)
        {
            var compSet = await _companyCertificateSettingRepository.GetByIdAsync(id);
            if(compSet  == null)
            {
                _logger.LogWarning("Could not find existing company certificate setting in - CompanyCertificateSetting Service");
            }
            else
            {
                if (request.Image != null)
                {
                    string wwwRootPath = _webHostEnvironment.WebRootPath;
                    var oldImagePath = Path.Combine(wwwRootPath, compSet.ImageLink.TrimStart('\\'));
                    if (File.Exists(oldImagePath))
                    {
                        File.Delete(oldImagePath);
                    }
                    request.ImageLink = await Helper.Helper.UploadImage(request.Image, _webHostEnvironment, StaticDetails.StandardImage);
                }
                else
                {
                    request.ImageLink = compSet.ImageLink;
                }
                _mapper.Map(request, compSet);
                await _companyCertificateSettingRepository.UpdateAsync(compSet);
                await _companyCertificateSettingRepository.SaveChangesAsync();
            }
            

        }
    }
}
