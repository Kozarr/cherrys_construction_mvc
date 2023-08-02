
using cherrys_construction_mvc.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace cherrys_construction_mvc.ViewComponents
{
    public class MembersViewComponent : ViewComponent
    {
        private readonly IMemberService _memberService;
        public MembersViewComponent(IMemberService memberService)
        {
            _memberService = memberService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var members = await _memberService.GetMemberssAsync();
            if (members.Any())
            {
                return View(members);
            }
            else
            {
                return View(null);
            }
        }
    }
}
