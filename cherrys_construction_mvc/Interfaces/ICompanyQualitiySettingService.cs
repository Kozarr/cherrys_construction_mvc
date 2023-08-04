using cherrys_construction_mvc.ViewModels.Requests;
using cherrys_construction_mvc.ViewModels.Responce;

namespace cherrys_construction_mvc.Interfaces
{
    public interface ICompanyQualitiySettingService
    {

        Task<IEnumerable<CompanyQualitiySettingResponce>> GetCompanyQualitiesSettingsAsync();
        Task<CompanyQualitiySettingResponce> GetCompanyQualitiesSettingByIdAsync(int id);
        Task CreateCompanyQualitiesSettingAsync(CompanyQualitySettingRequest request);
        Task UpdateCompanyQualitiesSettingAsync(int id, CompanyQualitySettingRequest request);
        Task DeleteCompanyQualitiesSettingAsync(int id);

    }
}
