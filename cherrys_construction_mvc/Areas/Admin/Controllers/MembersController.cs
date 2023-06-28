using cherrys_construction_mvc.Interfaces;
using cherrys_construction_mvc.Utility;
using cherrys_construction_mvc.ViewModels.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace cherrys_construction_mvc.Areas.Admin.Controllers
{
    [Area(Breadcrumb.AdminArea)]
    [Authorize(Roles = StaticDetails.Role_Admin + "," + StaticDetails.Role_Employee)]
    public class MembersController : Controller
    {
        private readonly IMemberService _memberService;
        private readonly ILogger<MembersController> _logger;
        public MembersController(IMemberService memberService, 
            ILogger<MembersController> logger)
        {
            _memberService = memberService;
            _logger = logger;
        }


        // GET : CREATE LIST
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var members = await _memberService.GetMemberssAsync();
            return View(members);
        }

        // GET : CREATE FORM
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST : CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] MemberRequest request)
        {    
            // for test 
            if (request.Name != null)
            {
                if (string.IsNullOrWhiteSpace(request.InstagramLink))
                {
                    request.InstagramLink = "";
                }
                await _memberService.CreateMemberAsync(request);
                TempData["success"] = "New Team Member Created Successfully";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["error"] = "Team Member Creation Failed";
                return View();
            }
            //var members = await _memberService.GetMemberssAsync();
        }

        // GET : EDIT (~Update)
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {

            var member = await _memberService.GetMemberByIdAsync(id);
            if(member != null)
            {
                MemberRequest editRequest = new ();
                if(!string.IsNullOrWhiteSpace(member.Name))
                {
                    editRequest.Name = member.Name.Trim();
                }
                if (!string.IsNullOrWhiteSpace(member.Description))
                {
                    editRequest.Description = member.Description.Trim();
                }
                if (!string.IsNullOrWhiteSpace(member.InstagramLink))
                {
                    editRequest.InstagramLink = member.InstagramLink.Trim();
                }
                if (!string.IsNullOrWhiteSpace(member.Role))
                {
                    editRequest.Role = member.Role.Trim();
                }
                if (!string.IsNullOrWhiteSpace(member.ImageLink))
                {
                    editRequest.ImageLink = member.ImageLink;
                }
                return View(editRequest);
            }
            else
            {
                _logger.LogWarning("Could not find existing member to display in edit form, in - MembersController");
                return RedirectToAction(nameof(Index));
            }
        }

        // POST : EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(MemberRequest request,int id)
        {
            if (!ModelState.IsValid)
            {

                if (string.IsNullOrWhiteSpace(request.InstagramLink))
                {
                    request.InstagramLink = "";
                }
                await _memberService.UpdateMemberAsync(id, request);
                TempData["success"] = "Team Member Updated Successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["error"] = "Team Member Failed To Update";
                return RedirectToAction(nameof(Index));
            }

        }

        // GET : DELETE
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var member = await _memberService.GetMemberByIdAsync(id);
            return View(member);
        }

        // POST : DELETE
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteMember(int id)
        {
            if(id != 0) { 
                await _memberService.DeleteMemberAsync(id);
                TempData["success"] = "Team Member Deleted Successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["success"] = "Team Member Failed To Delete";
                return View();
            }
        }
    }
}
