using cherrys_construction_mvc.ViewModels.Requests;
using cherrys_construction_mvc.ViewModels.Responce;

namespace cherrys_construction_mvc.Interfaces
{
    public interface IProjectService
    {
        Task<IEnumerable<ProjectResponce>> GetProjectsAsync(bool withSpec = true);
        Task<IEnumerable<ProjectResponce>> GetProjectsWithoutTestimonyAsync();
        Task<ProjectResponce> GetProjectByIdAsync(int projectId);
        Task CreateProjectAsync(ProjectRequest request);
        Task UpdateProjectAsync(int projectId, ProjectRequest request);
        Task DeleteProjectAsync(int projectId);
    }
}
