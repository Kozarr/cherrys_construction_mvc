using cherrys_construction_mvc.Interfaces;
using cherrys_construction_mvc.Models;
using cherrys_construction_mvc.Utility;
using cherrys_construction_mvc.ViewModels.Blog;
using cherrys_construction_mvc.ViewModels.Responce;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using X.PagedList;

namespace cherrys_construction_mvc.Areas.User.Controllers
{
    [Area(Breadcrumb.HomeArea)]
    public class BlogController : Controller
    {
        private readonly ILogger<BlogController> _logger;
        private readonly ICompanyInfoService _companyInfoService;
        public BlogController(
            ILogger<BlogController> logger,
            ICompanyInfoService companyInfoService
            )
        {
             _logger = logger;  
            _companyInfoService = companyInfoService;
        }

        public static BlogViewModel blogViewModel = new();
        

        [HttpGet]
        public async Task<IActionResult> Index(int? page)
        {
            // Testing Pagination Hookup
            List<BlogPostResponce> PostsList = new();
            
            if(!PostsList.Any())
            {
                for (int i = 0; i < 20; i++)
                {
                    BlogPostResponce post = new()
                    {
                        Title = "Title Post " + i.ToString(),
                        Description = "This is a test description of a blog post. Many words can be here to use for blogs. " +
                        "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. " +
                        "Eu feugiat pretium nibh ipsum consequat nisl vel. Pellentesque id nibh tortor id aliquet lectus proin nibh nisl. " +
                        "Massa eget egestas purus viverra accumsan in. Viverra adipiscing at in tellus integer feugiat scelerisque.",
                        ImageLink = "/assets/img/blog/blog-1.jpg",
                        Author = "Cherry's Construction",
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now
                    };
                    PostsList.Add(post);
                }
            }
            
            var pageNumber = page ?? 1;

            // Sort by recent posts
            PostsList = PostsList.OrderBy(x => x.CreatedDate).ToList();

            // Reduce description size before sending to front
            if (PostsList.Any())
            {
                foreach (var post in PostsList)
                {
                    post.Description = post.Description.Substring(0, 200);
                    post.Description += "...";
                }
            }
            
            var pageOfPosts = PostsList.ToPagedList(pageNumber, 6);

            ViewBag.OnePageOfProducts = pageOfPosts;

            var companyInfo = await _companyInfoService.GetCompanyInfosAsync();
            if (companyInfo.Any())
            {
                var info = companyInfo.First();
                blogViewModel.CompanyInfo = info;
            }

            return View(blogViewModel);
        }

        // Error, when searching for a word and on second page, results comin in for second page of that result list
        // but when going to page 1 of new resulted list, program bumps to IActionResult and resets list.


        public async Task<ViewResult> Index(int? page, string? searchString)
        {
            // Testing Pagination Hookup
            List<BlogPostResponce> PostsList = new();

            if (!PostsList.Any())
            {
                for (int i = 0; i < 20; i++)
                {
                    BlogPostResponce post = new()
                    {
                        Title = "Title Post " + i.ToString(),
                        Description = "This is a test description of a blog post. Many words can be here to use for blogs. " +
                        "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. " +
                        "Eu feugiat pretium nibh ipsum consequat nisl vel. Pellentesque id nibh tortor id aliquet lectus proin nibh nisl. " +
                        "Massa eget egestas purus viverra accumsan in. Viverra adipiscing at in tellus integer feugiat scelerisque.",
                        ImageLink = "/assets/img/blog/blog-1.jpg",
                        Author = "Cherry's Construction",
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now
                    };
                    PostsList.Add(post);
                }
            }

            var pageNumber = page ?? 1;
            if (!string.IsNullOrEmpty(searchString))
            {
                if (PostsList.Any())
                {
                    var newList = PostsList.Where(s => s.Title.ToLower().Contains(searchString.Trim().ToLower()) ||
                                    s.Description.ToLower().Contains(searchString.Trim().ToLower())).ToList();
                    var pageOfPosts = newList.ToPagedList(pageNumber, 6);
                    ViewBag.OnePageOfProducts = pageOfPosts;
                }                
            }
            else
            {
                var pageOfPosts = PostsList.ToPagedList(pageNumber, 6);
                ViewBag.OnePageOfProducts = pageOfPosts;
            }
            return View(blogViewModel);
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
