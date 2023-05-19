using cherrys_construction_mvc.Interfaces;
using cherrys_construction_mvc.Utility;
using cherrys_construction_mvc.ViewModels.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace cherrys_construction_mvc.Areas.Admin.Controllers
{
    [Area(Breadcrumb.AdminArea)]
    [Authorize(Roles = StaticDetails.Role_Admin + "," + StaticDetails.Role_Employee)]
    public class QualitiesSectionSettingsController : Controller
    {
        private readonly ICompanyQualitiySettingService _qualitiesSettings;
        public QualitiesSectionSettingsController(ICompanyQualitiySettingService qualitiesSettings)
        {
            _qualitiesSettings = qualitiesSettings;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var quality = await _qualitiesSettings.GetCompanyQualitiySettingsAsync();
            return View(quality);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CompanyQualitySettingRequest request)
        {
            if (ModelState.IsValid)
            {
                var checker = await _qualitiesSettings.GetCompanyQualitiySettingsAsync();
                if(checker.Any())
                {
                    TempData["error"] = "Qualities Section Information Already Exists";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    await _qualitiesSettings.CreateCompanyQualitiySettingAsync(request);
                    TempData["success"] = "Qualities Section Information Created";
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
                var item = await _qualitiesSettings.GetCompanyQualitiySettingByIdAsync(id);
                if(item != null)
                {
                    CompanyQualitySettingRequest request = new();
                    request.Title = item.Title;
                    request.Description = item.Description;
                    request.ImageLink = item.ImageLink;
                    return View(request);
                }
                else
                {
                    TempData["error"] = "Qualities Section Information Not Found";
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
        public async Task<IActionResult> Edit(CompanyQualitySettingRequest request, int id)
        {
            if (ModelState.IsValid)
            {
                var checker = await _qualitiesSettings.GetCompanyQualitiySettingByIdAsync(id);
                if(checker != null)
                {
                    await _qualitiesSettings.UpdateCompanyQualitiySettingAsync(id, request);
                    TempData["success"] = "Qualities Section Information Updated";
                }
                else
                {
                    TempData["error"] = "Qualities Section Information Not Found To Update";
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
