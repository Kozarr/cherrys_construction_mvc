using cherrys_construction_mvc.Utility;
using cherrys_construction_mvc.ViewModels.Requests;
using cherrys_construction_mvc.ViewModels.Responce;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace cherrys_construction_mvc.Areas.Admin.Controllers
{
    [Area(Breadcrumb.AdminArea)]
    [Authorize(Roles = StaticDetails.Role_Admin + "," + StaticDetails.Role_Employee)]
    public class BlogPostController : Controller
    {
        private readonly ILogger<BlogPostController> _logger;

        public BlogPostController(ILogger<BlogPostController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }

        //Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BlogPostRequest blogPost)
        {
            if(ModelState.IsValid)
            {
                // add blog post
                TempData["success"] = "Blog Post Created Successfully";
                return RedirectToAction("Index");
            }

            TempData["error"] = "Blog Post Creation Failed";
            return RedirectToAction("Index");
        }

        // Edit
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if(id != 0)
            {
                // get post from db
                BlogPostResponce blogPost = new();
                return View(blogPost);
            }
            TempData["error"] = "Failed To Fing Blog Post";
            _logger.LogError("Blog Post Edit-Get : passed Id=0 or failed to find existing Blog Post");
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(BlogPostRequest blogPost)
        {
            if(ModelState.IsValid)
            {
                // update method
                TempData["success"] = "Blog Post Updated Successfully";
                return RedirectToAction("Index");
            }
            TempData["error"] = "Blog Post Update Failed";
            return RedirectToAction("Index");
        }


        //Delete
        [HttpGet]
        public async Task<IActionResult> Delete(int? Id)
        {
            if(Id != 0)
            {
                // Get post
                BlogPostResponce blogPost = new();
                return View(blogPost);
            }
            TempData["error"] = "Failed To Fing Blog Post";
            _logger.LogError("Blog Post Delete-Get : passed Id=0 or failed to find existing Blog Post");
            return RedirectToAction("Index");

        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(int? Id)
        {
            if(Id != 0)
            {
                // delete method
                TempData["success"] = "Blog Post Deleted Successfully";
                return RedirectToAction("Index");
            }
            TempData["error"] = "Blog Post Deletion Failed";
            _logger.LogError("Blog Post Delete-Get : passed Id=0 or failed to find existing Blog Post");
            return RedirectToAction("Index");
        }
    }
}
