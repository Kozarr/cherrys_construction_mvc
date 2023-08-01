using cherrys_construction_mvc.ViewModels.Requests;
using cherrys_construction_mvc.ViewModels.Responce;

namespace cherrys_construction_mvc.Interfaces
{
    public interface ITestimonyService
    {
        Task<IEnumerable<TestimonyResponce>> GetTestimoniesAsync();
        Task<TestimonyResponce> GetTestimonyByIdAsync(int testimonyId);
        Task CreateTestimonyAsync(TestimonyRequest request);
        Task UpdateTestimonyAsync(int testimonyId, TestimonyRequest request);
        Task DeleteTestimonyAsync(int testimonyId);
    }
}
