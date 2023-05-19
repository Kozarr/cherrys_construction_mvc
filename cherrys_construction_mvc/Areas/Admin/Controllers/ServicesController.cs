using cherrys_construction_mvc.Interfaces;
using cherrys_construction_mvc.Utility;
using cherrys_construction_mvc.ViewModels.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace cherrys_construction_mvc.Areas.Admin.Controllers
{
    [Area(Breadcrumb.AdminArea)]
    [Authorize(Roles = StaticDetails.Role_Admin + "," + StaticDetails.Role_Employee)]
    public class ServicesController : Controller
    {
        private readonly IServiceService _serviceService;
        public ServicesController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }


        // GET: ServiceController
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var services = await _serviceService.GetServicessAsync();
            return View(services);
        }

        // GET: ServiceController/Details/5
        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            var service = await _serviceService.GetServiceByIdAsync(id);
            return View(service);
        }

        // GET: ServiceController/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: ServiceController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ServiceRequest request)
        {
            if (ModelState.IsValid) { 
                await _serviceService.CreateServiceAsync(request);
                TempData["success"] = "Service Added Successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["error"] = "Service Failed To Add";
                return View();
            }
        }

        // GET: ServiceController/Edit/5
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            var service = await _serviceService.GetServiceByIdAsync(id);
            var editRequest = new ServiceRequest()
            {
                Description = service.Description,
                Icon = service.Icon,
                Title = service.Title,
                ArticleTitle = service.ArticleTitle,
                ArticleDescription = service.ArticleDescription,
                ImageLink = service.ImageLink,
            };
            return View(editRequest);
        }

        // POST: ServiceController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ServiceRequest request,int id)
        {
            if(ModelState.IsValid) { 
                await _serviceService.UpdateServiceAsync(id,request);
                TempData["success"] = "Service Updated Successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["error"] = "Service Failed To Add";
                return View();
            }
        }

        // GET: ServiceController/Delete/5
        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            var service = await _serviceService.GetServiceByIdAsync(id);
            return View(service);
        }

        // POST: ServiceController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteService(int id)
        {
            try
            {
                await _serviceService.DeleteServiceAsync(id);
                TempData["success"] = "Service Deleted Successfully";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["error"] = "Service Failed To Delete";
                return View();
            }
        }
    }
}
