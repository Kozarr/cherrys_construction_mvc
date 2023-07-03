using AutoMapper;
using cherrys_construction_mvc.Interfaces;
using cherrys_construction_mvc.Utility;
using cherrys_construction_mvc.ViewModels.Blog;
using cherrys_construction_mvc.ViewModels.Requests;
using cherrys_construction_mvc.ViewModels.Responce;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MimeKit;
using System.Globalization;
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
        public static List<BlogPostResponce> mainList = new();
        public static string ConstSearchString = "";
        [HttpGet]
        public async Task<IActionResult> Index(int? page)
        {
            var pageNumber = page ?? 1;
            var blogList = await _postService.GetBlogPostsAsync();
            if (blogList.Any())
            {
                mainList = blogList.ToList();
                // Reduce blog description on preview View
                foreach (var post in mainList)
                {
                    if (!post.Description.IsNullOrEmpty())
                    {
                        post.ShortDescription = post.Description.Substring(0, 200);
                        post.ShortDescription += "...";
                        post.CreatedDateString = post.CreatedDate.ToString("MMMM dd, yyyy");
                        if (post.UpdatedDate != null)
                        {
                            var upDate = post.UpdatedDate.Value;
                            post.UpdatedDateString = upDate.ToString("MMMM dd, yyyy");
                        }
                    }               
                }

                // Sort to recent
                mainList = mainList.OrderByDescending(o => o.CreatedDate).ToList();

                var pageofPosts = mainList.ToPagedList(pageNumber, 6);
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
                mainList = blogList.ToList();
                // Shorten Description for front view
                foreach (var post in mainList)
                {
                    if (!post.Description.IsNullOrEmpty())
                    {

                        post.ShortDescription = post.Description.Substring(0, 200);
                        post.ShortDescription += "...";
                        post.CreatedDateString = post.CreatedDate.ToString("MMMM dd, yyyy");
                        if(post.UpdatedDate != null)
                        {
                            var upDate = post.UpdatedDate.Value;
                            post.UpdatedDateString = upDate.ToString("MMMM dd, yyyy");
                        }
                        
                    }
                }

                // search title and description
                if (!string.IsNullOrWhiteSpace(searchString))
                {
                    ConstSearchString = searchString;
                    pageNumber = 1;
                    var newList = mainList.Where(s => s.Title.ToLower().Contains(searchString.Trim().ToLower()) ||
                                    s.Description.ToLower().Contains(searchString.Trim().ToLower()) || 
                                    s.CreatedDateString.ToLower().Contains(searchString.Trim().ToLower())).ToList();
                    newList = newList.OrderByDescending(o => o.CreatedDate).ToList();
                    mainList = newList;
                }
                else
                {
                    // !!!!!! TODO: need to fix pagination and searching
                    // search and be able to go to second page of search not reset list.
                    mainList = blogList.ToList();
                }
                
                // Sort to recent
                mainList = mainList.OrderByDescending(o => o.CreatedDate).ToList();

                var pageOfPosts = mainList.ToPagedList(pageNumber, 6);
                ViewBag.OnePageOfBlogs = pageOfPosts;
            
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

                var updatedDate = DateTime.Now;
                updatedDate = updatedDate.AddDays(-1);
                
                if(updatedDate >= blogPost.CreatedDate)
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
