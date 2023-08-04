using cherrys_construction_mvc.ViewModels.Requests;
using cherrys_construction_mvc.ViewModels.Responce;
namespace cherrys_construction_mvc.Interfaces
{
    public interface IServiceService
    {
        Task<IEnumerable<ServiceResponce>> GetServicesAsync();
        Task<ServiceResponce> GetServiceByIdAsync(int serviceId);
        Task CreateServiceAsync(ServiceRequest request);
        Task UpdateServiceAsync(int serviceId, ServiceRequest request);
        Task DeleteServiceAsync(int serviceId);

    }
}
