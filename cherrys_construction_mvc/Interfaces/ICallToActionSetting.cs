using cherrys_construction_mvc.ViewModels.Requests;
using cherrys_construction_mvc.ViewModels.Responce;

namespace cherrys_construction_mvc.Interfaces
{
    public interface ICallToActionSetting
    {
        Task<IEnumerable<CallToActionSettingResponce>> GetCallToActionSettingsAsync();
        Task<CallToActionSettingResponce> GetCallToActionSettingByIdAsync(int id);
        Task CreateCallToActionSettingAsync(CallToActionSettingRequest request);
        Task UpdateCallToActionSettingAsync(int id, CallToActionSettingRequest request);
        Task DeleteCallToActionSettingAsync(int id);

    }
}
