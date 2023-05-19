using AutoMapper;
using cherrys_construction_mvc.EfRepository.Interfaces;
using cherrys_construction_mvc.Interfaces;
using cherrys_construction_mvc.Models;
using cherrys_construction_mvc.ViewModels.Requests;
using cherrys_construction_mvc.ViewModels.Responce;

namespace cherrys_construction_mvc.Services
{
    public class CompanyQualityService : ICompanyQualityService
    {
        private readonly IEfRepository<CompanyQuality> _companyQualityRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CompanyQualityService> _logger;
        public CompanyQualityService(IEfRepository<CompanyQuality> companyQuyalityRepository,
            IMapper mapper,
            ILogger<CompanyQualityService> logger)
        {
            _companyQualityRepository = companyQuyalityRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task CreateCompanyQualityAsync(CompanyQualityRequest request)
        {
            var companyQuality = _mapper.Map<CompanyQuality>(request);
            await _companyQualityRepository.AddAsync(companyQuality);
            await _companyQualityRepository.SaveChangesAsync();
        }

        public async Task DeleteCompanyQualityAsync(int id)
        {
            var companyQuality = await _companyQualityRepository.GetByIdAsync(id);
            if (companyQuality == null)
            {
                _logger.LogWarning("Could not find existing company quality to delete in - CompanyQuality Service");
            }
            else
            {
                await _companyQualityRepository.DeleteAsync(companyQuality);
                await _companyQualityRepository.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<CompanyQualityResponce>> GetCompanyQualitiesAsync()
        {
            var companyQualities = await _companyQualityRepository.ListAsync();
            return _mapper.Map<IEnumerable<CompanyQualityResponce>>(companyQualities);
        }

        public async Task<CompanyQualityResponce> GetCompanyQualityByIdAsync(int id)
        {
            var companyQuality = await _companyQualityRepository.GetByIdAsync(id);
            return _mapper.Map<CompanyQualityResponce>(companyQuality);
        }

        public async Task UpdateCompanyQualityAsync(int id, CompanyQualityRequest request)
        {
            var companyQuality = await _companyQualityRepository.GetByIdAsync(id);
            if (companyQuality == null)
            {
                _logger.LogWarning("Could not find existing company quality to update in - CompanyQuality Service");
            }
            else
            {
                _mapper.Map(request, companyQuality);
                await _companyQualityRepository.UpdateAsync(companyQuality);
                await _companyQualityRepository.SaveChangesAsync();
            }
        }
    }
}
