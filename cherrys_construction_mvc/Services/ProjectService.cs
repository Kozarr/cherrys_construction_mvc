using AutoMapper;
using cherrys_construction_mvc.EfRepository.Interfaces;
using cherrys_construction_mvc.Interfaces;
using cherrys_construction_mvc.Models;
using cherrys_construction_mvc.Specification.ProjectSpec;
using cherrys_construction_mvc.Utility;
using cherrys_construction_mvc.ViewModels.Requests;
using cherrys_construction_mvc.ViewModels.Responce;

namespace cherrys_construction_mvc.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IEfRepository<Project> _projectRepository;
        private readonly IEfRepository<ImageModel> _imageRepository;
        private readonly IImageService _imageService;
        private readonly IProjectTagService _projectTagService;
        private readonly IMapper _mapper;
        private readonly ILogger<ProjectService> _logger;
        public ProjectService(IEfRepository<Project> projectRepository,
            IEfRepository<ImageModel> imageRepository,
            IMapper mapper,
            IImageService imageService,
            IWebHostEnvironment webHostEnvironment,
            IProjectTagService projectTagService,
            ILogger<ProjectService> logger)
        {
            _projectRepository = projectRepository;
            _imageRepository = imageRepository;
            _mapper = mapper;
            _imageService = imageService;
            _webHostEnvironment = webHostEnvironment;
            _projectTagService = projectTagService;
            _logger = logger;
        }

        public async Task CreateProjectAsync(ProjectRequest request)
        {
            var project = _mapper.Map<Project>(request);

            await _projectRepository.AddAsync(project);
            await _projectRepository.SaveChangesAsync();

            if (request.TagIds.Any())
            {
                foreach (var tagId in request.TagIds)
                {
                    var projectTagRequest = new ProjectTagRequest()
                    {
                        ProjectId = project.Id,
                        TagId = tagId
                    };

                    await _projectTagService.CreateProjectTagAsync(projectTagRequest);
                }
            }


            if (request.Files.Any())
            {
                foreach (var item in request.Files)
                {
                    var imageRequest = new ImageRequest()
                    {
                        PathImage = Helper.Helper.UploadImage(item, _webHostEnvironment, StaticDetails.StandardImage).Result,
                        ProjectId = project.Id
                    };
                    await _imageService.CreateImageAsync(imageRequest);
                }
            }
        }

        public async Task DeleteProjectAsync(int projectId)
        {
            var project = await _projectRepository.GetByIdAsync(projectId);
            if (project != null)
            {
                var imagesForDelete = await _imageService.GetByProjectIdImage(projectId);
                if (imagesForDelete.Any())
                {
                    await _projectRepository.DeleteAsync(project);
                    await _projectRepository.SaveChangesAsync();
                    foreach (var image in imagesForDelete)
                    {
                        var fullPathForDelete = _webHostEnvironment.WebRootPath + image.PathImage;
                        Helper.Helper.DeleteImage(fullPathForDelete);
                    }
                }
            }
            else
            {
                _logger.LogWarning("Could not find existing project in - Project Service");
            }

        }

        public async Task<ProjectResponce> GetProjectByIdAsync(int projectId)
        {
            var spec = new ProjectIncludeFullInfoSpecification(projectId);

            var project = await _projectRepository.GetBySpecAsync(spec);

            return _mapper.Map<ProjectResponce>(project);
        }

        public async Task<IEnumerable<ProjectResponce>> GetProjectsAsync(bool withSpec = true)
        {
            if (withSpec)
            {
                var spec = new ProjectIncludeFullInfoSpecification();
                var projects = await _projectRepository.ListAsync(spec);
                return _mapper.Map<IEnumerable<ProjectResponce>>(projects);
            }
            else
            {
                var projects = await _projectRepository.ListAsync();
                return _mapper.Map<IEnumerable<ProjectResponce>>(projects);
            }

        }

        public async Task<IEnumerable<ProjectResponce>> GetProjectsWithoutTestimonyAsync()
        {
            var spec = new ProjectsWithoutTestimony();
            var projects = await _projectRepository.ListAsync(spec);
            return _mapper.Map<IEnumerable<ProjectResponce>>(projects);
        }

        public async Task UpdateProjectAsync(int projectId, ProjectRequest request)
        {
            var project = await _projectRepository.GetByIdAsync(projectId);
            if (project == null)
            {
                _logger.LogWarning("Could not find existing project in - Project Service");
            }
            else
            {
                _mapper.Map(request, project);
                await _projectRepository.UpdateAsync(project);
                await _projectRepository.SaveChangesAsync();

                var tags = await _projectTagService.GetAllDataByProjectId(projectId);
                if (tags.Any())
                {
                    foreach (var tagId in request.TagIds)
                    {
                        if (!tags.Where(a => a.TagId == tagId).Any())
                        {
                            var projectTagRequest = new ProjectTagRequest()
                            {
                                ProjectId = projectId,
                                TagId = tagId
                            };
                            await _projectTagService.CreateProjectTagAsync(projectTagRequest);
                            //var item = tags.SingleOrDefault(a => a.TagId == tagId);
                            tags.Remove(tags.SingleOrDefault(a => a.TagId == tagId));
                        }
                        else
                        {
                            tags.Remove(tags.SingleOrDefault(a => a.TagId == tagId));
                        }
                    }

                    foreach (var tag in tags)
                    {
                        await _projectTagService.DeleteProjectTagAsync(tag.ProjectId, tag.TagId);
                    }

                    if (request.SelectedDeletePhoto != null)
                    {
                        if (request.SelectedDeletePhoto.Any())
                        {
                            foreach (var id in request.SelectedDeletePhoto)
                            {
                                var imageForDelete = await _imageRepository.GetByIdAsync(id);
                                if (imageForDelete == null)
                                {
                                    _logger.LogWarning("Could not find existing image to delete in - Project Service");
                                }
                                else
                                {
                                    var fullPathForDelete = _webHostEnvironment.WebRootPath + imageForDelete.PathImage;
                                    Helper.Helper.DeleteImage(fullPathForDelete);
                                    await _imageService.DeleteImageAsync(id);
                                }
                            }
                        }
                    }

                    if (request.Files != null)
                    {
                        foreach (var item in request.Files)
                        {
                            var imageRequest = new ImageRequest()
                            {
                                PathImage = Helper.Helper.UploadImage(item, _webHostEnvironment, StaticDetails.StandardImage).Result,
                                ProjectId = projectId
                            };
                            await _imageService.CreateImageAsync(imageRequest);
                        }
                    }
                }
                else
                {
                    _logger.LogWarning("Could not find existing tag in - Project Service");
                }
            }
        }
    }
}
