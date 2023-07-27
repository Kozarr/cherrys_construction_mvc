using AutoMapper;
using cherrys_construction_mvc.Interfaces;
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
        private readonly IBlogCategoryService _blogCategoryService;
        private readonly IMapper _mapper;
        private readonly IBlogPostService _blogPostService;

        public BlogCategoriesController(ILogger<BlogCategoriesController> logger, 
            IBlogCategoryService blogCategoryService,
            IBlogPostService blogPostService,
            IMapper mapper)
        {
            _logger = logger;
            _blogCategoryService = blogCategoryService;
            _blogPostService = blogPostService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var categories = await _blogCategoryService.GetBlogCategoriesAsync();
            return View(categories);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            if(Id > 0)
            {
                var blogCategory = await _blogCategoryService.GetBlogCategoryByIdAsync(Id);
                if(blogCategory != null)
                {
                    BlogCategoryRequest request = new();
                    if (!string.IsNullOrWhiteSpace(blogCategory.Name))
                    {
                        request.Name = blogCategory.Name.Trim();
                        request.Id = blogCategory.Id;
                        return View(request);
                    }
                    request.Id = blogCategory.Id;
                    return View(request);
                }
                TempData["error"] = "Failed To Fing Blog Category";
                _logger.LogError("BlogCategoryController Edit-Get : Retrieved a null");
                return RedirectToAction(nameof(Index));
            }
            TempData["error"] = "Did not receive the Item Id to find the object, contact admin.";
            _logger.LogError("BlogCategoryController Edit-Get : Passed Id=0 To Method");
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(BlogCategoryRequest blogCategory)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrWhiteSpace(blogCategory.Name))
                {
                    blogCategory.Name = blogCategory.Name.Trim();
                }
                await _blogCategoryService.UpdateBlogCategoryAsync(blogCategory.Id, blogCategory);
                TempData["success"] = "Blog Category Updated Successfully";
                return RedirectToAction(nameof(Index));
            }
            _logger.LogError("BlogCategoryController Edit-POST : Model State Failed");
            TempData["error"] = "Blog Category Update Failed, ModelState Failed contact admin";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BlogCategoryRequest blogCategory)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrWhiteSpace(blogCategory.Name))
                {
                    blogCategory.Name = blogCategory.Name.Trim();
                }
                await _blogCategoryService.CreateBlogCategoryAsync(blogCategory);
                TempData["success"] = "Blog Category Created Successfully";
                return RedirectToAction(nameof(Index));
            }
            TempData["error"] = "Blog Category Creation Failed";
            _logger.LogError("BlogCategoryController Create-Post: Failed to create blog category");
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int Id)
        {
            if(Id > 0)
            {
                BlogCategoryResponce blogCategory = await _blogCategoryService.GetBlogCategoryByIdAsync(Id);
                if (blogCategory != null)
                {
                    return View(blogCategory);
                }
                TempData["error"] = "Failed to Find Blog Category, Contact Admin";
                _logger.LogError("BlogCategoryController Delete-Get : Id was above 0, failed to retrieve DB item");
                return RedirectToAction(nameof(Index));
            }
            TempData["error"] = "Failed to Find Blog Category, Contact Admin";
            _logger.LogError("BlogCategoryController Delete-Get : Passed Id=0 To The Method");
            return RedirectToAction(nameof(Index));
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCategory(int Id)
        {
            if (Id > 0)
            {
                var posts = await _blogPostService.GetBlogPostsAsync();
                if (posts.Any())
                {
                    foreach (var post in posts)
                    {
                        if (post.BlogCategoryId == Id)
                        {
                            post.BlogCategoryId = null;
                            post.BlogCategory = null;
                            var postRequest = _mapper.Map<BlogPostRequest>(post);
                            await _blogPostService.UpdateBlogPostAsync(postRequest.Id, postRequest);

                        }
                    }
                    await _blogPostService.SaveChangesAsync();
                }
                await _blogCategoryService.DeleteBlogCategoryAsync(Id);
                TempData["success"] = "Blog Category Deleted Successfully";
                return RedirectToAction(nameof(Index));
            }
            TempData["error"] = "Failed to Delete Blog Category, Contact Admin";
            _logger.LogError("BlogCategoryController Delete-POST : Passed Id=0 To The Method");
            return RedirectToAction(nameof(Index));
        }
    }
}
