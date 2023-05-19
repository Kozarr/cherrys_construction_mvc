using cherrys_construction_mvc.ViewModels.Requests;
using cherrys_construction_mvc.ViewModels.Responce;

namespace cherrys_construction_mvc.Interfaces
{
    public interface ITagService
    {
        Task<IEnumerable<TagResponce>> GetTagsAsync();
        Task<TagResponce> GetTagByIdAsync(int tagId);
        Task CreateTagAsync(TagRequest request);
        Task UpdateTagAsync(int tagId, TagRequest request);
        Task DeleteTagAsync(int tagId);
    }
}
