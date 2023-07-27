using cherrys_construction_mvc.Interfaces;
using cherrys_construction_mvc.Utility;
using cherrys_construction_mvc.ViewModels.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace cherrys_construction_mvc.Areas.Admin.Controllers
{
    [Area(Breadcrumb.AdminArea)]
    [Authorize(Roles = StaticDetails.Role_Admin + "," + StaticDetails.Role_Employee)]
    public class TagsController : Controller
    {
        private readonly ITagService _tagService;
        public TagsController(ITagService tagService)
        {
           _tagService = tagService;
        }
        public async Task<IActionResult> Index()
        {
            var tags = await _tagService.GetTagsAsync();
            return View(tags);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] TagRequest request)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrWhiteSpace(request.Name))
                {
                    request.Name = request.Name.Trim();
                }
                await _tagService.CreateTagAsync(request);
                TempData["success"] = "New Tag Added Successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["error"] = "Tag Failed To Add";
                return RedirectToAction(nameof(Index));
            }

        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var tag = await _tagService.GetTagByIdAsync(id);
            var editRequest = new TagRequest()
            {
                Name = tag.Name,

            };

            return View(editRequest);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Edit([FromForm] TagRequest request, int id)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrWhiteSpace(request.Name))
                {
                    request.Name = request.Name.Trim();
                }
                await _tagService.UpdateTagAsync(id, request);
                TempData["success"] = "Tag Updated Successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["error"] = "Tag Failed To Update";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var tag = await _tagService.GetTagByIdAsync(id);
            return View(tag);
        }

        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteTag(int id)
        {
            if (ModelState.IsValid)
            {
                await _tagService.DeleteTagAsync(id);
                TempData["success"] = "Tag Deleted Successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["error"] = "Tag Failed To Delete";
                return RedirectToAction(nameof(Index));
            }

        }
    }
}
