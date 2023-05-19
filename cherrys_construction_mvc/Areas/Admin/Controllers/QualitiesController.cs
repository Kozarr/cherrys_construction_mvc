using cherrys_construction_mvc.Interfaces;
using cherrys_construction_mvc.Utility;
using cherrys_construction_mvc.ViewModels.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace cherrys_construction_mvc.Areas.Admin.Controllers
{
    [Area(Breadcrumb.AdminArea)]
    [Authorize(Roles = StaticDetails.Role_Admin + "," + StaticDetails.Role_Employee)]
    public class QualitiesController : Controller
    {
        private readonly ICompanyQualityService _companyQualityService;
        public QualitiesController(ICompanyQualityService companyQualityService)
        {
            _companyQualityService = companyQualityService;
        }
        // GET: CompanyQualityController
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var companyQualities = await _companyQualityService.GetCompanyQualitiesAsync();
            return View(companyQualities);
        }

        // GET: CompanyQualityController/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var companyQuality = await _companyQualityService.GetCompanyQualityByIdAsync(id);
            return View(companyQuality);
        }

        // GET: CompanyQualityController/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: CompanyQualityController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] CompanyQualityRequest request )
        {
            try
            {

                if (ModelState.IsValid)
                {
                    await _companyQualityService.CreateCompanyQualityAsync(request);
                    TempData["success"] = "New Company Quality Added Successfully";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["error"] = "Company Quality Failed To Add";
                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: CompanyQualityController/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var companyQuality = await _companyQualityService.GetCompanyQualityByIdAsync(id);
            var editRequest = new CompanyQualityRequest()
            {
                Description = companyQuality.Description,
                Icon = companyQuality.Icon,
                Title = companyQuality.Title
            };
            return View(editRequest);
        }

        // POST: CompanyQualityController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromForm] CompanyQualityRequest request,int id)
        {          
            if (ModelState.IsValid)
            {
                await _companyQualityService.UpdateCompanyQualityAsync(id, request);
                TempData["success"] = "Company Quality Updated Successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["error"] = "Company Quality Failed To Update";
                return RedirectToAction(nameof(Index));
            }

        }

        // GET: CompanyQualityController/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var companyQuality = await _companyQualityService.GetCompanyQualityByIdAsync(id);
            return View(companyQuality);
        }

        // POST: CompanyQualityController/Delete/5
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCompanyQuality(int id)
        {
            try
            {
                await _companyQualityService.DeleteCompanyQualityAsync(id);
                TempData["success"] = "Company Quality Deleted Successfully";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
