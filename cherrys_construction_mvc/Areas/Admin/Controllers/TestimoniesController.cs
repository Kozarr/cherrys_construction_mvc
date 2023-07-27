using cherrys_construction_mvc.Interfaces;
using cherrys_construction_mvc.Utility;
using cherrys_construction_mvc.ViewModels.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace cherrys_construction_mvc.Areas.Admin.Controllers
{
    [Area(Breadcrumb.AdminArea)]
    [Authorize(Roles = StaticDetails.Role_Admin + "," + StaticDetails.Role_Employee)]
    public class TestimoniesController : Controller
    {

        private readonly ITestimonyService _testimonyService;
        private readonly IProjectService _projectService;
        public TestimoniesController(ITestimonyService testimonyService, IProjectService projectService)
        {
            _testimonyService = testimonyService;
            _projectService = projectService;
        }


        // GET: TestimonyController
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var testimonies = await _testimonyService.GetTestimonysAsync();
            return View(testimonies);
        }

        // GET: TestimonyController/Details/5
        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            var testimony = await _testimonyService.GetTestimonyByIdAsync(id);
            return View(testimony);
        }

        // GET: TestimonyController/Create
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            TestimonyRequest testimonyRequest = new()
            {
                Projects = (List<ViewModels.Responce.ProjectResponce>)await _projectService.GetProjectsWithoutTestimonyAsync()
            };

            return View(testimonyRequest);
        }

        // POST: TestimonyController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([FromForm] TestimonyRequest request)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrWhiteSpace(request.Name))
                {
                    request.Name = request.Name.Trim();
                }
                if (!string.IsNullOrWhiteSpace(request.Description))
                {
                    request.Description = request.Description.Trim();
                }
                if (!string.IsNullOrWhiteSpace(request.Position))
                {
                    request.Position = request.Position.Trim();
                }
                await _testimonyService.CreateTestimonyAsync(request);
                TempData["success"] = "Testimony Added Successfully";
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: TestimonyController/Edit/5
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            var testimony = await _testimonyService.GetTestimonyByIdAsync(id);
            var projectName = await _projectService.GetProjectByIdAsync(testimony.ProjectId);
            var editRequest = new TestimonyRequest()
            {
                Position = testimony.Position,
                Stars = testimony.Stars,
                ProjectId = testimony.ProjectId,
                Name = testimony.Name,
                ImageLink =  testimony.ImageLink,
                Description = testimony.Description,
                Projects = (List<ViewModels.Responce.ProjectResponce>)await _projectService.GetProjectsWithoutTestimonyAsync(),
            };
            if (projectName != null && projectName.Title != null)
            {
                editRequest.ProjectName = projectName.Title;
            }
            return View(editRequest);
        }

        // POST: TestimonyController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([FromForm] TestimonyRequest request,int id)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrWhiteSpace(request.Name))
                {
                    request.Name = request.Name.Trim();
                }
                if (!string.IsNullOrWhiteSpace(request.Description))
                {
                    request.Description = request.Description.Trim();
                }
                if (!string.IsNullOrWhiteSpace(request.Position))
                {
                    request.Position = request.Position.Trim();
                }
                await _testimonyService.UpdateTestimonyAsync(id, request);
                TempData["success"] = "Testimony Updated Successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["error"] = "Testimony Failed To Update";
                return View();
            }
        }

        // GET: TestimonyController/Delete/5
        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            var testimony = await _testimonyService.GetTestimonyByIdAsync(id);
            return View(testimony);
        }

        // POST: TestimonyController/Delete/5
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteTestimony(int id)
        {
            if (id > 0)
            {               
                await _testimonyService.DeleteTestimonyAsync(id);
                TempData["success"] = "Testimony Deleted Successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["error"] = "Testimony Failed To Delete";
                return View();
            }
        }
    }
}
