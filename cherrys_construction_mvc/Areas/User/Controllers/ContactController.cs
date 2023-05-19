using cherrys_construction_mvc.Helper;
using cherrys_construction_mvc.Interfaces;
using cherrys_construction_mvc.Models;
using cherrys_construction_mvc.Utility;
using cherrys_construction_mvc.ViewModels.Contact;
using cherrys_construction_mvc.ViewModels.Responce;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace cherrys_construction_mvc.Areas.User.Controllers
{
    [Area(Breadcrumb.HomeArea)]
    public class ContactController : Controller
    {
        private readonly ILogger<ContactController> _logger;
        private readonly ICompanyInfoService _companyInfoService;
        private readonly Mail _mail;
        public ContactController(
            ILogger<ContactController> logger,
            ICompanyInfoService companyInfoService,
            Mail mail)
        {
            _logger = logger;
            _companyInfoService = companyInfoService;
            _mail = mail;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ContactViewModel contactViewModel = new();

            var companyInfo = await _companyInfoService.GetCompanyInfosAsync();
            if (companyInfo.Any()) 
            {
                var info = companyInfo.First();
                contactViewModel.CompanyInfo = info;
            }
            else { }

            return View(contactViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(ContactViewModel model)
        {
            if(model != null)
            {
                if (!string.IsNullOrWhiteSpace(model.Message.Email))
                {
                    CompanyInfoResponce settings = new();
                    var compInfo = await _companyInfoService.GetCompanyInfosAsync();
                    if (compInfo.Any())
                    {
                        settings = compInfo.First();
                        if (!string.IsNullOrWhiteSpace(model.Message.Body))
                        {
                            await _mail.SendGridEmail(model.Attachment, model.Message, settings);
                            TempData["success"] = "Message Sent";
                        }
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        _logger.LogError("Could not retrieve CompanyInfo for sending mail in - Contact Controller");
                        TempData["success"] = "Message Failed To Send";
                        return RedirectToAction(nameof(Index));
                    }
                }
                else
                {
                    TempData["error"] = "Message Failed To Send";
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                _logger.LogWarning("Did not receive any email content to send - Contact Controller");
                TempData["error"] = "Message Failed To Send";
                return RedirectToAction(nameof(Index));
            }
            
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
