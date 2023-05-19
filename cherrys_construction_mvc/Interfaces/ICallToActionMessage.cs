using cherrys_construction_mvc.ViewModels.Requests;
using cherrys_construction_mvc.ViewModels.Responce;

namespace cherrys_construction_mvc.Interfaces
{
    public interface ICallToActionMessage
    {
        Task<IEnumerable<CallToActionMessageResponce>> GetCallToActionMessagesAsync();
        Task<CallToActionMessageResponce> GetCallToActionMessageByIdAsync(int id);
        Task CreateCallToActionMessageAsync(CallToActionMessageRequest request);
        Task UpdateCallToActionMessageAsync(int id, CallToActionMessageRequest request);
        Task DeleteCallToActionMessageAsync(int id);
    }
}
