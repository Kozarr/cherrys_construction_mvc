using AutoMapper;
using cherrys_construction_mvc.EfRepository.Interfaces;
using cherrys_construction_mvc.Interfaces;
using cherrys_construction_mvc.Models;
using cherrys_construction_mvc.ViewModels.Requests;
using cherrys_construction_mvc.ViewModels.Responce;

namespace cherrys_construction_mvc.Services
{
    public class BlogCategoryService : IBlogCategoryService
    {
        private readonly IEfRepository<BlogCategory> _blogCategoryRepository;
        private readonly IMapper _mapper;

        public BlogCategoryService(IEfRepository<BlogCategory> blogCategory,
            IMapper mapper)
        {
            _blogCategoryRepository = blogCategory;
            _mapper = mapper;   
        }
        public async Task CreateBlogCategoryAsync(BlogCategoryRequest request)
        {   
            var blogCategory = _mapper.Map<BlogCategory>(request);
            await _blogCategoryRepository.AddAsync(blogCategory);
            await _blogCategoryRepository.SaveChangesAsync();
        }

        public async Task DeleteBlogCategoryAsync(int id)
        {
            var blogCategory = await _blogCategoryRepository.GetByIdAsync(id);
            if (blogCategory != null)
            {               
                await _blogCategoryRepository.DeleteAsync(blogCategory);
                await _blogCategoryRepository.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<BlogCategoryResponce>> GetBlogCategoriesAsync()
        {
            var categories = await _blogCategoryRepository.ListAsync();
            return _mapper.Map<IEnumerable<BlogCategoryResponce>>(categories);
        }

        public async Task<BlogCategoryResponce> GetBlogCategoryByIdAsync(int id)
        {
            var category = await _blogCategoryRepository.GetByIdAsync(id);
             return _mapper.Map<BlogCategoryResponce>(category);
        }

        public async Task UpdateBlogCategoryAsync(int id, BlogCategoryRequest request)
        {
            var category = await _blogCategoryRepository.GetByIdAsync(id);
            _mapper.Map(request, category);
            await _blogCategoryRepository.UpdateAsync(category);
            await _blogCategoryRepository.SaveChangesAsync();
        }
    }

        
    
}
