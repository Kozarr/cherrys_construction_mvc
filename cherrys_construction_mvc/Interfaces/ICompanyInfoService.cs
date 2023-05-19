using cherrys_construction_mvc.ViewModels.Requests;
using cherrys_construction_mvc.ViewModels.Responce;

namespace cherrys_construction_mvc.Interfaces
{
    public interface ICompanyInfoService
    {
        Task<IEnumerable<CompanyInfoResponce>> GetCompanyInfosAsync();
        Task<CompanyInfoResponce> GetCompanyInfoByIdAsync(int companyInfoId);
        Task CreateCompanyInfoAsync(CompanyInfoRequest request);
        Task UpdateCompanyInfoAsync(int companyInfoId, CompanyInfoRequest request);
        Task DeleteCompanyInfoAsync(int companyInfoId);
    }
}
