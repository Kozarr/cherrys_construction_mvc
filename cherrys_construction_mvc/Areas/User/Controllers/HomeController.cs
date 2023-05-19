using cherrys_construction_mvc.Helper;
using cherrys_construction_mvc.Interfaces;
using cherrys_construction_mvc.Models;
using cherrys_construction_mvc.Utility;
using cherrys_construction_mvc.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace cherrys_construction_mvc.Areas.User.Controllers
{
    [Area(Breadcrumb.HomeArea)]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMemberService _memberService;
        private readonly ICompanyValueService _companyValueService;
        private readonly IHeroSliderService _heroSliderService;
        private readonly IServiceTypeService _serviceTypeService;
        private readonly IServiceService _serviceService;
        private readonly ICompanyCertificateService _companyCertificateService;
        private readonly ITestimonyService _testimonyService;
        private readonly ICallToActionMessage _callToActionMessage;
        private readonly ICallToActionSetting _callToActionSetting;
        private readonly ICompanyInfoService _companyInfoService;
        private readonly Mail _mail;
        private readonly ILegalDocumentService _legal;
        private readonly ICompanyCertificateSettingService _certSettings;
        public HomeController(ILogger<HomeController> logger, 
            IMemberService memberService, 
            ICompanyValueService companyValueService, 
            IHeroSliderService heroSliderService, 
            IServiceTypeService serviceTypeService, 
            IServiceService serviceService, 
            ICompanyCertificateService companyCertificateService, 
            ITestimonyService testimonyService,
            ICallToActionMessage callToActionMessage,
            ICallToActionSetting callToActionSetting,
            ICompanyInfoService companyInfoService,
            Mail mail,
            ILegalDocumentService legal,
            ICompanyCertificateSettingService certSettings)
        {
            _memberService = memberService;
            _logger = logger;
            _companyValueService = companyValueService;
            _heroSliderService = heroSliderService;
            _serviceTypeService = serviceTypeService;
            _serviceService = serviceService;
            _companyCertificateService = companyCertificateService;
            _testimonyService = testimonyService;
            _callToActionSetting = callToActionSetting;
            _callToActionMessage = callToActionMessage;
            _companyInfoService = companyInfoService;
            _mail = mail;
            _legal = legal;
            _certSettings = certSettings;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var homeResponce = new HomeViewModel();

            // HERO SLIDER
            var heroSlider = await _heroSliderService.GetHeroSlidersAsync();
            if (heroSlider.Any())
            {
                var hero = heroSlider.First();
                homeResponce.HeroSlider = hero;
            }
            else { }

            // Serviec Types
            var serviceType = await _serviceTypeService.GetServiceTypesAsync();
            if(serviceType.Any())
            {
                homeResponce.ServiceTypes = serviceType;
            }
            else { }

            var certSectionSettings = await _certSettings.GetCompanyCertificateSettingsAsync();
            if (certSectionSettings.Any())
            {
                homeResponce.CertSettings = certSectionSettings.First();
            }
            else { }

            // Services
            var services = await _serviceService.GetServicessAsync();
            if (services.Any())
            {
                homeResponce.Services = services;
            }
            else { }
            
            // Company Certificates
            var companyCerts = await _companyCertificateService.GetCertificatesAsync();
            if (companyCerts.Any())
            {
                homeResponce.CompanyCertificates = companyCerts;
            }
            else { }

            // Company Values
            var companyVal = await _companyValueService.GetCompanyValuesAsync();
            if (companyVal.Any())
            {
                homeResponce.CompanyValues = companyVal;
            }
            else { }

            // Testimonies
            var testimonies = await _testimonyService.GetTestimonysAsync();
            if (testimonies.Any())
            {
                homeResponce.Testimonies = testimonies;
            }
            else { }

            // Call To Action Settings
            var ctaToReturn = await _callToActionSetting.GetCallToActionSettingsAsync();
            if (ctaToReturn.Any())
            {
                var returnCTA = ctaToReturn.First();
                homeResponce.CallToActionSettings = returnCTA;
            }
            else { }

            var companyInfo = await _companyInfoService.GetCompanyInfosAsync();
            if (companyInfo.Any())
            {
                var compInfo = companyInfo.First();
                homeResponce.CompanyInfo = compInfo;              
            }    
            else { }

            return View(homeResponce);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Mail(HomeViewModel home)
        {
            if (ModelState.IsValid)
            {
                await _mail.SendGridEmail(home.Attachment, home.Message, home.CompanyInfo);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(nameof(Index));
            }
        }

        public async Task<IActionResult> Legal(int id)
        {
            if(id != 0)
            {
                LegalVM vM = new();
                var doc = await _legal.GetLegalDocumentByIdAsync(id);
                if (doc == null)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    var companyInfo = await _companyInfoService.GetCompanyInfosAsync();
                    vM.CompanyInfo = companyInfo.First();
                    vM.LegalDocument = doc;
                       
                    return View(vM);
                }
            }
            else
            {
                TempData["error"] = "Document Not Found";
                return Redirect(nameof(Index));
            }
            
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}