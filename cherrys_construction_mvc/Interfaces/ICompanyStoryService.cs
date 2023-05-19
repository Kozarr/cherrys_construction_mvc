using cherrys_construction_mvc.ViewModels.Requests;
using cherrys_construction_mvc.ViewModels.Responce;

namespace cherrys_construction_mvc.Interfaces
{
    public interface ICompanyStoryService
    {
        Task<IEnumerable<CompanyStoryResponce>> GetCompanyStoriesAsync();
        Task<CompanyStoryResponce> GetCompanyStoryByIdAsync(int companyStoryId);
        Task CreateCompanyStoryAsync(CompanyStoryRequest request);
        Task UpdateCompanyStoryAsync(int companyStoryId, CompanyStoryRequest request);
        Task DeleteCompanyStoryAsync(int companyStoryId);
    }
}
