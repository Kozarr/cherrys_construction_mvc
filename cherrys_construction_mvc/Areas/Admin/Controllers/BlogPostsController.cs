using AutoMapper;
using cherrys_construction_mvc.Interfaces;
using cherrys_construction_mvc.Utility;
using cherrys_construction_mvc.ViewModels.Blog;
using cherrys_construction_mvc.ViewModels.Requests;
using cherrys_construction_mvc.ViewModels.Responce;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using X.PagedList;

namespace cherrys_construction_mvc.Areas.Admin.Controllers
{
    [Area(Breadcrumb.AdminArea)]
    [Authorize(Roles = StaticDetails.Role_Admin + "," + StaticDetails.Role_Employee)]
    public class BlogPostsController : Controller
    {
        private readonly ILogger<BlogPostsController> _logger;
        private readonly IBlogPostService _postService;
        private readonly IBlogCategoryService _blogCategoryService;
        private readonly ICompanyInfoService _compInfo;
        private readonly IMapper _mapper;

        public BlogPostsController(ILogger<BlogPostsController> logger,
            IBlogPostService postService,
            IBlogCategoryService blogCategoryService,
            ICompanyInfoService compInfo,
            IMapper mapper)
        {
            _logger = logger;
            _postService = postService;
            _blogCategoryService = blogCategoryService;
            _compInfo = compInfo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int? page)
        {
            var pageNumber = page ?? 1;
            var blogList = await _postService.GetBlogPostsAsync();
            if (blogList.Any())
            {
                // Reduce blog description on preview View
                foreach(var post in blogList)
                {
                    if (!post.Description.IsNullOrEmpty())
                    {
                        post.Description = post.Description.Substring(0, 200);
                        post.Description += "...";
                    }               
                }

                // Sort to recent
                blogList = blogList.OrderBy(o => o.CreatedDate).ToList();

                var pageofPosts = blogList.ToPagedList(pageNumber, 6);
                ViewBag.OnePageOfBlogs = pageofPosts;
            }
            return View();
        }


        public async Task<ViewResult> Index(int? page, string? searchString)
        {
            var pageNumber = page ?? 1;
            var blogList = await _postService.GetBlogPostsAsync();
            if (blogList.Any())
            {
                foreach (var post in blogList)
                {
                    if (!post.Description.IsNullOrEmpty())
                    {
                        post.Description = post.Description.Substring(0, 200);
                        post.Description += "...";
                    }
                }
                // Sort to recent
                blogList = blogList.OrderBy(o => o.CreatedDate).ToList();

                // !!!!!!!!!!!!!!!!!!!!!!!
                // Searches only small version of description !!!!!!
                if (!string.IsNullOrEmpty(searchString))
                {
                    pageNumber = 1;
                    var newList = blogList.Where(s => s.Title.ToLower().Contains(searchString.Trim().ToLower()) ||
                                    s.Description.ToLower().Contains(searchString.Trim().ToLower())).ToList();
                    newList = newList.OrderBy(o => o.CreatedDate).ToList();
                    var pageOfPosts = newList.ToPagedList(pageNumber, 6);
                    ViewBag.OnePageOfBlogs = pageOfPosts;
                }
                else
                {
                    var pageOfPosts = blogList.ToPagedList(pageNumber, 6);
                    ViewBag.OnePageOfBlogs = pageOfPosts;
                }
            }
            return View();
        }


        //Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            BlogPostRequest request = new();
            var categories = await _blogCategoryService.GetBlogCategoriesAsync();
            if (categories.Any())
            {
                request.BlogCategories = categories.ToList();
            }
            return View(request);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BlogPostRequest blogPost)
        {
            if(ModelState.IsValid)
            {
                blogPost.CreatedDate = DateTime.Now;         
                var compList = await _compInfo.GetCompanyInfosAsync();
                if (compList.Any())
                {
                    var compInfo = compList.ToList().First();
                    blogPost.Author = compInfo.CompanyName;
                }
                
                await _postService.CreateBlogPostAsync(blogPost);
                TempData["success"] = "Blog Post Created Successfully";
                return RedirectToAction("Index");
            }

            TempData["error"] = "Blog Post Creation Failed";
            return RedirectToAction(nameof(Index));
        }

        // Edit
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if(id > 0)
            {
                var responce = await _postService.GetBlogPostByIdAsync(id);
                if(responce != null)
                {
                    var request = _mapper.Map<BlogPostRequest>(responce);
                    var catList = await _blogCategoryService.GetBlogCategoriesAsync();
                    if (catList.Any())
                    {
                        request.BlogCategories = catList.ToList();
                    }
                    return View(request);
                }
                TempData["error"] = "Failed To Fing Blog Post";
                _logger.LogError("BlogPostController Edit-Get : Get not retreive item from DB");
                return RedirectToAction(nameof(Index));
            }
            TempData["error"] = "Failed to get Item Id, Contact admin";
            _logger.LogError("BlogPostController Edit-Get : Passed Id=0 To Method");
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(BlogPostRequest blogPost)
        {
            if(ModelState.IsValid)
            {

                var dateChecker = DateTime.Now;
                dateChecker = dateChecker.AddDays(-1);
                
                if(dateChecker >= blogPost.CreatedDate)
                {
                    blogPost.UpdatedDate = DateTime.Now;
                }
                else
                {
                    blogPost.UpdatedDate = null;
                }
                
                await _postService.UpdateBlogPostAsync(blogPost.Id, blogPost);
                TempData["success"] = "Blog Post Updated Successfully";
                return RedirectToAction(nameof(Index));
            }
            TempData["error"] = "Blog Post Update Failed";
            return RedirectToAction(nameof(Index));
        }


        //Delete
        [HttpGet]
        public async Task<IActionResult> Delete(int? Id)
        {
            if(Id > 0)
            {
                // Get post
                BlogPostResponce blogPost = new();
                return View(blogPost);
            }
            TempData["error"] = "Failed To Fing Blog Post";
            _logger.LogError("BlogPostController Delete-Get : Passed Id=0 To Method");
            return RedirectToAction(nameof(Index));

        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(int? Id)
        {
            if(Id > 0)
            {
                // delete method
                TempData["success"] = "Blog Post Deleted Successfully";
                return RedirectToAction(nameof(Index));
            }
            TempData["error"] = "Blog Post Deletion Failed";
            _logger.LogError("BlogPostController Delete-Post : Passed Id=0 To Method");
            return RedirectToAction(nameof(Index));
        }
    }
}
