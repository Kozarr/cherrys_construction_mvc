using cherrys_construction_mvc.Interfaces;
using cherrys_construction_mvc.Models;
using cherrys_construction_mvc.Services;
using cherrys_construction_mvc.Utility;
using cherrys_construction_mvc.ViewModels.Contact;
using cherrys_construction_mvc.ViewModels.Project;
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

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ContactViewModel contactViewModel = new();

            var companyInfo = await _companyInfoService.GetCompanyInfosAsync();
            if (companyInfo.Any())
            {
                var info = companyInfo.First();
                contactViewModel.CompanyInfo = info;
            }
            else { }

            return View(contactViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
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
