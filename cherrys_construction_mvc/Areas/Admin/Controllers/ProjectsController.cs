using cherrys_construction_mvc.Interfaces;
using cherrys_construction_mvc.Models;
using cherrys_construction_mvc.Utility;
using cherrys_construction_mvc.ViewModels.Requests;
using cherrys_construction_mvc.ViewModels.Responce;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace cherrys_construction_mvc.Areas.Admin.Controllers
{
    [Area(Breadcrumb.AdminArea)]
    [Authorize(Roles = StaticDetails.Role_Admin + "," + StaticDetails.Role_Employee)]
    public class ProjectsController : Controller
    {
        private readonly IProjectService _projectService;
        private readonly IServiceTypeService _serviceTypeService;
        private readonly ITagService _tagService;
        private readonly IProjectTagService _projectTagService;
        private readonly ILogger<ProjectsController> _logger;
        private readonly ITestimonyService _testimonyService;
        public ProjectsController(IProjectService projectService, 
            IServiceTypeService serviceTypeService, 
            ITagService tagService, 
            IProjectTagService projectTagService, 
            ILogger<ProjectsController> logger,
            ITestimonyService testimonyService)
        {
            _projectService = projectService;
            _serviceTypeService = serviceTypeService;
            _tagService = tagService;
            _projectTagService = projectTagService;
            _logger = logger;
            _testimonyService = testimonyService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var projects = await _projectService.GetProjectsAsync();
            if (projects.Any())
            {
                foreach(var item in projects)
                {
                    if(item.ServiceTypeId > 0)
                    {
                        var serviceType = await _serviceTypeService.GetServiceTypeByIdAsync(item.ServiceTypeId);
                        if(serviceType != null)
                        {
                            item.ServiceType = serviceType;
                        }
                    }                      
                }
                return View(projects);
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            // list of tags pulled from DB
            var tagsData = await _tagService.GetTagsAsync();
            List<TagItem> tags = new List<TagItem>();
            foreach (var tag in tagsData)
            {
                var item = new TagItem()
                {
                    Value = tag.Id.ToString(),
                    Text = tag.Name
                };
                tags.Add(item);
            }
            if (tags.Any())
            {
                ProjectRequest projectRequest = new()
                {
                    ServiceTypes = (List<ServiceTypeResponce>)await _serviceTypeService.GetServiceTypesAsync(),
                    Tags = tags,
                };
                return View(projectRequest);
            }
            else
            {
                TempData["error"] = "Please Add Tags";
                return RedirectToAction(nameof(Index), Breadcrumb.ProjectsController, new {area = Breadcrumb.AdminArea});
            }
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] ProjectRequest request, IFormCollection collection)
        {  
            var selectedTagsIds = collection["skill"].ToList();
            List<int> TagIds = new List<int>();
            if (selectedTagsIds.Any())
            {
                foreach (var item in selectedTagsIds)
                {
                    if(item == null)
                    {
                        _logger.LogWarning("item not found in selected tags passed in Projects Controller");
                    }
                    else
                    {
                        TagIds.Add(int.Parse(item));
                    }
                    
                }
                request.TagIds = TagIds;
            }
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrWhiteSpace(request.Title))
                {
                    request.Title = request.Title.Trim();
                }
                if (!string.IsNullOrWhiteSpace(request.Description))
                {
                    request.Description = request.Description.Trim();
                }
                if (!string.IsNullOrWhiteSpace(request.ClientName))
                {
                    request.ClientName = request.ClientName.Trim();
                }
                await _projectService.CreateProjectAsync(request);
                TempData["success"] = "Project Added Successfully";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["error"] = "Project Failed To Add";
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var project = await _projectService.GetProjectByIdAsync(id);
            var infoIds = await _projectTagService.GetAllDataByProjectId(id);
            ProjectRequest editRequest = new();
            if (project != null)
            {
                // Possibly remove code below
                if (project.ProjectTags != null)
                {
                    if (project.ProjectTags.Any())
                    {
                        IEnumerable<int> SelectedTagIds = project.ProjectTags.Select(i => i.TagId);
                    }
                }
                // remove code above if funcationality is not affected
                editRequest.Title = project.Title;
                editRequest.ServiceTypeId = project.ServiceTypeId;
                editRequest.ClientName = project.ClientName;
                editRequest.Description = project.Description;
                editRequest.ProjectEndDate = project.ProjectEndDate;
                editRequest.ProjectStartDate = project.ProjectStartDate;
                if(project.Images != null)
                {
                    if (project.Images.Any())
                    {
                        editRequest.Images = project.Images;
                    }
                }
            }
            var tagsData = await _tagService.GetTagsAsync();

            List<TagItem> tagListItems = new();

            if (tagsData.Any())
            {
                foreach (var tag in tagsData)
                {
                    var item = new TagItem()
                    {
                        Value = tag.Id.ToString(),
                        Text = tag.Name
                    };
                    if (infoIds.Select(a => a.TagId).ToList().Contains(tag.Id))
                    {
                        item.Selected = true;
                    }
                    tagListItems.Add(item);
                }
            }
            var serviceTypes = await _serviceTypeService.GetServiceTypesAsync();
            editRequest.ServiceTypes = serviceTypes.ToList();
            editRequest.Tags = tagListItems;                      
            return View(editRequest);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProjectRequest request, int id, IFormCollection collection)
        {
            if(ModelState.IsValid)
            { 
                var selectedDeletePhoto = collection["checkPhoto"].ToList();
                var selectedTagsIds = collection["skill"].ToList();

                List<int> TagIds = new();
                List<int> SelectedDeletePhotoIds = new();
                if (selectedTagsIds.Any())
                {
                    foreach (var item in selectedTagsIds)
                    {
                        if (item != null)
                        {
                            TagIds.Add(int.Parse(item));
                        }
                        else
                        {
                            _logger.LogError("Could not find a tag in already found list of tags in - Project Controller");
                        }
                    }
                    request.TagIds = TagIds;
                }
                if (selectedDeletePhoto.Any())
                {
                    foreach (var item in selectedDeletePhoto)
                    {
                        if (item != null)
                        {
                            SelectedDeletePhotoIds.Add(int.Parse(item));
                        }
                        else
                        {
                            _logger.LogError("Could not find a photo in already selected list of photos to delete in - Project Controller");
                        }
                    }
                    request.SelectedDeletePhoto = SelectedDeletePhotoIds;
                }
                if (!string.IsNullOrWhiteSpace(request.Title))
                {
                    request.Title = request.Title.Trim();
                }
                if (!string.IsNullOrWhiteSpace(request.Description))
                {
                    request.Description = request.Description.Trim();
                }
                if (!string.IsNullOrWhiteSpace(request.ClientName))
                {
                    request.ClientName = request.ClientName.Trim();
                }
                await _projectService.UpdateProjectAsync(id, request);
                TempData["success"] = "Project Updated Successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["error"] = "Project Failed To Update";
                return View();
            }
        }

        // GET : DELETE
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var project = await _projectService.GetProjectByIdAsync(id);
            return View(project);
        }

        // POST : DELETE
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteProject(int id)
        {
            if(id > 0)
            {              
                await _projectService.DeleteProjectAsync(id);
                TempData["success"] = "Project Deleted Successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["error"] = "Project Failed To Delete";
                return View();
            }
        }


    }
}
