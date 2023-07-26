using cherrys_construction_mvc.Interfaces;
using cherrys_construction_mvc.Utility;
using cherrys_construction_mvc.ViewModels.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace cherrys_construction_mvc.Areas.Admin.Controllers
{
    [Area(Breadcrumb.AdminArea)]
    [Authorize(Roles = StaticDetails.Role_Admin + "," + StaticDetails.Role_Employee)]
    public class CertificatesController : Controller
    {
        private readonly ICompanyCertificateService _companyCertificateService;
        public CertificatesController(ICompanyCertificateService companyCertificateService)
        {
            _companyCertificateService = companyCertificateService;
        }
        public async Task<IActionResult> Index()
        {
            var certificates = await _companyCertificateService.GetCertificatesAsync();
            return View(certificates);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] CompanyCertificateRequest request)
        {
            if (ModelState.IsValid)
            {
                await _companyCertificateService.CreateCertificateAsync(request);
                TempData["success"] = "Certificate Added Successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["error"] = "Certificate Failed To Add";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var companyCertificate = await _companyCertificateService.GetCertificateByIdAsync(id);
            var editRequest = new CompanyCertificateRequest()
            {
               Description = companyCertificate.Description,    
               Title = companyCertificate.Title,
            };

            return View(editRequest);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromForm] CompanyCertificateRequest request, int id)
        {
            if (ModelState.IsValid)
            {
                await _companyCertificateService.UpdateCertificateAsync(id,request);
                TempData["success"] = "Certificate Updated Successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["error"] = "Certificate Failed To Update";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _companyCertificateService.GetCertificateByIdAsync(id);
            return View(category);
        }

        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteCompanyCertificate(int id)
        {
            if (ModelState.IsValid)
            {
                await _companyCertificateService.DeleteCertificateAsync(id);
                TempData["success"] = "Certificate Deleted Successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["error"] = "Certificate Failed To Delete";
                return RedirectToAction(nameof(Index));
            }
        }

    }
}
