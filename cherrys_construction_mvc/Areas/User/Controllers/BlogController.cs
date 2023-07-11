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
        private readonly IBlogCategoryService _categoryService;
        private readonly IBlogPostService _postService;
        public BlogController(
            ILogger<BlogController> logger,
            ICompanyInfoService companyInfoService,
            IBlogCategoryService categoryService,
            IBlogPostService postService
            )
        {
             _logger = logger;  
            _companyInfoService = companyInfoService;
            _categoryService = categoryService;
            _postService = postService;
        }

        public static BlogViewModel blogViewModel = new();


        [HttpGet]
        public async Task<IActionResult> Index(string sortOrder, string currentFilter,
                                                string searchString, int? pageNumber)
        {
            ViewData["DateSortParam"] = string.IsNullOrEmpty(sortOrder) ? "Oldest" : "";
            ViewData["CurrentSort"] = sortOrder;

            if (!string.IsNullOrWhiteSpace(searchString))
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
                var categories = await _categoryService.GetBlogCategoriesAsync();
                if (categories.Any())
                {
                    blogViewModel.Categories = categories;
                }
                foreach (var post in blogPosts)
                {
                    if (!string.IsNullOrWhiteSpace(post.Description))
                    {
                        if(post.Description.Length > 200)
                        {
                            post.ShortDescription = post.Description.Substring(0, 200);
                            post.ShortDescription += "...";
                        }
                        else
                        {
                            post.ShortDescription = post.Description.Substring(0, post.Description.Length);
                            post.ShortDescription += "...";
                        }
                        post.CreatedDateString = post.CreatedDate.ToString("MMMM dd, yyyy");
                        if (post.UpdatedDate != null)
                        {
                            var upDate = post.UpdatedDate.Value;
                            post.UpdatedDateString = upDate.ToString("MMMM dd, yyyy");
                        }
                        if (post.BlogCategoryId > 0)
                        {
                            post.BlogCategory = await _categoryService.GetBlogCategoryByIdAsync(post.BlogCategoryId.Value);
                        }
                    }
                }

                if (!string.IsNullOrWhiteSpace(searchString))
                {
                    blogPosts = blogPosts.Where(s => s.Title.ToLower().Contains(searchString.ToLower())
                                            || s.Description.ToLower().Contains(searchString.ToLower())
                                            || s.CreatedDateString.ToString().ToLower().Contains(searchString.ToLower())
                                            || s.BlogCategory.Name.ToLower().Contains(searchString.ToLower()));
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
                var compInfo = await _companyInfoService.GetCompanyInfosAsync();
                blogViewModel.CompanyInfo = compInfo.First();
                blogDetailsVM.CompanyInfo = blogViewModel.CompanyInfo;
            }           
            var blogList = await _postService.GetBlogPostsAsync();
            if (blogList.Any())
            {
                var mainBlog = blogList.Where(b => b.Id == id).FirstOrDefault();
                blogDetailsVM.Post = mainBlog;
                foreach(var post in  blogList)
                {
                    post.CreatedDateString = post.CreatedDate.ToString("MMMM dd, yyyy");
                    if (post.UpdatedDate != null)
                    {
                        var upDate = post.UpdatedDate.Value;
                        post.UpdatedDateString = upDate.ToString("MMMM dd, yyyy");
                    }
                    if (post.BlogCategoryId > 0)
                    {
                        post.BlogCategory = await _categoryService.GetBlogCategoryByIdAsync(post.BlogCategoryId.Value);
                    }
                    blogDetailsVM.BlogList = blogList.Take(6).ToList();
                }
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
