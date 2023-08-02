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
        private readonly IServiceTypeService _serviceTypeService;
        private readonly IServiceService _serviceService;
        private readonly ICompanyInfoService _companyInfoService;
        public ServicesController(
            ILogger<ServicesController> logger,        
            IServiceTypeService serviceTypeService,
            IServiceService serviceService,
            ICompanyInfoService companyInfoService)
        {           
            _logger = logger;    
            _serviceTypeService = serviceTypeService;
            _serviceService = serviceService;          
            _companyInfoService = companyInfoService;
        }
        public async Task<IActionResult> Index()
        {
            ServiceViewModel serviceResponce = new();

            var services = await _serviceService.GetServicessAsync();
            if(services.Any())
            {
                serviceResponce.Services = services;
            }

            var serviceTypes = await _serviceTypeService.GetServiceTypesAsync();
            if (serviceTypes.Any())
            {
                serviceResponce.ServiceTypes = serviceTypes;
            }

            var companyInfo = await _companyInfoService.GetCompanyInfosAsync();
            if (companyInfo.Any())
            {
                var info = companyInfo.ToList().FirstOrDefault();
                serviceResponce.CompanyInfo = info;
            }

            return View(serviceResponce);
        }

        public async Task<IActionResult> Details(int id)
        {
            var service = await _serviceService.GetServiceByIdAsync(id);
            if (service == null)
            {
                TempData["error"] = "Service Not Found";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ServiceViewModel serviceResponce = new();

                var services = await _serviceService.GetServicessAsync();
                if (services.Any())
                {
                    serviceResponce.Services = services;
                }

                var serviceTypes = await _serviceTypeService.GetServiceTypesAsync();
                if (serviceTypes.Any())
                {
                    serviceResponce.ServiceTypes = serviceTypes;
                }

                var companyInfo = await _companyInfoService.GetCompanyInfosAsync();
                if (companyInfo.Any())
                {
                    var info = companyInfo.ToList().FirstOrDefault();
                    serviceResponce.CompanyInfo = info;
                }
                serviceResponce.Service = service;
                return View(serviceResponce);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
