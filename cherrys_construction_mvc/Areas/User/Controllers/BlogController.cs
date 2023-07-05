using cherrys_construction_mvc.Helper;
using cherrys_construction_mvc.Interfaces;
using cherrys_construction_mvc.Models;
using cherrys_construction_mvc.Utility;
using cherrys_construction_mvc.ViewModels.Blog;
using cherrys_construction_mvc.ViewModels.Responce;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;

namespace cherrys_construction_mvc.Areas.User.Controllers
{
    [Area(Breadcrumb.HomeArea)]
    public class BlogController : Controller
    {
        private readonly ILogger<BlogController> _logger;
        private readonly ICompanyInfoService _companyInfoService;
        private readonly IBlogPostService _postService;
        public BlogController(
            ILogger<BlogController> logger,
            ICompanyInfoService companyInfoService,
            IBlogPostService postService
            )
        {
             _logger = logger;  
            _companyInfoService = companyInfoService;
            _postService = postService;
        }

        public static BlogViewModel blogViewModel = new();


        [HttpGet]
        public async Task<IActionResult> Index(string sortOrder, string currentFilter,
    string searchString, int? pageNumber)
        {
            ViewData["DateSortParam"] = string.IsNullOrEmpty(sortOrder) ? "Oldest" : "";
            ViewData["CurrentSort"] = sortOrder;

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewData["CurrentFilter"] = searchString;

            var blogPosts = await _postService.GetBlogPostsAsync();
            if (blogPosts.Any())
            {
                foreach (var post in blogPosts)
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


                if (!string.IsNullOrWhiteSpace(searchString))
                {
                    blogPosts = blogPosts.Where(s => s.Title.ToLower().Contains(searchString.ToLower())
                                            || s.Description.ToLower().Contains(searchString.ToLower())
                                            || s.CreatedDateString.ToString().ToLower().Contains(searchString.ToLower()));
                }


                blogPosts = sortOrder switch
                {
                    "Oldest" => blogPosts.OrderBy(p => p.CreatedDate),
                    _ => blogPosts.OrderByDescending(p => p.CreatedDate),
                };


                int pageSize = 6;
                blogViewModel.Posts = await PaginatedList<BlogPostResponce>.CreateAsync(blogPosts.ToList(), pageNumber ?? 1, pageSize);
                return View(blogViewModel);
            }
            return View();
        }



        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            BlogDetailsViewModel blogDetailsVM = new();
            if(blogViewModel != null)
            {
                blogDetailsVM.CompanyInfo = blogViewModel.CompanyInfo;
            }
            return View(blogDetailsVM);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
