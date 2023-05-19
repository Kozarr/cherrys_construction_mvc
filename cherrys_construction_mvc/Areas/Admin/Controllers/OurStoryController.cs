using cherrys_construction_mvc.Interfaces;
using cherrys_construction_mvc.Utility;
using cherrys_construction_mvc.ViewModels.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace cherrys_construction_mvc.Areas.Admin.Controllers
{
    [Area(Breadcrumb.AdminArea)]
    [Authorize(Roles = StaticDetails.Role_Admin + "," + StaticDetails.Role_Employee)]
    public class OurStoryController : Controller
    {
        private readonly ICompanyStoryService _companyStoryService;
       // private readonly IProjectTagService _projectTagService;
        public OurStoryController(ICompanyStoryService companyStoryService)
        {
            _companyStoryService = companyStoryService;
            
        }
        // GET: CompanyStoryController
        public async Task<ActionResult> Index()
        {
            //var data = new ProjectTagRequest()
            //{
            //    TagId = 2,
            //    ProjectId = 2,
            //};
            //await _projectTagService.CreateProjectTagAsync(data);
            var companyStories = await _companyStoryService.GetCompanyStoriesAsync();
            return View(companyStories);
        }

        // GET: CompanyStoryController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var companyStory = await _companyStoryService.GetCompanyStoryByIdAsync(id);
            return View(companyStory);
        }

        // GET: CompanyStoryController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CompanyStoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([FromForm] CompanyStoryRequest request)
        {
            if(ModelState.IsValid) { 

                var checker = await _companyStoryService.GetCompanyStoriesAsync();
                if (!checker.Any())
                {
                    await _companyStoryService.CreateCompanyStoryAsync(request);
                    TempData["success"] = "Story Added Successfully";
                }
                else
                {
                    TempData["error"] = "Story Already Exists";
                }
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["error"] = "Story Failed To Add";
                return View();
            }
        }

        // GET: CompanyStoryController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var companyStory = await _companyStoryService.GetCompanyStoryByIdAsync(id);
            var editRequest = new CompanyStoryRequest()
            {
                ArticleDescription = companyStory.ArticleDescription,
                ArticleTitle = companyStory.ArticleTitle,
                ArticleSmallText = companyStory.ArticleSmallText,
                ImageLink = companyStory.ImageLink,
                Title = companyStory.Title
            };
            return View(editRequest);
        }

        // POST: CompanyStoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([FromForm]CompanyStoryRequest request,int id)
        {
            if (ModelState.IsValid) 
            { 
                await _companyStoryService.UpdateCompanyStoryAsync(id,request);
                TempData["success"] = "Story Updated Successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["error"] = "Story Failed To Update";
                return View();
            }
        }

        // GET: CompanyStoryController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var companyStory = await _companyStoryService.GetCompanyStoryByIdAsync(id);
            return View(companyStory);
        }

        // POST: CompanyStoryController/Delete/5
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteCompanyStory(int id)
        {
            try
            {
                await _companyStoryService.DeleteCompanyStoryAsync(id);
                TempData["success"] = "Story Deleted Successfully";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["error"] = "Story Failed To Delete";
                return View();
            }
        }
    }
}
