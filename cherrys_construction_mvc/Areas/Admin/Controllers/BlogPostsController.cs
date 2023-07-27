using AutoMapper;
using cherrys_construction_mvc.Helper;
using cherrys_construction_mvc.Interfaces;
using cherrys_construction_mvc.Utility;
using cherrys_construction_mvc.ViewModels.Blog;
using cherrys_construction_mvc.ViewModels.Requests;
using cherrys_construction_mvc.ViewModels.Responce;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace cherrys_construction_mvc.Areas.Admin.Controllers
{
    [Area(Breadcrumb.AdminArea)]
    [Authorize(Roles = StaticDetails.Role_Admin + "," + StaticDetails.Role_Employee)]
    public class BlogPostsController : Controller
    {
        private readonly ILogger<BlogPostsController> _logger;
        private readonly IBlogPostService _postService;
        private readonly IBlogCategoryService _categoryService;
        private readonly ICompanyInfoService _compInfo;
        private readonly IMapper _mapper;

        public BlogPostsController(ILogger<BlogPostsController> logger,
            IBlogPostService postService,
            IBlogCategoryService categoryService,
            ICompanyInfoService compInfo,
            IMapper mapper)
        {
            _logger = logger;
            _postService = postService;
            _categoryService = categoryService;
            _compInfo = compInfo;
            _mapper = mapper;
        }
        
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

            BlogVM blogViewModel = new();

            var blogPosts = await _postService.GetBlogPostsAsync();
            if (blogPosts.Any())
            {
                foreach (var post in blogPosts)
                {
                    if (!string.IsNullOrWhiteSpace(post.Description))
                    {
                        if (post.Description.Length > 200)
                        {
                            post.ShortDescription = post.Description[..200];
                            post.ShortDescription += "...";
                        }
                        else
                        {
                            post.ShortDescription = post.Description;
                            post.ShortDescription += "...";
                        }
                        post.CreatedDateString = post.CreatedDate.ToString("MMMM dd, yyyy");
                        if (post.UpdatedDate != null)
                        {
                            var upDate = post.UpdatedDate.Value;
                            post.UpdatedDateString = upDate.ToString("MMMM dd, yyyy");
                        }
                        if(post.BlogCategoryId > 0)
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
                var categories = await _categoryService.GetBlogCategoriesAsync();
                if (categories.Any())
                {
                    blogViewModel.Categories = categories;
                }
                return View(blogViewModel);
            }
            return View();
        }
    


        //Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            BlogPostRequest request = new();
            var categories = await _categoryService.GetBlogCategoriesAsync();
            if (categories.Any())
            {
                request.blogCategoriesList = categories;
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
                if (!string.IsNullOrWhiteSpace(blogPost.Description))
                {
                    blogPost.Description = blogPost.Description.Trim();
                }
                if (!string.IsNullOrWhiteSpace(blogPost.Title))
                {
                    blogPost.Title = blogPost.Title.Trim();
                }

                await _postService.CreateBlogPostAsync(blogPost);
                await _postService.SaveChangesAsync();
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
                    var catList = await _categoryService.GetBlogCategoriesAsync();
                    if (catList.Any())
                    {
                        request.blogCategoriesList = catList.ToList();
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
                if (string.IsNullOrWhiteSpace(blogPost.Author))
                {
                    var complist = await _compInfo.GetCompanyInfosAsync();
                    blogPost.Author = complist.ToList().First().CompanyName;
                }

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

                if (!string.IsNullOrWhiteSpace(blogPost.Description))
                {
                    blogPost.Description = blogPost.Description.Trim();
                }
                if (!string.IsNullOrWhiteSpace(blogPost.Title))
                {
                    blogPost.Title = blogPost.Title.Trim();
                }

                await _postService.UpdateBlogPostAsync(blogPost.Id, blogPost);
                await _postService.SaveChangesAsync();
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
            if (Id > 0)
            {
                var blogPost = await _postService.GetBlogPostByIdAsync(Id.Value);
                if (blogPost != null)
                {
                    return View(blogPost);
                }
                else
                {
                    TempData["error"] = "Failed To Fing Blog Post";
                    _logger.LogError("BlogPostController Delete-Get : Failed to find blog post");
                    return RedirectToAction(nameof(Index));
                }
            }
            TempData["error"] = "Failed To get Necessary Information";
            _logger.LogError("BlogPostController Delete-Get : Passed Id=0 To Method");
            return RedirectToAction(nameof(Index));

        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(int? Id)
        {
            if (Id > 0)
            {
                await _postService.DeleteBlogPostAsync(Id.Value);
                await _postService.SaveChangesAsync();
                TempData["success"] = "Blog Post Deleted Successfully";
                return RedirectToAction(nameof(Index));
              
            }
            TempData["error"] = "Blog Post Deletion Failed";
            _logger.LogError("BlogPostController Delete-Post : Passed Id=0 To Method");
            return RedirectToAction(nameof(Index));
        }

    }
}
