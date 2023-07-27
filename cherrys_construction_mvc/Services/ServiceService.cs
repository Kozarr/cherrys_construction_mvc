using AutoMapper;
using cherrys_construction_mvc.EfRepository.Interfaces;
using cherrys_construction_mvc.Interfaces;
using cherrys_construction_mvc.Models;
using cherrys_construction_mvc.Utility;
using cherrys_construction_mvc.ViewModels.Requests;
using cherrys_construction_mvc.ViewModels.Responce;

namespace cherrys_construction_mvc.Services
{
    public class ServiceService : IServiceService
    {
        private readonly IEfRepository<Service> _serviceRepository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<ServiceService> _logger;
        public ServiceService(IEfRepository<Service> serviceRepository,
            IMapper mapper,
            IWebHostEnvironment webHostEnvironment,
            ILogger<ServiceService> logger)
        {
            _serviceRepository = serviceRepository;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
        }

        public async Task CreateServiceAsync(ServiceRequest request)
        {
            if (request.Image != null)
            {
                request.ImageLink = await Helper.Helper.UploadImage(request.Image, _webHostEnvironment, StaticDetails.StandardImage);
            }
            var service = _mapper.Map<Service>(request);
            await _serviceRepository.AddAsync(service);
            await _serviceRepository.SaveChangesAsync();
        }

        public async Task DeleteServiceAsync(int serviceId)
        {
            var service = await _serviceRepository.GetByIdAsync(serviceId);
            if (service != null)
            {
                if (!string.IsNullOrWhiteSpace(service.ImageLink))
                {
                    string wwwRootPath = _webHostEnvironment.WebRootPath;
                    var oldImagePath = Path.Combine(wwwRootPath, service.ImageLink.TrimStart('\\'));
                    if (oldImagePath != null)
                    {
                        if (File.Exists(oldImagePath))
                        {
                            File.Delete(oldImagePath);
                        }
                    }
                }
                await _serviceRepository.DeleteAsync(service);
                await _serviceRepository.SaveChangesAsync();
            }
            else
            {
                _logger.LogWarning("Could not existing service in - Service Service");
            }
        }

        public async Task<ServiceResponce> GetServiceByIdAsync(int serviceId)
        {
            var service = await _serviceRepository.GetByIdAsync(serviceId);
            return _mapper.Map<ServiceResponce>(service);
        }

        public async Task<IEnumerable<ServiceResponce>> GetServicessAsync()
        {
            var services = await _serviceRepository.ListAsync();
            return _mapper.Map<IEnumerable<ServiceResponce>>(services);
        }

        public async Task UpdateServiceAsync(int serviceId, ServiceRequest request)
        {
            var service = await _serviceRepository.GetByIdAsync(serviceId);
            if (service == null)
            {
                _logger.LogWarning("Could not find existing service in - Service Serivce");
            }
            else
            {
                if (request.Image != null)
                {
                    if (!string.IsNullOrWhiteSpace(service.ImageLink))
                    {
                        string wwwRootPath = _webHostEnvironment.WebRootPath;
                        var oldImagePath = Path.Combine(wwwRootPath, service.ImageLink.TrimStart('\\'));
                        if (File.Exists(oldImagePath))
                        {
                            File.Delete(oldImagePath);
                        }
                    }
                    request.ImageLink = await Helper.Helper.UploadImage(request.Image, _webHostEnvironment, StaticDetails.StandardImage);
                }
                else
                {
                    request.ImageLink = service.ImageLink;
                }
                _mapper.Map(request, service);
                service.Id = serviceId;
                await _serviceRepository.UpdateAsync(service);
                await _serviceRepository.SaveChangesAsync();
            }

        }
    }
}
