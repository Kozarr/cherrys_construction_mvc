using cherrys_construction_mvc.Interfaces;
using cherrys_construction_mvc.ViewModels.Responce;
using Microsoft.AspNetCore.Mvc;

namespace cherrys_construction_mvc.ViewComponents
{
    public class TeamViewComponent : ViewComponent
    {
        private readonly IMemberService _memberService;
        public TeamViewComponent(IMemberService memberService)
        {
            _memberService = memberService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<MemberResponce> team = new();
            var members = await _memberService.GetMembersAsync();
            if(members.Any())
            {
                team = members.ToList();
            }
            return View(team);
        }
    }
}
