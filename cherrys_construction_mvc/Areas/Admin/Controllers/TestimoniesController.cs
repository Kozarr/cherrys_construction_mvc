using AutoMapper;
using cherrys_construction_mvc.Interfaces;
using cherrys_construction_mvc.Utility;
using cherrys_construction_mvc.ViewModels.Requests;
using cherrys_construction_mvc.ViewModels.Responce;
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
        private readonly IMapper _mapper;
        public TestimoniesController(ITestimonyService testimonyService, 
            IProjectService projectService,
            IMapper mapper)
        {
            _testimonyService = testimonyService;
            _projectService = projectService;
            _mapper = mapper;
        }


        // GET: TestimonyController
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var testimonies = await _testimonyService.GetTestimoniesAsync();
            if (testimonies.Any())
            {
                foreach(var item in testimonies)
                {
                    if(item.ProjectId > 0)
                    {
                        var project = await _projectService.GetProjectByIdAsync(item.ProjectId);
                        item.CurrentProject = project;
                    }                          
                }
                return View(testimonies);
            }
            return View();
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
            TestimonyRequest testimonyRequest = new();
            var listProjects = await _projectService.GetProjectsWithoutTestimonyAsync();
            if (listProjects.Any())
            {
                testimonyRequest.Projects = listProjects.ToList();
            }                     
            return View(testimonyRequest);
        }

        // POST: TestimonyController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([FromForm] TestimonyRequest request)
        {
            if (ModelState.IsValid)
            {
                if(request.Stars > 5)
                {
                    request.Stars = 5;
                }
                if(request.Stars < 0)
                {
                    request.Stars = 0;
                }
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
                if(request.Image == null)
                {
                    request.ImageLink = "\\assets\\img\\user-circle.png";
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
            if (id > 0)
            {
                TestimonyRequest editRequest = new();
                var testimony = await _testimonyService.GetTestimonyByIdAsync(id);
                var currentProject = await _projectService.GetProjectByIdAsync(testimony.ProjectId);
                if(testimony!= null)
                {
                    editRequest = _mapper.Map<TestimonyRequest>(testimony);
                }
                if(currentProject != null)
                {
                    editRequest.CurrentProject = currentProject;
                }
                return View(editRequest);
            }
            return View();
        }

        // POST: TestimonyController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([FromForm] TestimonyRequest request,int id)
        {
            if (ModelState.IsValid)
            {
                if (request.Stars > 5)
                {
                    request.Stars = 5;
                }
                if (request.Stars < 0)
                {
                    request.Stars = 0;
                }
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
