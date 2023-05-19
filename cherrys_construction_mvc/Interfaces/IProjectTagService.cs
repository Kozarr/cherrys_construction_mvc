using cherrys_construction_mvc.ViewModels.Requests;
using cherrys_construction_mvc.ViewModels.Responce;

namespace cherrys_construction_mvc.Interfaces
{
    public interface IProjectTagService
    {
        Task CreateProjectTagAsync(ProjectTagRequest request);
        Task<List<ProjectTagResponce>> GetAllDataByProjectId(int projectId);
        Task DeleteProjectTagAsync(int id, int tagId);
    }
}
