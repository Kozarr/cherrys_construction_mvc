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
        private readonly ICallToActionMessage _callToActionMessage;
        private readonly ICallToActionSetting _callToActionSetting;
        private readonly ICompanyInfoService _companyInfoService;
        private readonly Mail _mail;
        private readonly ILegalDocumentService _legal;
        public HomeController(ILogger<HomeController> logger,
            ICallToActionMessage callToActionMessage,
            ICallToActionSetting callToActionSetting,
            ICompanyInfoService companyInfoService,
            Mail mail,
            ILegalDocumentService legal)
        {
            _logger = logger;
            _callToActionSetting = callToActionSetting;
            _callToActionMessage = callToActionMessage;
            _companyInfoService = companyInfoService;
            _mail = mail;
            _legal = legal;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            HomeViewModel homeResponse = new();

            // Call To Action Settings
            var ctaToReturn = await _callToActionSetting.GetCallToActionSettingsAsync();
            if (ctaToReturn.Any())
            {
                var returnCTA = ctaToReturn.First();
                homeResponse.CallToActionSettings = returnCTA;
            }

            var companyInfo = await _companyInfoService.GetCompanyInfosAsync();
            if (companyInfo.Any())
            {
                var compInfo = companyInfo.First();
                homeResponse.CompanyInfo = compInfo;
            }

            return View(homeResponse);
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
            if (id > 0)
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