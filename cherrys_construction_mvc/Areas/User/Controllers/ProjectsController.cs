using cherrys_construction_mvc.Interfaces;
using cherrys_construction_mvc.Models;
using cherrys_construction_mvc.Utility;
using cherrys_construction_mvc.ViewModels.Project;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace cherrys_construction_mvc.Areas.User.Controllers
{
    [Area(Breadcrumb.HomeArea)]
    public class ProjectsController : Controller
    {
        private readonly ILogger<ProjectsController> _logger;
        private readonly IProjectService _projectService;
        private readonly ITagService _tagService;
        private readonly ICompanyInfoService _companyInfoService;
        private readonly IServiceTypeService _serviceTypeService;
        private readonly ITestimonyService _testimonyService;
        public ProjectsController(
            ILogger<ProjectsController> logger,
            IProjectService projectService,
            ITagService tagService,
            ICompanyInfoService companyInfoService,
            IServiceTypeService serviceTypeService,
            ITestimonyService testimonyService)
        {
            _logger = logger;
            _projectService = projectService;
            _tagService = tagService;
            _companyInfoService = companyInfoService;
            _serviceTypeService = serviceTypeService;
            _testimonyService = testimonyService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var projectViewModelResponce = new ProjectViewModel();

            var projects = await _projectService.GetProjectsAsync();
            if(projects.Any())
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
                projectViewModelResponce.Projects = projects;
            }

            var tags = await _tagService.GetTagsAsync();
            if (tags.Any())
            {
                projectViewModelResponce.Tags = tags;
            }

            var companyInfo = await _companyInfoService.GetCompanyInfosAsync();
            if (companyInfo.Any())
            {
                var info = companyInfo.ToList()[0];
                projectViewModelResponce.CompanyInfo = info;
            }
            
            return View(projectViewModelResponce);

        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            ProjectDetailsViewModel projectDetails = new();

            var project = await _projectService.GetProjectByIdAsync(id);
            if(project != null)
            {
                if(project.ServiceTypeId > 0)
                {
                    var serviceType = await _serviceTypeService.GetServiceTypeByIdAsync(project.ServiceTypeId);
                    if(serviceType != null)
                    {
                        project.ServiceType = serviceType;
                    }
                }
                projectDetails.Project = project;
            }

            var companyInfo = await _companyInfoService.GetCompanyInfosAsync();
            if (companyInfo.Any())
            {
                var info = companyInfo.First();
                projectDetails.CompanyInfo = info;
            }

            return View(projectDetails);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
