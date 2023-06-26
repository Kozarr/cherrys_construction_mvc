using cherrys_construction_mvc.Interfaces;
using cherrys_construction_mvc.Models;
using cherrys_construction_mvc.Utility;
using cherrys_construction_mvc.ViewModels.Blog;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace cherrys_construction_mvc.Areas.User.Controllers
{
    [Area(Breadcrumb.HomeArea)]
    public class BlogController : Controller
    {
        private readonly ILogger<BlogController> _logger;
        private readonly ICompanyInfoService _companyInfoService;
        public BlogController(
            ILogger<BlogController> logger,
            ICompanyInfoService companyInfoService
            )
        {
             _logger = logger;  
            _companyInfoService = companyInfoService;
        }

        public BlogViewModel blogViewModel = new();

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var companyInfo = await _companyInfoService.GetCompanyInfosAsync();
            if (companyInfo.Any())
            {
                var info = companyInfo.First();
                blogViewModel.CompanyInfo = info;
            }

            return View(blogViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Details()
        {
            BlogDetailsViewModel blogDetailsVM = new();
            if(blogViewModel != null)
            {
                blogDetailsVM.CompanyInfo = blogViewModel.CompanyInfo;
            }
            return View(blogDetailsVM);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
