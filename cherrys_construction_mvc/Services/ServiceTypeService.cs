using AutoMapper;
using cherrys_construction_mvc.EfRepository.Interfaces;
using cherrys_construction_mvc.Interfaces;
using cherrys_construction_mvc.Models;
using cherrys_construction_mvc.Utility;
using cherrys_construction_mvc.ViewModels.Requests;
using cherrys_construction_mvc.ViewModels.Responce;
using Microsoft.IdentityModel.Logging;

namespace cherrys_construction_mvc.Services
{
    public class ServiceTypeService : IServiceTypeService
    {
        private readonly ILogger<ServiceTypeService> _logger;
        private readonly IEfRepository<ServiceType> _serviceTypeRepository;
        private readonly IEfRepository<Project> _projectRepository;
        private readonly IMapper _mapper;
        private IWebHostEnvironment _webHostEnvironment;

        public ServiceTypeService(IEfRepository<ServiceType> serviceTypeRepository, 
            IEfRepository<Project> projectRepository,
            IMapper mapper, 
            IWebHostEnvironment webHostEnvironment,
            ILogger<ServiceTypeService> logger)
        {
             _serviceTypeRepository = serviceTypeRepository;
            _projectRepository = projectRepository;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
        }
        public async Task CreateServiceTypeAsync(ServiceTypeRequest request)
        {
            if (request.Image != null)
            {
                request.ImageLink = await Helper.Helper.UploadImage(request.Image, _webHostEnvironment, StaticDetails.StandardImage);
            }
            var serviceType = _mapper.Map<ServiceType>(request);
            await _serviceTypeRepository.AddAsync(serviceType);
            await _serviceTypeRepository.SaveChangesAsync();
        }

        public async Task DeleteServiceTypeAsync(int serviceTypeId)
        {
            var serviceType = await _serviceTypeRepository.GetByIdAsync(serviceTypeId);

            if(serviceType != null)
            {
               // When deleting a ServiceType, we remove connections to service type in all projects
                var projectsList = await _projectRepository.ListAsync();
                // oooooooooooooooooooooooooooooooooo
                foreach (var item in projectsList)
                {
                    if (item.ServiceTypeId == serviceTypeId)
                    {
                        item.ServiceType = null;
                        await _projectRepository.UpdateAsync(item);
                        await _projectRepository.SaveChangesAsync();
                    }
                }

                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if(serviceType.ImageLink != null)
                {
                    var oldImagePath = Path.Combine(wwwRootPath, serviceType.ImageLink.TrimStart('\\'));
                    if (File.Exists(oldImagePath))
                    {
                        File.Delete(oldImagePath);
                    }                  
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
                    string wwwRootPath = _webHostEnvironment.WebRootPath;
                    var oldImagePath = Path.Combine(wwwRootPath, serviceType.ImageLink.TrimStart('\\'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                    request.ImageLink = await Helper.Helper.UploadImage(request.Image, _webHostEnvironment, StaticDetails.StandardImage);
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
