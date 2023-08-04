using AutoMapper;
using cherrys_construction_mvc.Interfaces;
using cherrys_construction_mvc.Services.ImageSharp.Interface;
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
        private readonly IImageProcessorService _imageProcessorService;
        private readonly IMapper _mapper;
        public WebsiteSettingsController(
            IWebHostEnvironment webHostEnvironment,
            ICompanyInfoService companyInfoService,
            IMapper mapper,
            IImageProcessorService imageProcessorService)
        {
            _webHostEnvironment = webHostEnvironment;
            _companyInfoService = companyInfoService;
            _mapper = mapper;
            _imageProcessorService = imageProcessorService;
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
                    if (item.CompanyInfo != null)
                    {
                        if (item.NavigationImage != null)
                        {
                            item.CompanyInfo.NavigationImageURL = await _imageProcessorService.ProcessImageAsync(item.NavigationImage, _webHostEnvironment, StaticDetails.WideImage);
                            TempData["success"] = "Header Image Uploaded";
                        }
                        if (item.FooterImage != null)
                        {
                            item.CompanyInfo.FooterImageURL = await _imageProcessorService.ProcessImageAsync(item.FooterImage, _webHostEnvironment, StaticDetails.WideImage);
                            TempData["success"] = "Footer Image Uploaded";
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
                    TempData["error"] = "Website Settings Failed To Add";
                    return View(item);
                }
                TempData["error"] = "Website Settings Failed To Add";
                return View(item);
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
                    CompanyInfoRequestViewModel companyInfoRequest = new()
                    {
                        CompanyInfo = new CompanyInfoRequest()
                    };

                    companyInfoRequest.CompanyInfo = _mapper.Map<CompanyInfoRequest>(itemToEditResponse);

                    return View(companyInfoRequest);
                }
                else
                {
                    TempData["error"] = "Information Not Found";
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                TempData["error"] = "Information Not Found";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CompanyInfoRequestViewModel item)
        {
            if (ModelState.IsValid)
            {
                if (item.CompanyInfo != null)
                {
                    var oldItems = await _companyInfoService.GetCompanyInfosAsync();
                    if (oldItems.Any())
                    {
                        var old = oldItems.FirstOrDefault();
                        if (old != null)
                        {
                            if (item.NavigationImage != null)
                            {
                                if (!string.IsNullOrWhiteSpace(old.NavigationImageURL))
                                {
                                    _imageProcessorService.DeleteImage(_webHostEnvironment.WebRootPath, old.NavigationImageURL);
                                }
                                item.CompanyInfo.NavigationImageURL = await _imageProcessorService.ProcessImageAsync(item.NavigationImage, _webHostEnvironment, StaticDetails.StandardImage);
                                TempData["success"] = "Header Image Updated";
                            }
                            else
                            {
                                item.CompanyInfo.NavigationImageURL = old.NavigationImageURL; 
                            }
                            if (item.FooterImage != null)
                            {
                                if (!string.IsNullOrWhiteSpace(old.FooterImageURL))
                                {
                                    _imageProcessorService.DeleteImage(_webHostEnvironment.WebRootPath, old.FooterImageURL);
                                }

                                item.CompanyInfo.FooterImageURL = await _imageProcessorService.ProcessImageAsync(item.FooterImage, _webHostEnvironment, StaticDetails.StandardImage);
                                TempData["success"] = "Footer Image Uploaded";
                            }
                            else
                            {
                                item.CompanyInfo.FooterImageURL = old.FooterImageURL;
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

                            await _companyInfoService.UpdateCompanyInfoAsync(old.Id, item.CompanyInfo);
                        }
                    }
                    TempData["success"] = "Website Settings Updated";
                    return RedirectToAction(nameof(Index));
                }
                TempData["error"] = "Could not find old information";
                return View(item);
            }
            TempData["error"] = "Submission Invalid";
            return View(item);
        }
    }

}
