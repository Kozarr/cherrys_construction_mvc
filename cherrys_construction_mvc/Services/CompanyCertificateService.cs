using AutoMapper;
using cherrys_construction_mvc.EfRepository.Interfaces;
using cherrys_construction_mvc.Interfaces;
using cherrys_construction_mvc.Models;
using cherrys_construction_mvc.ViewModels.Requests;
using cherrys_construction_mvc.ViewModels.Responce;

namespace cherrys_construction_mvc.Services
{
    public class CompanyCertificateService : ICompanyCertificateService
    {
        private readonly ILogger<CompanyCertificateService> _logger;
        private readonly IEfRepository<CompanyCertificate> _companyCertificateRepository;
        private readonly IMapper _mapper;
        public CompanyCertificateService(IEfRepository<CompanyCertificate> companyCertificateRepository,
            IMapper mapper,
            ILogger<CompanyCertificateService> logger)
        {
            _companyCertificateRepository = companyCertificateRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task CreateCertificateAsync(CompanyCertificateRequest request)
        {
            var certificate = _mapper.Map<CompanyCertificate>(request);
            await _companyCertificateRepository.AddAsync(certificate);
            await _companyCertificateRepository.SaveChangesAsync();

        }

        public async Task DeleteCertificateAsync(int companyCertificateId)
        {
            var certificate = await _companyCertificateRepository.GetByIdAsync(companyCertificateId);
            if (certificate != null)
            {
                await _companyCertificateRepository.DeleteAsync(certificate);
            }
            else
            {
                _logger.LogWarning("Could not find existing Certificate to delete in - CompanyCertificate Service");
            }
        }

        public async Task<CompanyCertificateResponce> GetCertificateByIdAsync(int companyCertificateId)
        {
            var certificate = await _companyCertificateRepository.GetByIdAsync(companyCertificateId);
            return _mapper.Map<CompanyCertificateResponce>(certificate);
        }

        public async Task<IEnumerable<CompanyCertificateResponce>> GetCertificatesAsync()
        {
            var certificates = await _companyCertificateRepository.ListAsync();
            return _mapper.Map<IEnumerable<CompanyCertificateResponce>>(certificates);
        }

        public async Task UpdateCertificateAsync(int companyCertificateId, CompanyCertificateRequest request)
        {
            var certificate = await _companyCertificateRepository.GetByIdAsync(companyCertificateId);
            if (certificate != null)
            {
                _mapper.Map(request, certificate);
                await _companyCertificateRepository.UpdateAsync(certificate);
                await _companyCertificateRepository.SaveChangesAsync();
            }
            else
            {
                _logger.LogWarning("Could not find existing company certificate in - CompanyCertificate Service");
            }
        }
    }
}
