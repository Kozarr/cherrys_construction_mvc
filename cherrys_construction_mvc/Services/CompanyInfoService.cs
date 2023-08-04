using AutoMapper;
using cherrys_construction_mvc.EfRepository.Interfaces;
using cherrys_construction_mvc.Interfaces;
using cherrys_construction_mvc.Models;
using cherrys_construction_mvc.ViewModels.Requests;
using cherrys_construction_mvc.ViewModels.Responce;

namespace cherrys_construction_mvc.Services
{
    public class CompanyInfoService : ICompanyInfoService
    {
        private readonly ILogger<CompanyInfoService> _logger;
        private readonly IEfRepository<CompanyInfo> _companyInfoRepository;
        private readonly IMapper _mapper;
        public CompanyInfoService(IEfRepository<CompanyInfo> companyInfoRepository,
            IMapper mapper,
            ILogger<CompanyInfoService> logger)
        {
            _companyInfoRepository = companyInfoRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task CreateCompanyInfoAsync(CompanyInfoRequest request)
        {
            var compInfo = _mapper.Map<CompanyInfo>(request);
            await _companyInfoRepository.AddAsync(compInfo);
            await _companyInfoRepository.SaveChangesAsync();
        }

        public async Task DeleteCompanyInfoAsync(int companyInfoId)
        {
            var compInfo = await _companyInfoRepository.GetByIdAsync(companyInfoId);
            if (compInfo != null)
            {
                await _companyInfoRepository.DeleteAsync(compInfo);
                await _companyInfoRepository.SaveChangesAsync();
            }
            else
            {
                _logger.LogWarning("Could not find existing company info to delete in - CompanyInfo Service");
            }
        }

        public async Task<CompanyInfoResponce> GetCompanyInfoByIdAsync(int companyInfoId)
        {
            var compInfo = await _companyInfoRepository.GetByIdAsync(companyInfoId);
            return _mapper.Map<CompanyInfoResponce>(compInfo);
        }

        public async Task<IEnumerable<CompanyInfoResponce>> GetCompanyInfosAsync()
        {
            var companyInfoList = await _companyInfoRepository.ListAsync();
            return _mapper.Map<IEnumerable<CompanyInfoResponce>>(companyInfoList);
        }

        public async Task UpdateCompanyInfoAsync(int oldCompanyInfoId, CompanyInfoRequest request)
        {
            var oldCompanyInfo = await _companyInfoRepository.GetByIdAsync(oldCompanyInfoId);
            if (oldCompanyInfo != null)
            {     
                _mapper.Map(request, oldCompanyInfo);
                await _companyInfoRepository.UpdateAsync(oldCompanyInfo);
                await _companyInfoRepository.SaveChangesAsync();
            }
            else
            {
                _logger.LogWarning("Could not find existing company info to update in CompanyInfo Service");
            }
        }
    }
}
