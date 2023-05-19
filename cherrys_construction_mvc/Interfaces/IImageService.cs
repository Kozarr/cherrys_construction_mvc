using cherrys_construction_mvc.ViewModels.Requests;
using cherrys_construction_mvc.ViewModels.Responce;

namespace cherrys_construction_mvc.Interfaces
{
    public interface IImageService
    {
        Task CreateImageAsync(ImageRequest request);
        Task DeleteImageAsync(int imageId);
        Task<ImageResponce> GetByIdImage(int imageId);
        Task<IEnumerable<ImageResponce>> GetByProjectIdImage(int projectId);
    }
}
