using cherrys_construction_mvc.Interfaces;
using cherrys_construction_mvc.ViewModels.Requests;
using cherrys_construction_mvc.ViewModels.Responce;
using AutoMapper;
using cherrys_construction_mvc.EfRepository.Interfaces;
using cherrys_construction_mvc.Models;
using cherrys_construction_mvc.Utility;
using cherrys_construction_mvc.Services.ImageSharp.Interface;

namespace cherrys_construction_mvc.Services
{
    public class CompanyValueService : ICompanyValueService
    {
        private readonly IEfRepository<CompanyValue> _companyValueRepository;
        private readonly IMapper _mapper;
        private IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<CompanyValueService> _logger;
        private readonly IImageProcessorService _imageProcessor;
        public CompanyValueService(
            IEfRepository<CompanyValue> companyValueRepository,
            IMapper mapper,
            IWebHostEnvironment webHostEnvironment,
            ILogger<CompanyValueService> logger,
            IImageProcessorService imageProcessor)
        {
            _companyValueRepository = companyValueRepository;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
            _imageProcessor = imageProcessor;
        }

        public async Task CreateCompanyValueAsync(CompanyValueRequest request)
        {
            if (request.Image != null)
            {
                request.ImageLink = await _imageProcessor.ProcessImageAsync(request.Image, _webHostEnvironment, StaticDetails.StandardImage);
            }
            var companyValue = _mapper.Map<CompanyValue>(request);
            await _companyValueRepository.AddAsync(companyValue);
            await _companyValueRepository.SaveChangesAsync();
        }

        public async Task DeleteCompanyValueAsync(int companyValueId)
        {
            var companyValue = await _companyValueRepository.GetByIdAsync(companyValueId);
            if (companyValue != null)
            {
                if (!string.IsNullOrWhiteSpace(companyValue.ImageLink))
                {
                    _imageProcessor.DeleteImage(_webHostEnvironment.WebRootPath, companyValue.ImageLink);
                }
                await _companyValueRepository.DeleteAsync(companyValue);
                await _companyValueRepository.SaveChangesAsync();
            }
            else
            {
                _logger.LogWarning("Could not find existing company value in CompanyValue Service");
            }
        }

        public async Task<CompanyValueResponce> GetCompanyValueByIdAsync(int companyValueId)
        {
            var companyValue = await _companyValueRepository.GetByIdAsync(companyValueId);
            return _mapper.Map<CompanyValueResponce>(companyValue);
        }

        public async Task<IEnumerable<CompanyValueResponce>> GetCompanyValuesAsync()
        {
            var companyValues = await _companyValueRepository.ListAsync();
            return _mapper.Map<IEnumerable<CompanyValueResponce>>(companyValues);
        }

        public async Task UpdateCompanyValueAsync(int companyValueId, CompanyValueRequest request)
        {
            var companyValue = await _companyValueRepository.GetByIdAsync(companyValueId);
            if (companyValue == null)
            {
                _logger.LogWarning("Could not find existing company value in - CompanyValue Service");
            }
            else
            {
                if (request.Image != null)
                {
                    if (!string.IsNullOrWhiteSpace(companyValue.ImageLink))
                    {
                        _imageProcessor.DeleteImage(_webHostEnvironment.WebRootPath, companyValue.ImageLink);
                    }
                    request.ImageLink = await _imageProcessor.ProcessImageAsync(request.Image, _webHostEnvironment, StaticDetails.StandardImage);
                }
                else
                {
                    request.ImageLink = companyValue.ImageLink;
                }
                _mapper.Map(request, companyValue);
                await _companyValueRepository.UpdateAsync(companyValue);
                await _companyValueRepository.SaveChangesAsync();
            }
        }
    }
}
