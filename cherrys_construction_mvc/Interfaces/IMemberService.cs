using cherrys_construction_mvc.ViewModels.Requests;
using cherrys_construction_mvc.ViewModels.Responce;

namespace cherrys_construction_mvc.Interfaces
{
    public interface IMemberService
    {
        Task CreateMemberAsync(MemberRequest request);
        Task<IEnumerable<MemberResponce>> GetMemberssAsync();
        Task DeleteMemberAsync(int memberId);
        Task<MemberResponce> GetMemberByIdAsync(int memberId);
        Task UpdateMemberAsync(int memberId, MemberRequest request);

    }
}
