using cherrys_construction_mvc.Interfaces;
using cherrys_construction_mvc.Utility;
using cherrys_construction_mvc.ViewModels.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace cherrys_construction_mvc.Areas.Admin.Controllers
{
    [Area(Breadcrumb.AdminArea)]
    [Authorize(Roles = StaticDetails.Role_Admin)]
    public class WebsiteSettingsController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ICompanyInfoService _companyInfoService;
        public WebsiteSettingsController(
            IWebHostEnvironment webHostEnvironment,
            ICompanyInfoService companyInfoService)
        {
            _webHostEnvironment = webHostEnvironment;
            _companyInfoService = companyInfoService;
        }
        public async Task<IActionResult> Index()
        {
            var settingsInDb = await _companyInfoService.GetCompanyInfosAsync();
            if (settingsInDb.Any())
            {
                var settingList = settingsInDb.ToList();
                var returnItem = settingList[0];
                return View(returnItem);
            }
            return View();
        }

        // Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var checker = await _companyInfoService.GetCompanyInfosAsync();
            if (checker.Any())
            {
                RedirectToAction(nameof(Index));
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CompanyInfoRequestViewModel item)
        {
            var checker = await _companyInfoService.GetCompanyInfosAsync();
            if (checker.Any())
            {
                TempData["error"] = "Website Settings Already Exist";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                if (ModelState.IsValid)
                {                   
                    if (item.NavigationImage != null)
                    {
                        var imageLink = await Helper.Helper.UploadImage(item.NavigationImage, _webHostEnvironment, StaticDetails.WideImage);
                        TempData["success"] = "Image Uploaded";
                        item.CompanyInfo.NavigationImageURL = imageLink;
                    }
                    if (item.FooterImage != null)
                    {
                        var imageLink = await Helper.Helper.UploadImage(item.FooterImage, _webHostEnvironment, StaticDetails.WideImage);
                        TempData["success"] = "Image Uploaded";
                        item.CompanyInfo.FooterImageURL = imageLink;
                    }

                    await _companyInfoService.CreateCompanyInfoAsync(item.CompanyInfo);

                    TempData["success"] = "Website Settings Added";
                    return RedirectToAction(nameof(Index));
              
                }
                else
                {
                    TempData["error"] = "Website Settings Failed To Add";
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
            else
            {
                var itemToEditResponse = await _companyInfoService.GetCompanyInfoByIdAsync(id);
                if (itemToEditResponse != null)
                {
                    CompanyInfoRequestViewModel companyInfoRequest = new() { 
                        CompanyInfo = new CompanyInfoRequest()
                    };                

                    companyInfoRequest.CompanyInfo.CompanyName = itemToEditResponse.CompanyName;
                    companyInfoRequest.CompanyInfo.CompanyPhoneNumber = itemToEditResponse.CompanyPhoneNumber;
                    companyInfoRequest.CompanyInfo.CompanyEmail = itemToEditResponse.CompanyEmail;
                    companyInfoRequest.CompanyInfo.ServiceArea = itemToEditResponse.ServiceArea;
                    companyInfoRequest.CompanyInfo.NavigationImageURL = itemToEditResponse.NavigationImageURL;
                    companyInfoRequest.CompanyInfo.FooterImageURL = itemToEditResponse.FooterImageURL;
                    companyInfoRequest.CompanyInfo.SendButton = itemToEditResponse.SendButton;

                    companyInfoRequest.CompanyInfo.FaceBookLink = itemToEditResponse.FaceBookLink;
                    companyInfoRequest.CompanyInfo.InstagramLink = itemToEditResponse.InstagramLink;
                    companyInfoRequest.CompanyInfo.YoutubeLink = itemToEditResponse.YoutubeLink;
                    companyInfoRequest.CompanyInfo.LinkedInLink = itemToEditResponse.LinkedInLink;
                    companyInfoRequest.CompanyInfo.TwitterLink = itemToEditResponse.TwitterLink;

                    return View(companyInfoRequest); 
                }
                else
                {
                    return NotFound();
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CompanyInfoRequestViewModel item)
        {
            if (item.CompanyInfo != null)
            {
                var oldItem = await _companyInfoService.GetCompanyInfosAsync();

                if (oldItem.Any())
                {
                     var old = oldItem.ToList()[0];

                    if (item.NavigationImage != null)
                    {
                        if (old.NavigationImageURL != null)
                        {
                            string wwwRootPath = _webHostEnvironment.WebRootPath;
                            var oldImagePath = Path.Combine(wwwRootPath, old.NavigationImageURL.TrimStart('\\'));
                            if (oldImagePath != null)
                            {
                                Helper.Helper.DeleteImage(oldImagePath);
                            }
                            var imageLink = await Helper.Helper.UploadImage(item.NavigationImage, _webHostEnvironment, StaticDetails.StandardImage);
                            TempData["success"] = "Image Uploaded";
                            item.CompanyInfo.NavigationImageURL = imageLink;
                        }
                        else
                        {
                            var imageLink = await Helper.Helper.UploadImage(item.NavigationImage, _webHostEnvironment, StaticDetails.StandardImage);
                            TempData["success"] = "Image Uploaded";
                            item.CompanyInfo.NavigationImageURL = imageLink;
                        }
                    }
                    if (item.FooterImage != null)
                    {
                        if (old.FooterImageURL != null)
                        {
                            string wwwRootPath = _webHostEnvironment.WebRootPath;
                            var oldImagePath = Path.Combine(wwwRootPath, old.FooterImageURL.TrimStart('\\'));
                            if (oldImagePath != null)
                            {
                                Helper.Helper.DeleteImage(oldImagePath);
                            }
                            var imageLink = await Helper.Helper.UploadImage(item.FooterImage, _webHostEnvironment, StaticDetails.StandardImage);
                            TempData["success"] = "Image Uploaded";
                            item.CompanyInfo.FooterImageURL = imageLink;
                        }
                        else
                        {
                            var imageLink = await Helper.Helper.UploadImage(item.FooterImage, _webHostEnvironment, StaticDetails.StandardImage);
                            TempData["success"] = "Image Uploaded";
                            item.CompanyInfo.FooterImageURL = imageLink;
                        }
                    }

                    await _companyInfoService.UpdateCompanyInfoAsync(old.Id, item.CompanyInfo);
                    TempData["success"] = "Website Settings Updated";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["error"] = "Could not find old information";
                    return View(item);
                }               
            }
            else
            {
                TempData["error"] = "Failed To Update Website Settings";
                return View(item);
            }

        }
    }
}
