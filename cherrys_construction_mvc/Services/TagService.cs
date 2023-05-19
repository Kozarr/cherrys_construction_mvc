using AutoMapper;
using cherrys_construction_mvc.EfRepository.Interfaces;
using cherrys_construction_mvc.Interfaces;
using cherrys_construction_mvc.Models;
using cherrys_construction_mvc.ViewModels.Requests;
using cherrys_construction_mvc.ViewModels.Responce;

namespace cherrys_construction_mvc.Services
{
    public class TagService : ITagService
    {
        private readonly ILogger<TagService> _logger;
        private readonly IEfRepository<Tag> _tagRepository;
        private readonly IMapper _mapper;

        public TagService(IEfRepository<Tag> tagRepository,
            IMapper mapper,
            ILogger<TagService> logger)
        {
            _mapper = mapper;
            _tagRepository = tagRepository;
            _logger = logger;
        }
        public async Task CreateTagAsync(TagRequest request)
        {
            var tag = _mapper.Map<Tag>(request);
            await _tagRepository.AddAsync(tag);
            await _tagRepository.SaveChangesAsync();
        }

        public async Task DeleteTagAsync(int tagId)
        {
            var tag = await _tagRepository.GetByIdAsync(tagId);
            if(tag == null)
            {
                _logger.LogInformation("Could not find tag to delete in TagService");
            }
            else
            {
                await _tagRepository.DeleteAsync(tag);
                await _tagRepository.SaveChangesAsync();
            }
        }

        public async Task<TagResponce> GetTagByIdAsync(int tagId)
        {
            var tag = await _tagRepository.GetByIdAsync(tagId);
            return _mapper.Map<TagResponce>(tag);
        }

        public async Task<IEnumerable<TagResponce>> GetTagsAsync()
        {
            var tags = await _tagRepository.ListAsync();
            return _mapper.Map<IEnumerable<TagResponce>>(tags);
        }

        public async Task UpdateTagAsync(int tagId, TagRequest request)
        {
            var tagFromDb = await _tagRepository.GetByIdAsync(tagId);
            if(tagFromDb == null)
            {
                _logger.LogWarning("Could not find existing tag in - Tag Service");
            }
            else
            {
                tagFromDb.Name = request.Name;
                await _tagRepository.UpdateAsync(tagFromDb);
                await _tagRepository.SaveChangesAsync();
            }
           
        }
    }
}
