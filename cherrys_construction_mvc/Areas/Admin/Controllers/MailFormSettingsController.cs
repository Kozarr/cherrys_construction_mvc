using cherrys_construction_mvc.Interfaces;
using cherrys_construction_mvc.Utility;
using cherrys_construction_mvc.ViewModels.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace cherrys_construction_mvc.Areas.Admin.Controllers
{
    [Area(Breadcrumb.AdminArea)]
    [Authorize(Roles = StaticDetails.Role_Admin + "," + StaticDetails.Role_Employee)]
    public class MailFormSettingsController : Controller
    {
        private readonly ICallToActionSetting _callToActionSetting;

        public MailFormSettingsController(ICallToActionSetting callToActionSetting)
        {
            _callToActionSetting = callToActionSetting;
        }
        public async Task<IActionResult> Index()
        {
            var settingsInDb = await _callToActionSetting.GetCallToActionSettingsAsync();
            if (settingsInDb.Any())
            {
                var settingList = settingsInDb.ToList();
                return View(settingList.First());
            }
            return View();
        }

        // Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CallToActionSettingRequest item)
        {
            var checker = await _callToActionSetting.GetCallToActionSettingsAsync(); 
            if (checker.Any())
            {
                TempData["error"] = "Call To Action Setting Already Exists";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                if (ModelState.IsValid)
                {
                    if (!string.IsNullOrWhiteSpace(item.Title))
                    {
                        item.Title = item.Title.Trim();
                    }
                    if (!string.IsNullOrWhiteSpace(item.Description))
                    {
                        item.Description = item.Description.Trim();
                    }
                    if (!string.IsNullOrWhiteSpace(item.FormTitle))
                    {
                        item.FormTitle = item.FormTitle.Trim();
                    }
                    if (!string.IsNullOrWhiteSpace(item.FormDescription))
                    {
                        item.FormDescription = item.FormDescription.Trim();
                    }
                    await _callToActionSetting.CreateCallToActionSettingAsync(item);
                    TempData["success"] = "Call To Action Settings Added";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["error"] = "Call To Action Settings Failed To Add";
                    return View(item);
                }

            }

        }

        // Edit
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var itemToEdit = await _callToActionSetting.GetCallToActionSettingByIdAsync(id);
            if (itemToEdit == null)
            {
                return NotFound();
            }
            else
            {
                CallToActionSettingRequest request = new()
                {
                    Title = itemToEdit.Title,
                    Description = itemToEdit.Description,
                    ButtonText = itemToEdit.ButtonText,
                    FormDescription = itemToEdit.FormDescription,
                    FormTitle = itemToEdit.FormTitle,
                };
                return View(request);
            }
            
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CallToActionSettingRequest item)
        {
            int id = new();
            var itemList = await _callToActionSetting.GetCallToActionSettingsAsync();
            if (itemList.Any())
            {
                var itemToupdate = itemList.ToList().First();
                id = itemToupdate.Id;
            }
            else
            {
                TempData["error"] = "Failed To Load Settings";
                return View(item);
            }

            if (ModelState.IsValid)
            {
                if (!string.IsNullOrWhiteSpace(item.Title))
                {
                    item.Title = item.Title.Trim();
                }
                if (!string.IsNullOrWhiteSpace(item.Description))
                {
                    item.Description = item.Description.Trim();
                }
                if (!string.IsNullOrWhiteSpace(item.FormTitle))
                {
                    item.FormTitle = item.FormTitle.Trim();
                }
                if (!string.IsNullOrWhiteSpace(item.FormDescription))
                {
                    item.FormDescription = item.FormDescription.Trim();
                }
                await _callToActionSetting.UpdateCallToActionSettingAsync(id ,item);
                TempData["success"] = "Call To Action Settings Updated";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["error"] = "Failed To Update Call To Action Settings";
                return View(item);
            }
            
        }
    }
}
