using cherrys_construction_mvc.Utility;
using cherrys_construction_mvc.ViewModels.Requests;
using cherrys_construction_mvc.ViewModels.Responce;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace cherrys_construction_mvc.Areas.Admin.Controllers
{
    [Area(Breadcrumb.AdminArea)]
    [Authorize(Roles = StaticDetails.Role_Admin + "," + StaticDetails.Role_Employee)]
    public class BlogCategoriesController : Controller
    {
        private readonly ILogger<BlogCategoriesController> _logger;

        public BlogCategoriesController(ILogger<BlogCategoriesController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? Id)
        {
            if(Id > 0)
            {
                // get value
                BlogCategoryResponce blogCategory = new();
                return View(blogCategory);
            }
            TempData["error"] = "Failed To Fing Blog Category";
            _logger.LogError("BlogCategoryController Edit-Get : Passed Id=0 To Method");
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(BlogCategoryRequest blogCategory)
        {
            if (ModelState.IsValid)
            {
                // update method
                TempData["success"] = "Blog Category Updated Successfully";
                return RedirectToAction(nameof(Index));
            }
            TempData["error"] = "Blog Category Update Failed";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BlogCategoryRequest blogCategory)
        {
            if (ModelState.IsValid)
            {
                // create method
                TempData["success"] = "Blog Category Created Successfully";
                return RedirectToAction(nameof(Index));
            }
            TempData["error"] = "Blog Category Creation Failed";
            _logger.LogError("BlogCategoryController Create-Post: Failed to create blog category");
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int? Id)
        {
            if(Id > 0)
            {
                // get item
                BlogCategoryResponce blogCategory = new();
                return View(blogCategory);
            }
            TempData["error"] = "Failed to Find Blog Category";
            _logger.LogError("BlogCategoryController Delete-Get : Passed Id=0 To The Method");
            return RedirectToAction(nameof(Index));
        }
    }
}
