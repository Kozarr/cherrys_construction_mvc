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
    public class ServiceTypeService : IServiceTypeService
    {
        private readonly ILogger<ServiceTypeService> _logger;
        private readonly IEfRepository<ServiceType> _serviceTypeRepository;
        private readonly IEfRepository<Project> _projectRepository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IImageProcessorService _imageProcessor;
        public ServiceTypeService(IEfRepository<ServiceType> serviceTypeRepository,
            IEfRepository<Project> projectRepository,
            IMapper mapper,
            IWebHostEnvironment webHostEnvironment,
            ILogger<ServiceTypeService> logger,
            IImageProcessorService imageProcessor)
        {
            _serviceTypeRepository = serviceTypeRepository;
            _projectRepository = projectRepository;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
            _imageProcessor = imageProcessor;
        }
        public async Task CreateServiceTypeAsync(ServiceTypeRequest request)
        {
            if (request.Image != null)
            {
                request.ImageLink = await _imageProcessor.ProcessImageAsync(request.Image, _webHostEnvironment, StaticDetails.StandardImage);
            }
            var serviceType = _mapper.Map<ServiceType>(request);
            await _serviceTypeRepository.AddAsync(serviceType);
            await _serviceTypeRepository.SaveChangesAsync();
        }

        public async Task DeleteServiceTypeAsync(int serviceTypeId)
        {
            var serviceType = await _serviceTypeRepository.GetByIdAsync(serviceTypeId);

            if (serviceType != null)
            {
                if (!string.IsNullOrWhiteSpace(serviceType.ImageLink))
                {
                    _imageProcessor.DeleteImage(_webHostEnvironment.WebRootPath, serviceType.ImageLink);
                }
                await _serviceTypeRepository.DeleteAsync(serviceType);
                await _serviceTypeRepository.SaveChangesAsync();

            }
            else
            {
                _logger.LogWarning("Could not find existing service type in - ServiceType Service");
            }

        }

        public async Task<ServiceTypeResponce> GetServiceTypeByIdAsync(int serviceTypeId)
        {
            var serviceType = await _serviceTypeRepository.GetByIdAsync(serviceTypeId);
            return _mapper.Map<ServiceTypeResponce>(serviceType);
        }

        public async Task<IEnumerable<ServiceTypeResponce>> GetServiceTypesAsync()
        {
            var serviceTypes = await _serviceTypeRepository.ListAsync();
            return _mapper.Map<IEnumerable<ServiceTypeResponce>>(serviceTypes);
        }

        public async Task UpdateServiceTypeAsync(int serviceTypeId, ServiceTypeRequest request)
        {
            var serviceType = await _serviceTypeRepository.GetByIdAsync(serviceTypeId);
            if (serviceType != null)
            {
                if (request.Image != null)
                {
                    if (!string.IsNullOrWhiteSpace(serviceType.ImageLink))
                    {
                        _imageProcessor.DeleteImage(_webHostEnvironment.WebRootPath, serviceType.ImageLink);
                    }
                    request.ImageLink = await _imageProcessor.ProcessImageAsync(request.Image, _webHostEnvironment, StaticDetails.StandardImage);
                }
                else
                {
                    request.ImageLink = serviceType.ImageLink;
                }
                _mapper.Map(request, serviceType);
                await _serviceTypeRepository.UpdateAsync(serviceType);
                await _serviceTypeRepository.SaveChangesAsync();
            }
            else
            {
                _logger.LogWarning("Could not find existing service type in - ServiceType Service");
            }
        }
    }
}
