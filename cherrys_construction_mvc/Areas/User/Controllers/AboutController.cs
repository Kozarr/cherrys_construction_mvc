using cherrys_construction_mvc.Interfaces;
using cherrys_construction_mvc.Models;
using cherrys_construction_mvc.Utility;
using cherrys_construction_mvc.ViewModels.About;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace cherrys_construction_mvc.Areas.User.Controllers
{
    [Area(Breadcrumb.HomeArea)]
    public class AboutController : Controller
    {
        private readonly ILogger<AboutController> _logger;
        private readonly IMemberService _memberService;
        private readonly ICompanyCertificateService _companyCertificateService;
        private readonly ITestimonyService _testimonyService;
        private readonly ICompanyStoryService _companyStoryService;
        private readonly ICompanyQualityService _companyQualityService;
        private readonly ICompanyInfoService _companyInfoService;
        private readonly ICompanyQualitiySettingService _companyQualitiySettingService;
        private readonly ICompanyCertificateSettingService _certSettings;

        public AboutController(
            ILogger<AboutController> logger,
            IMemberService memberService,
            ICompanyCertificateService companyCertificateService,
            ITestimonyService testimonyService,
            ICompanyStoryService companyStoryService,
            ICompanyQualityService companyQualityService,
            ICompanyInfoService companyInfoService,
            ICompanyQualitiySettingService companyQualitiySettingService,
            ICompanyCertificateSettingService certSettings)
        {
            _memberService = memberService;
            _logger = logger;
            _companyCertificateService = companyCertificateService;
            _testimonyService = testimonyService;
            _companyStoryService = companyStoryService;
            _companyQualityService = companyQualityService;
            _companyInfoService = companyInfoService;
            _companyQualitiySettingService = companyQualitiySettingService;
            _certSettings = certSettings;
        }

        public async Task<IActionResult> Index()
        {
            var aboutResponce = new AboutViewModel();

            var qualitiesSettings = await _companyQualitiySettingService.GetCompanyQualitiySettingsAsync();
            if (qualitiesSettings.Any())
            {
                aboutResponce.CompanyQualitiesSettings = qualitiesSettings.First();
            } else { }

            var certSectionSettings = await _certSettings.GetCompanyCertificateSettingsAsync();
            if (certSectionSettings.Any())
            {
                aboutResponce.CertSettings = certSectionSettings.First();
            }
            else { }

            var members = await _memberService.GetMemberssAsync();
            if (members.Any())
            {
                aboutResponce.Members = members;
            }
            else { }

            var certs = await _companyCertificateService.GetCertificatesAsync();
            if (certs.Any())
            {
                aboutResponce.CompanyCertificates = certs;
            }
            else { }

            var testimonies = await _testimonyService.GetTestimonysAsync();
            if (testimonies.Any())
            {
                aboutResponce.Testimonies = testimonies;
            }
            else { }

            var aboutStory = await _companyStoryService.GetCompanyStoriesAsync();
            if (aboutStory.Any())
            {
                aboutResponce.Story = aboutStory;
            }
            else { }

            var qualities = await _companyQualityService.GetCompanyQualitiesAsync();
            if (qualities.Any())
            {
                aboutResponce.Qualities = qualities;
            }
            else { }

            var compInfo = await _companyInfoService.GetCompanyInfosAsync();
            if (compInfo.Any())
            {
                var info = compInfo.ToList()[0];
                aboutResponce.CompanyInfo = info;
            }
            else { }

            return View(aboutResponce);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
