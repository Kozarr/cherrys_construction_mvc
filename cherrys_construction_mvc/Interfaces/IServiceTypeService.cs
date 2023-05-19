using cherrys_construction_mvc.ViewModels.Requests;
using cherrys_construction_mvc.ViewModels.Responce;

namespace cherrys_construction_mvc.Interfaces
{
    public interface IServiceTypeService
    {
        Task<IEnumerable<ServiceTypeResponce>> GetServiceTypesAsync();
        Task<ServiceTypeResponce> GetServiceTypeByIdAsync(int serviceTypeId);
        Task CreateServiceTypeAsync(ServiceTypeRequest request);
        Task UpdateServiceTypeAsync(int serviceTypeId, ServiceTypeRequest request);
        Task DeleteServiceTypeAsync(int serviceTypeId);
    }
}
