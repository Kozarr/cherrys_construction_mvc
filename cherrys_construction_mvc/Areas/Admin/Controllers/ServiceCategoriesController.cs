using cherrys_construction_mvc.Interfaces;
using cherrys_construction_mvc.Utility;
using cherrys_construction_mvc.ViewModels.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace cherrys_construction_mvc.Areas.Admin.Controllers
{
    [Area(Breadcrumb.AdminArea)]
    [Authorize(Roles = StaticDetails.Role_Admin + "," + StaticDetails.Role_Employee)]
    public class ServiceCategoriesController : Controller
    {
        private readonly IServiceTypeService _serviceTypeService;
        public ServiceCategoriesController(IServiceTypeService serviceTypeService)
        {
            _serviceTypeService = serviceTypeService;
        }


        // GET: ServiceTypeController
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var serviceTypes = await _serviceTypeService.GetServiceTypesAsync();
            return View(serviceTypes);
        }

        // GET: ServiceTypeController/Details/5
        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            var serviceType = await _serviceTypeService.GetServiceTypeByIdAsync(id);
            return View(serviceType);
        }

        // GET: ServiceTypeController/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: ServiceTypeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([FromForm] ServiceTypeRequest request)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrWhiteSpace(request.Title))
                {
                    request.Title = request.Title.Trim();
                }
                if (!string.IsNullOrWhiteSpace(request.Description))
                {
                    request.Description = request.Description.Trim();
                }
                await _serviceTypeService.CreateServiceTypeAsync(request);
                TempData["success"] = "Service Category Added Successfully";
                return RedirectToAction(nameof(Index));

               
            }
            TempData["error"] = "Service Category Failed To Add";
            return View();
        }

        // GET: ServiceTypeController/Edit/5
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            var serviceType = await _serviceTypeService.GetServiceTypeByIdAsync(id);
            var editRequest = new ServiceTypeRequest()
            {
                Description = serviceType.Description,
                ImageLink = serviceType.ImageLink,
                Title = serviceType.Title,

            };
            return View(editRequest);
        }

        // POST: ServiceTypeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([FromForm]ServiceTypeRequest request,int id)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrWhiteSpace(request.Title))
                {
                    request.Title = request.Title.Trim();
                }
                if (!string.IsNullOrWhiteSpace(request.Description))
                {
                    request.Description = request.Description.Trim();
                }
                await _serviceTypeService.UpdateServiceTypeAsync(id,request);
                TempData["success"] = "Service Category Updated Successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["error"] = "Service Category Failed To Update";
                return View();
            }
        }

        // GET: ServiceTypeController/Delete/5
        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            var serviceType = await _serviceTypeService.GetServiceTypeByIdAsync(id);
            return View(serviceType);
        }

        // POST: ServiceTypeController/Delete/5
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteServiceType(int id)
        {
            if(id != 0)
            { 
                await _serviceTypeService.DeleteServiceTypeAsync(id);
                TempData["success"] = "Service Category Deleted Successfully";
            }
            else
            {
                TempData["success"] = "Service Category Failed To Delete";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
