using cherrys_construction_mvc.Interfaces;
using cherrys_construction_mvc.Models;
using cherrys_construction_mvc.Utility;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace cherrys_construction_mvc.Areas.User.Controllers
{
    [Area(Breadcrumb.HomeArea)]
    public class AboutController : Controller
    {
        private readonly ICompanyInfoService _companyInfoService;


        public AboutController(ICompanyInfoService companyInfoService)
        {
            _companyInfoService = companyInfoService;
        }

        public async Task<IActionResult> Index()
        {
            var settings = await _companyInfoService.GetCompanyInfosAsync();
            if(settings.Any())
            {
                var info = settings.FirstOrDefault();
                return View(info);
            }
            return View(null);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
