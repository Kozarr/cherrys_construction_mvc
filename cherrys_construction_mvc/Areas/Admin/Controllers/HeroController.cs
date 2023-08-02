using cherrys_construction_mvc.Interfaces;
using cherrys_construction_mvc.Utility;
using cherrys_construction_mvc.ViewModels.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace cherrys_construction_mvc.Areas.Admin.Controllers
{
    [Area(Breadcrumb.AdminArea)]
    [Authorize(Roles = StaticDetails.Role_Admin + "," + StaticDetails.Role_Employee)]
    public class HeroController : Controller
    {

        private readonly IHeroSliderService _heroSliderService;
        private readonly ILogger<HeroController> _logger;
        public HeroController(IHeroSliderService heroSliderService,
            ILogger<HeroController> logger)
        {
            _heroSliderService = heroSliderService;
            _logger = logger;
        }


        // GET: HeroSliderController
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var settingsInDb = await _heroSliderService.GetHeroSlidersAsync();
            if (settingsInDb.Any())
            {
                var settingList = settingsInDb.ToList();
                var returnItem = settingList[0];
                return View(returnItem);
            }
            return View();
        }

        // GET: HeroSliderController/Details/5
        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            var slider = await _heroSliderService.GetHeroSliderByIdAsync(id);
            return View(slider);
        }

        // GET: HeroSliderController/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: HeroSliderController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([FromForm] HeroSliderRequest request)
        {
            try
            {
                var checker = await _heroSliderService.GetHeroSlidersAsync();
                if (!checker.Any())
                {
                    if (!string.IsNullOrWhiteSpace(request.Description))
                    {
                        request.Description = request.Description.Trim();
                    }
                    if (!string.IsNullOrWhiteSpace(request.Title))
                    {
                        request.Title = request.Title.Trim();
                    }
                    if (!string.IsNullOrWhiteSpace(request.ButtonText))
                    {
                        request.ButtonText = request.ButtonText.Trim();
                    }

                    await _heroSliderService.CreateHeroSliderAsync(request);
                    TempData["success"] = "Hero Slides Added Successfully";
                }
                else
                {
                    TempData["error"] = "Slider Already Exists";
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["error"] = "Hero Slides Failed To Add";
                return View();
            }
        }

        // GET: HeroSliderController/Edit/5
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            var slider = await _heroSliderService.GetHeroSliderByIdAsync(id);
            if(slider != null)
            {
                var editRequest = new HeroSliderRequest()
                {
                    ButtonText = slider.ButtonText,
                    Description = slider.Description,
                    Title = slider.Title,
                    ActiveImages = slider.Images
                };
                return View(editRequest);
            }
            return View(null);
        }

        // POST: HeroSliderController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([FromForm] HeroSliderRequest request, int id, IFormCollection collection)
        {
            if (ModelState.IsValid) 
            { 
                var selectedDeletePhoto = collection["checkPhoto"].ToList();
                List<int> SelectedDeletePhotoIds = new List<int>();
                if (selectedDeletePhoto.Any())
                {
                    foreach (var item in selectedDeletePhoto)
                    {
                        if (item != null)
                        {
                            SelectedDeletePhotoIds.Add(int.Parse(item));
                        }
                        else
                        {
                            _logger.LogInformation("Cant find selected photo to add to delete list in - Hero Controller");
                        }
                        
                    }
                    request.SelectedDeletePhoto = SelectedDeletePhotoIds;
                }
                if (!string.IsNullOrWhiteSpace(request.Description))
                {
                    request.Description = request.Description.Trim();
                }
                if (!string.IsNullOrWhiteSpace(request.Title))
                {
                    request.Title = request.Title.Trim();
                }
                if (!string.IsNullOrWhiteSpace(request.ButtonText))
                {
                    request.ButtonText = request.ButtonText.Trim();
                }
                await _heroSliderService.UpdateHeroSliderAsync(id, request);
                TempData["success"] = "Hero Slides Updated Successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["error"] = "Hero Slides Failed To Update";
                return View();
            }
        }

        // GET: HeroSliderController/Delete/5
        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            var slider = await _heroSliderService.GetHeroSliderByIdAsync(id);
            return View(slider);
        }

        // POST: HeroSliderController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteSlider(int id)
        {
            try
            {
                await _heroSliderService.DeleteHeroSliderAsync(id);
                TempData["success"] = "Hero Slides Deleted Successfully";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["success"] = "Hero Slides Failed To Delete";
                return View();
            }
        }
    }
}
