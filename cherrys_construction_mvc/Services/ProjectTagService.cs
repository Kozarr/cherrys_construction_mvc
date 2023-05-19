using AutoMapper;
using cherrys_construction_mvc.EfRepository.Interfaces;
using cherrys_construction_mvc.Interfaces;
using cherrys_construction_mvc.Models;
using cherrys_construction_mvc.Specification.ProjectSpec;
using cherrys_construction_mvc.ViewModels.Requests;
using cherrys_construction_mvc.ViewModels.Responce;

namespace cherrys_construction_mvc.Services
{
    public class ProjectTagService : IProjectTagService
    {
        private readonly IEfRepository<ProjectTag> _projectTagRepository;
        private readonly IMapper _mapper;
        private ILogger<ProjectTagService> _logger;
        public ProjectTagService(IEfRepository<ProjectTag> projectTagRepository,
            IMapper mapper,
            ILogger<ProjectTagService> logger)
        {
            _mapper = mapper;
            _projectTagRepository = projectTagRepository;
            _logger = logger;
        }
        public async Task CreateProjectTagAsync(ProjectTagRequest request)
        {
            var projectTag = _mapper.Map<ProjectTag>(request);
            await _projectTagRepository.AddAsync(projectTag);
            await _projectTagRepository.SaveChangesAsync();
        }

        public async Task DeleteProjectTagAsync(int projectId,int tagId)
        {
            var spec = new GetProjectTagDataByProjectId(projectId,tagId);
            if(spec != null)
            {
                var item = await _projectTagRepository.GetBySpecAsync(spec);
                if(item != null)
                {
                    await _projectTagRepository.DeleteAsync(item);
                    await _projectTagRepository.SaveChangesAsync();
                }
                else
                {
                    _logger.LogError("Could not find project tag to delete in returned specification in - ProjectTag Service");
                }             
            }
            else
            {
                _logger.LogWarning("Could get specification for project and tag ids - ProjectTag Service");
            }
            
        }

        public async Task<List<ProjectTagResponce>> GetAllDataByProjectId(int projectId)
        {
            var spec = new GetProjectTagDataByProjectId(projectId);
            var data = await _projectTagRepository.ListAsync(spec);
            return _mapper.Map<List<ProjectTagResponce>>(data);

        }
    }
}
