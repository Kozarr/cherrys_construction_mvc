using cherrys_construction_mvc.ViewModels.Requests;
using cherrys_construction_mvc.ViewModels.Responce;

namespace cherrys_construction_mvc.Interfaces
{
    public interface ICompanyCertificateService
    {
        Task<IEnumerable<CompanyCertificateResponce>> GetCertificatesAsync();
        Task<CompanyCertificateResponce> GetCertificateByIdAsync(int companyCertificateId);
        Task CreateCertificateAsync(CompanyCertificateRequest request);
        Task UpdateCertificateAsync(int companyCertificateId, CompanyCertificateRequest request);
        Task DeleteCertificateAsync(int companyCertificateId);
    }
}
