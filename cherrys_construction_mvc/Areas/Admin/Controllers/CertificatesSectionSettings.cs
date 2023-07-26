using cherrys_construction_mvc.Interfaces;
using cherrys_construction_mvc.Utility;
using cherrys_construction_mvc.ViewModels.Requests;
using cherrys_construction_mvc.ViewModels.Responce;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace cherrys_construction_mvc.Areas.Admin.Controllers
{
    [Area(Breadcrumb.AdminArea)]
    [Authorize(Roles = StaticDetails.Role_Admin + "," + StaticDetails.Role_Employee)]
    public class CertificatesSectionSettingsController : Controller
    {
        private readonly ICompanyCertificateSettingService _certSettings;
        public CertificatesSectionSettingsController(ICompanyCertificateSettingService certSettings)
        {
            _certSettings = certSettings;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var itemList = await _certSettings.GetCompanyCertificateSettingsAsync();
            
            if (itemList.Any())
            {
                CompanyCertificateSettingResponce setting = itemList.First();
                return View(setting);
            }
            else
            {
                return View();
            }
            
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CompanyCertificateSettingRequest request)
        {
            if (ModelState.IsValid)
            {
                var checker = await _certSettings.GetCompanyCertificateSettingsAsync();
                if(checker.Any())
                {
                    TempData["error"] = "Certificates Section Information Already Exists";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    await _certSettings.CreateCompanyCertificateSettingAsync(request);
                    TempData["success"] = "Certificates Section Information Created";
                    return RedirectToAction(nameof(Index));
                }             
            }
            else
            {
                TempData["error"] = "Request Is Invalid";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if(id != 0)
            {
                var item = await _certSettings.GetCompanyCertificateSettingByIdAsync(id);
                if(item != null)
                {
                    CompanyCertificateSettingRequest request = new();
                    request.Title = item.Title;
                    request.Description = item.Description;
                    request.ImageLink = item.ImageLink;
                    return View(request);
                }
                else
                {
                    TempData["error"] = "Certificates Section Information Not Found";
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                TempData["error"] = "Invalid Request";
                TempData["error"] = "Contact Website Developers";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CompanyCertificateSettingRequest request, int id)
        {
            if (ModelState.IsValid)
            {
                var checker = await _certSettings.GetCompanyCertificateSettingByIdAsync(id);
                if (checker != null)
                {
                    await _certSettings.UpdateCompanyCertificateSettingAsync(id, request);
                    TempData["success"] = "Certificates Section Information Updated";
                }
                else
                {
                    TempData["error"] = "Certificates Section Information Not Found To Update";
                }               
            }
            else
            {
                TempData["error"] = "Invalid Request";  
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
