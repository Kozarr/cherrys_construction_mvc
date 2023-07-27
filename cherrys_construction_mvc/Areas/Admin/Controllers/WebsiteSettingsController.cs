using AutoMapper;
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
        private readonly IMapper _mapper;
        public WebsiteSettingsController(
            IWebHostEnvironment webHostEnvironment,
            ICompanyInfoService companyInfoService,
            IMapper mapper)
        {
            _webHostEnvironment = webHostEnvironment;
            _companyInfoService = companyInfoService;
            _mapper = mapper;
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
                if (item.CompanyInfo != null)
                {                   
                    if (item.NavigationImage != null)
                    {
                        var imageLink = await Helper.Helper.UploadImage(item.NavigationImage, _webHostEnvironment, StaticDetails.WideImage);
                        TempData["success"] = "Header Image Uploaded";
                        item.CompanyInfo.NavigationImageURL = imageLink;
                    }
                    if (item.FooterImage != null)
                    {
                        var imageLink = await Helper.Helper.UploadImage(item.FooterImage, _webHostEnvironment, StaticDetails.WideImage);
                        TempData["success"] = "Footer Image Uploaded";
                        item.CompanyInfo.FooterImageURL = imageLink;
                    }

                    // Company Information Trim
                    if (!string.IsNullOrWhiteSpace(item.CompanyInfo.CompanyName))
                    {
                        item.CompanyInfo.CompanyName = item.CompanyInfo.CompanyName.Trim();
                    }
                    if (!string.IsNullOrWhiteSpace(item.CompanyInfo.CompanyPhoneNumber))
                    {
                        item.CompanyInfo.CompanyPhoneNumber = item.CompanyInfo.CompanyPhoneNumber.Trim();
                    }
                    if (!string.IsNullOrWhiteSpace(item.CompanyInfo.CompanyEmail))
                    {
                        item.CompanyInfo.CompanyEmail = item.CompanyInfo.CompanyEmail.Trim();
                    }
                    if (!string.IsNullOrWhiteSpace(item.CompanyInfo.SendButton))
                    {
                        item.CompanyInfo.SendButton = item.CompanyInfo.SendButton.Trim();
                    }
                    if (!string.IsNullOrWhiteSpace(item.CompanyInfo.ServiceArea))
                    {
                        item.CompanyInfo.ServiceArea = item.CompanyInfo.ServiceArea.Trim();
                    }

                    // Company Social Media Links Trim
                    if (!string.IsNullOrWhiteSpace(item.CompanyInfo.YoutubeLink))
                    {
                        item.CompanyInfo.YoutubeLink = item.CompanyInfo.YoutubeLink.Trim();
                    }
                    if (!string.IsNullOrWhiteSpace(item.CompanyInfo.FaceBookLink))
                    {
                        item.CompanyInfo.FaceBookLink = item.CompanyInfo.FaceBookLink.Trim();
                    }
                    if (!string.IsNullOrWhiteSpace(item.CompanyInfo.InstagramLink))
                    {
                        item.CompanyInfo.InstagramLink = item.CompanyInfo.InstagramLink.Trim();
                    }
                    if (!string.IsNullOrWhiteSpace(item.CompanyInfo.LinkedInLink))
                    {
                        item.CompanyInfo.LinkedInLink = item.CompanyInfo.LinkedInLink.Trim();
                    }
                    if (!string.IsNullOrWhiteSpace(item.CompanyInfo.TwitterLink))
                    {
                        item.CompanyInfo.TwitterLink = item.CompanyInfo.TwitterLink.Trim();
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
            if (id > 0)
            {
                var itemToEditResponse = await _companyInfoService.GetCompanyInfoByIdAsync(id);
                if (itemToEditResponse != null)
                {
                    CompanyInfoRequestViewModel companyInfoRequest = new() { 
                        CompanyInfo = new CompanyInfoRequest()
                    };

                    companyInfoRequest.CompanyInfo = _mapper.Map<CompanyInfoRequest>(itemToEditResponse);

                    return View(companyInfoRequest); 
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return NotFound();
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
                        if (!string.IsNullOrWhiteSpace(old.NavigationImageURL))
                        {
                            string wwwRootPath = _webHostEnvironment.WebRootPath;
                            var oldImagePath = Path.Combine(wwwRootPath, old.NavigationImageURL.TrimStart('\\'));
                            if (oldImagePath != null)
                            {
                                Helper.Helper.DeleteImage(oldImagePath);
                            }
                            var imageLink = await Helper.Helper.UploadImage(item.NavigationImage, _webHostEnvironment, StaticDetails.StandardImage);
                            TempData["success"] = "Image Replaced";
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
                        if (!string.IsNullOrWhiteSpace(old.FooterImageURL))
                        {
                            string wwwRootPath = _webHostEnvironment.WebRootPath;
                            var oldImagePath = Path.Combine(wwwRootPath, old.FooterImageURL.TrimStart('\\'));
                            if (oldImagePath != null)
                            {
                                Helper.Helper.DeleteImage(oldImagePath);
                            }
                            var imageLink = await Helper.Helper.UploadImage(item.FooterImage, _webHostEnvironment, StaticDetails.StandardImage);
                            TempData["success"] = "Image Replaced";
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
