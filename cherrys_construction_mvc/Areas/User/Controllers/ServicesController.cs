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
        private readonly ICompanyValueService _companyValueService;
        private readonly IServiceTypeService _serviceTypeService;
        private readonly IServiceService _serviceService;
        private readonly ICompanyInfoService _companyInfoService;
        private readonly ITestimonyService _testimonyService;

        public ServicesController(
            ILogger<ServicesController> logger,        
            ICompanyValueService companyValueService,
            IServiceTypeService serviceTypeService,
            IServiceService serviceService,
            ITestimonyService testimonyService,
            ICompanyInfoService companyInfoService)
        {           
            _logger = logger;
            _companyValueService = companyValueService;       
            _serviceTypeService = serviceTypeService;
            _serviceService = serviceService;          
            _testimonyService = testimonyService;
            _companyInfoService = companyInfoService;
        }
        public async Task<IActionResult> Index()
        {
            var serviceResponce = new ServiceViewModel();

            var services = await _serviceService.GetServicessAsync();
            if(services.Any())
            {
                serviceResponce.Services = services;
            }
            else { }

            var serviceTypes = await _serviceTypeService.GetServiceTypesAsync();
            if (serviceTypes.Any())
            {
                serviceResponce.ServiceTypes = serviceTypes;
            }
            else { }

            var companyValues = await _companyValueService.GetCompanyValuesAsync();
            if (companyValues.Any())
            {
                serviceResponce.CompanyValues = companyValues;
            }
            else { }

            var testimonies = await _testimonyService.GetTestimonysAsync();
            if (testimonies.Any())
            {
                serviceResponce.Testimonies = testimonies;
            }
            else { }

            var companyInfo = await _companyInfoService.GetCompanyInfosAsync();
            if (companyInfo.Any())
            {
                var info = companyInfo.ToList()[0];
                serviceResponce.CompanyInfo = info;
            }
            else { }

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
                var serviceResponce = new ServiceViewModel();

                var services = await _serviceService.GetServicessAsync();
                if (services.Any())
                {
                    serviceResponce.Services = services;
                }
                else { }

                var serviceTypes = await _serviceTypeService.GetServiceTypesAsync();
                if (serviceTypes.Any())
                {
                    serviceResponce.ServiceTypes = serviceTypes;
                }
                else { }

                var companyValues = await _companyValueService.GetCompanyValuesAsync();
                if (companyValues.Any())
                {
                    serviceResponce.CompanyValues = companyValues;
                }
                else { }

                var testimonies = await _testimonyService.GetTestimonysAsync();
                if (testimonies.Any())
                {
                    serviceResponce.Testimonies = testimonies;
                }
                else { }

                var companyInfo = await _companyInfoService.GetCompanyInfosAsync();
                if (companyInfo.Any())
                {
                    var info = companyInfo.ToList()[0];
                    serviceResponce.CompanyInfo = info;
                }
                else { }

                serviceResponce.Service = service;

                return View(serviceResponce);

            }
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
