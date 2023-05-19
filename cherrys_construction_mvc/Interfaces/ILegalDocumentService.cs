using cherrys_construction_mvc.ViewModels.Requests;
using cherrys_construction_mvc.ViewModels.Responce;

namespace cherrys_construction_mvc.Interfaces
{
    public interface ILegalDocumentService
    {
        Task<IEnumerable<LegalDocumentResponce>> GetLegalDocumentsAsync();
        Task<LegalDocumentResponce> GetLegalDocumentByIdAsync(int id);
        Task CreateLegalDocumentAsync(LegalDocumentRequest request);
        Task UpdateLegalDocumentAsync(int id, LegalDocumentRequest request);
        Task DeleteLegalDocumentAsync(int id);

    }
}
