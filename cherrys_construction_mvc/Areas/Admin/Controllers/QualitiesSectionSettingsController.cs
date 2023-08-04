using AutoMapper;
using cherrys_construction_mvc.Interfaces;
using cherrys_construction_mvc.Utility;
using cherrys_construction_mvc.ViewModels.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace cherrys_construction_mvc.Areas.Admin.Controllers
{
    [Area(Breadcrumb.AdminArea)]
    [Authorize(Roles = StaticDetails.Role_Admin + "," + StaticDetails.Role_Employee)]
    public class QualitiesSectionSettingsController : Controller
    {
        private readonly ICompanyQualitiySettingService _qualitiesSettings;
        private readonly IMapper _mapper;
        public QualitiesSectionSettingsController(ICompanyQualitiySettingService qualitiesSettings,
            IMapper mapper)
        {
            _qualitiesSettings = qualitiesSettings;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var quality = await _qualitiesSettings.GetCompanyQualitiesSettingsAsync();
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
                var checker = await _qualitiesSettings.GetCompanyQualitiesSettingsAsync();
                if(checker.Any())
                {
                    TempData["error"] = "Qualities Section Information Already Exists";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(request.Title))
                    {
                        request.Title = request.Title.Trim();
                    }
                    if (!string.IsNullOrWhiteSpace(request.Description))
                    {
                        request.Description = request.Description.Trim();
                    }
                    await _qualitiesSettings.CreateCompanyQualitiesSettingAsync(request);
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
            if(id > 0)
            {
                var item = await _qualitiesSettings.GetCompanyQualitiesSettingByIdAsync(id);
                if(item != null)
                {
                    var request = _mapper.Map<CompanyQualitySettingRequest>(item);
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
                var checker = await _qualitiesSettings.GetCompanyQualitiesSettingByIdAsync(id);
                if(checker != null)
                {
                    if (!string.IsNullOrWhiteSpace(request.Title))
                    {
                        request.Title = request.Title.Trim();
                    }
                    if (!string.IsNullOrWhiteSpace(request.Description))
                    {
                        request.Description = request.Description.Trim();
                    }
                    await _qualitiesSettings.UpdateCompanyQualitiesSettingAsync(id, request);
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
