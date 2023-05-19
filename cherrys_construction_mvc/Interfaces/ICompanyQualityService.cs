using cherrys_construction_mvc.ViewModels.Responce;
using cherrys_construction_mvc.ViewModels.Requests;

namespace cherrys_construction_mvc.Interfaces
{
    public interface ICompanyQualityService
    {
        Task<IEnumerable<CompanyQualityResponce>> GetCompanyQualitiesAsync();
        Task<CompanyQualityResponce> GetCompanyQualityByIdAsync(int id);
        Task CreateCompanyQualityAsync(CompanyQualityRequest request);
        Task UpdateCompanyQualityAsync(int id, CompanyQualityRequest request);
        Task DeleteCompanyQualityAsync(int id);
    }
}
