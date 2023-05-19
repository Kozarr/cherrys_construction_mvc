using cherrys_construction_mvc.ViewModels.Requests;
using cherrys_construction_mvc.ViewModels.Responce;

namespace cherrys_construction_mvc.Interfaces
{
    public interface ICompanyValueService
    {
        Task<IEnumerable<CompanyValueResponce>> GetCompanyValuesAsync();
        Task<CompanyValueResponce> GetCompanyValueByIdAsync(int companyValueId);
        Task CreateCompanyValueAsync(CompanyValueRequest request);
        Task UpdateCompanyValueAsync(int companyValueId, CompanyValueRequest request);
        Task DeleteCompanyValueAsync(int companyValueId);
    }
}
