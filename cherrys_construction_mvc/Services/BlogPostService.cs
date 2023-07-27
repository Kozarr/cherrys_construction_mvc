using AutoMapper;
using cherrys_construction_mvc.EfRepository.Interfaces;
using cherrys_construction_mvc.Interfaces;
using cherrys_construction_mvc.Models;
using cherrys_construction_mvc.Utility;
using cherrys_construction_mvc.ViewModels.Requests;
using cherrys_construction_mvc.ViewModels.Responce;
using Microsoft.IdentityModel.Tokens;

namespace cherrys_construction_mvc.Services
{
    public class BlogPostService : IBlogPostService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IEfRepository<BlogPost> _blogPostRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<BlogPost> _logger;


        public BlogPostService(IWebHostEnvironment webHostEnvironment,
            IEfRepository<BlogPost> blogPost,
            IMapper mapper,
            ILogger<BlogPost> logger)
        {
            _blogPostRepository = blogPost;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
        }

        public async Task CreateBlogPostAsync(BlogPostRequest request)
        {
            if (request.Image != null)
            {
                request.ImageLink = await Helper.Helper.UploadImage(request.Image, _webHostEnvironment, StaticDetails.SquareImage);
            }

            var post = _mapper.Map<BlogPost>(request);

            await _blogPostRepository.AddAsync(post);
        }


        public async Task DeleteBlogPostAsync(int id)
        {
            var post = await _blogPostRepository.GetByIdAsync(id);
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            if (post == null)
            {
                _logger.LogWarning("Could not find post to delete from existing ID");
            }
            else
            {
                var oldImagePath = Path.Combine(wwwRootPath, post.ImageLink.TrimStart('\\'));
                if (oldImagePath != null)
                {
                    if (File.Exists(oldImagePath))
                    {
                        File.Delete(oldImagePath);
                    }
                }
                await _blogPostRepository.DeleteAsync(post);
            }
        }

        public async Task<BlogPostResponce> GetBlogPostByIdAsync(int id)
        {
            var post = await _blogPostRepository.GetByIdAsync(id);
            return _mapper.Map<BlogPostResponce>(post);
        }

        public async Task<IEnumerable<BlogPostResponce>> GetBlogPostsAsync()
        {
            var posts = await _blogPostRepository.ListAsync();
            return _mapper.Map<IEnumerable<BlogPostResponce>>(posts);
        }

        public async Task SaveChangesAsync()
        {
            await _blogPostRepository.SaveChangesAsync();
        }

        public async Task UpdateBlogPostAsync(int id, BlogPostRequest request)
        {

            var post = await _blogPostRepository.GetByIdAsync(id);
            if (post != null)
            {
                if (request.Image != null)
                {
                    if (!string.IsNullOrWhiteSpace(request.ImageLink))
                    {
                        string wwwRootPath = _webHostEnvironment.WebRootPath;
                        var oldImagePath = Path.Combine(wwwRootPath, post.ImageLink.TrimStart('\\'));
                        if (File.Exists(oldImagePath))
                        {
                            File.Delete(oldImagePath);
                        }
                    }
                    request.ImageLink = await Helper.Helper.UploadImage(request.Image, _webHostEnvironment, StaticDetails.SquareImage);
                }
                else
                {
                    request.ImageLink = post.ImageLink;
                }
                request.CreatedDate = post.CreatedDate;

                request.Author ??= post.Author;

                _mapper.Map(request, post);
                await _blogPostRepository.UpdateAsync(post);
            }
            else
            {
                _logger.LogWarning("Could not find existing post in Blog Service");
            }
        }
    }

}
