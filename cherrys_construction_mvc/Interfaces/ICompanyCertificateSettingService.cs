using cherrys_construction_mvc.ViewModels.Requests;
using cherrys_construction_mvc.ViewModels.Responce;

namespace cherrys_construction_mvc.Interfaces
{
    public interface ICompanyCertificateSettingService
    {

        Task<IEnumerable<CompanyCertificateSettingResponce>> GetCompanyCertificateSettingsAsync();
        Task<CompanyCertificateSettingResponce> GetCompanyCertificateSettingByIdAsync(int id);
        Task CreateCompanyCertificateSettingAsync(CompanyCertificateSettingRequest request);
        Task UpdateCompanyCertificateSettingAsync(int id, CompanyCertificateSettingRequest request);
        Task DeleteCompanyCertificateSettingAsync(int id);


    }
}
