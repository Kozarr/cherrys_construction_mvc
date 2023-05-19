using cherrys_construction_mvc.ViewModels.Requests;
using cherrys_construction_mvc.ViewModels.Responce;

namespace cherrys_construction_mvc.Interfaces
{
    public interface ICompanyQualitiySettingService
    {

        Task<IEnumerable<CompanyQualitiySettingResponce>> GetCompanyQualitiySettingsAsync();
        Task<CompanyQualitiySettingResponce> GetCompanyQualitiySettingByIdAsync(int id);
        Task CreateCompanyQualitiySettingAsync(CompanyQualitySettingRequest request);
        Task UpdateCompanyQualitiySettingAsync(int id, CompanyQualitySettingRequest request);
        Task DeleteCompanyQualitiySettingAsync(int id);

    }
}
