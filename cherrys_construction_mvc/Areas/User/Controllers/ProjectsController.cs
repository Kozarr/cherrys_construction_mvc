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
        public ProjectsController(
            ILogger<ProjectsController> logger,
            IProjectService projectService,
            ITagService tagService,
            ICompanyInfoService companyInfoService)
        {
            _logger = logger;
            _projectService = projectService;
            _tagService = tagService;
            _companyInfoService = companyInfoService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var projectViewModelResponce = new ProjectViewModel();

            var projects = await _projectService.GetProjectsAsync();
            if(projects.Any())
            {
                projectViewModelResponce.Projects = projects;
            }
            else { }

            var tags = await _tagService.GetTagsAsync();
            if (tags.Any())
            {
                projectViewModelResponce.Tags = tags;
            }
            else { }

            var companyInfo = await _companyInfoService.GetCompanyInfosAsync();
            if (companyInfo.Any())
            {
                var info = companyInfo.ToList()[0];
                projectViewModelResponce.CompanyInfo = info;
            }
            else { }
            
            return View(projectViewModelResponce);

        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            ProjectDetailsViewModel projectDetails = new();

            var project = await _projectService.GetProjectByIdAsync(id);
            if(project != null)
            {
                projectDetails.Project = project;
            }
            else { }

            var companyInfo = await _companyInfoService.GetCompanyInfosAsync();
            if (companyInfo.Any())
            {
                var info = companyInfo.First();
                projectDetails.CompanyInfo = info;
            }
            else { }

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
