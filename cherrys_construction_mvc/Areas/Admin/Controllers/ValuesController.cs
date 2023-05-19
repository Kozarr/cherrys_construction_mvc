using cherrys_construction_mvc.Interfaces;
using cherrys_construction_mvc.Utility;
using cherrys_construction_mvc.ViewModels.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace cherrys_construction_mvc.Areas.Admin.Controllers
{
    [Area(Breadcrumb.AdminArea)]
    [Authorize(Roles = StaticDetails.Role_Admin + "," + StaticDetails.Role_Employee)]
    public class ValuesController : Controller
    {
        private readonly ICompanyValueService _companyValueService;
        public ValuesController(ICompanyValueService companyValueService)
        {
            _companyValueService = companyValueService;
        }
        // GET: CompanyValueController
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var companyValues = await _companyValueService.GetCompanyValuesAsync();
            return View(companyValues);
        }

        // GET: CompanyValueController/Details/5
        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            var companyValue = await _companyValueService.GetCompanyValueByIdAsync(id);
            return View(companyValue);
        }

        // GET: CompanyValueController/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: CompanyValueController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([FromForm]CompanyValueRequest request)
        {
            try
            {
                await _companyValueService.CreateCompanyValueAsync(request);
                TempData["success"] = "Value Added Successfully";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["error"] = "Value Failed To Add";
                return View();
            }
        }

        // GET: CompanyValueController/Edit/5
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            var companyValue = await _companyValueService.GetCompanyValueByIdAsync(id);
            var editRequest = new CompanyValueRequest()
            {
                Description = companyValue.Description, 
                ImageLink = companyValue.ImageLink, 
                Title = companyValue.Title
            };
            return View(editRequest);
        }

        // POST: CompanyValueController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([FromForm]CompanyValueRequest request,int id)
        {
            try
            {
                await _companyValueService.UpdateCompanyValueAsync(id,request);
                TempData["success"] = "Value Updated Successfully";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["error"] = "Value Failed To Update";
                return View();
            }
        }

        // GET: CompanyValueController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var companyValue = await _companyValueService.GetCompanyValueByIdAsync(id);
            return View(companyValue);
        }

        // POST: CompanyValueController/Delete/5
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteCompanyValue(int id)
        {
            try
            {
                await _companyValueService.DeleteCompanyValueAsync(id);
                TempData["success"] = "Value Deleted Successfully";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["error"] = "Value Failed To Delete";
                return View();
            }
        }
    }
}
