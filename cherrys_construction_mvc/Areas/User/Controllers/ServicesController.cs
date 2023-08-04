using cherrys_construction_mvc.Interfaces;
using cherrys_construction_mvc.Models;
using cherrys_construction_mvc.Utility;
using cherrys_construction_mvc.ViewModels.Service;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace cherrys_construction_mvc.Areas.User.Controllers
{
    [Area(Breadcrumb.HomeArea)]
    public class ServicesController : Controller
    {
        private readonly ILogger<ServicesController> _logger;
        private readonly ICompanyInfoService _companyInfoService;
        private readonly IServiceService _service;
        public ServicesController(
            ILogger<ServicesController> logger,
            ICompanyInfoService companyInfoService,
            IServiceService service)
        {
            _logger = logger;
            _companyInfoService = companyInfoService;
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var companyInfoList = await _companyInfoService.GetCompanyInfosAsync();
            if (companyInfoList.Any())
            {
                var info = companyInfoList.FirstOrDefault();
                return View(info);
            }
            return View(null);
        }

        public async Task<IActionResult> Details(int id)
        {
            var service = await _service.GetServiceByIdAsync(id);
            if (service == null)
            {
                TempData["error"] = "Service Not Found";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ServiceDetailsViewModel serviceDetails = new();

                var services = await _service.GetServicesAsync();
                if (services.Any())
                {
                    serviceDetails.Services = services;
                }

                var companyInfo = await _companyInfoService.GetCompanyInfosAsync();
                if (companyInfo.Any())
                {
                    var info = companyInfo.FirstOrDefault();
                    serviceDetails.CompanyInfo = info;
                }
                serviceDetails.Service = service;
                return View(serviceDetails);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
