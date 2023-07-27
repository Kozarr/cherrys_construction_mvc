using AutoMapper;
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
        private readonly IMapper _mapper;
        private readonly ILogger<ServicesController> _logger;
        public ServicesController(IServiceService serviceService,
            IMapper mapper,
            ILogger<ServicesController> logger)
        {
            _serviceService = serviceService;
            _mapper = mapper;
            _logger = logger;
        }


        // GET: ServiceController
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var services = await _serviceService.GetServicessAsync();
            if (services.Any())
            {
                foreach(var item in services)
                {
                    if (!string.IsNullOrWhiteSpace(item.Description))
                    {
                        if (item.Description.Length > 200)
                        {
                            item.ShortDescription = item.Description[..200];
                        }
                        else
                        {
                            item.ShortDescription = item.Description.Trim();
                            item.ShortDescription += "...";
                        }
                    }     
                }
                return View(services);
            }
            return View();          
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
            if (service != null)
            {
                var editRequest = _mapper.Map<ServiceRequest>(service);
                return View(editRequest);
            }
            _logger.LogError("Services Controller - Failed To Find Service For Edit");
            TempData["error"] = "Failed To Find Service";
            return RedirectToAction(nameof(Index));
            
            
        }

        // POST: ServiceController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ServiceRequest request,int id)
        {
            if(ModelState.IsValid) 
            {
                if (!string.IsNullOrWhiteSpace(request.Title))
                {
                    request.Title = request.Title.Trim();
                }
                if (!string.IsNullOrWhiteSpace(request.Description))
                {
                    request.Description = request.Description.Trim();
                }
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
