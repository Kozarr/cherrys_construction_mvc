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
        public ProjectsController(IProjectService projectService, 
            IServiceTypeService serviceTypeService, 
            ITagService tagService, 
            IProjectTagService projectTagService, 
            ILogger<ProjectsController> logger)
        {
            _projectService = projectService;
            _serviceTypeService = serviceTypeService;
            _tagService = tagService;
            _projectTagService = projectTagService;
            _logger = logger;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var projects = await _projectService.GetProjectsAsync();
            return View(projects);
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
            if (selectedTagsIds.Count() > 0)
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
            IEnumerable<int> SelectedTagIds = project.ProjectTags.Select(i => i.TagId);

            var tagsData = await _tagService.GetTagsAsync();


            List<TagItem> tagListItems = new List<TagItem>();

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

            var editRequest = new ProjectRequest()
            {
                Title = project.Title,
                ServiceTypeId = project.ServiceTypeId,
                ServiceTypes = (List<ServiceTypeResponce>)await _serviceTypeService.GetServiceTypesAsync(),
                ClientName = project.ClientName,
                Description = project.Description,
                ProjectEndDate = project.ProjectEndDate,
                ProjectStartDate = project.ProjectStartDate,
                Tags = tagListItems,
                Images = project.Images,
                
            };
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

                List<int> TagIds = new List<int>();
                List<int> SelectedDeletePhotoIds = new List<int>();
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
            if(id != 0)
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
