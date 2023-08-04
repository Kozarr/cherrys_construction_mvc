using cherrys_construction_mvc.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace cherrys_construction_mvc.ViewComponents
{
    public class TestimonialsViewComponent : ViewComponent
    {
        private readonly ITestimonyService _testimonyService;
        public TestimonialsViewComponent(ITestimonyService testimonyService)
        {
            _testimonyService = testimonyService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var testimonies = await _testimonyService.GetTestimoniesAsync();
            if(testimonies.Any())
            {
                return View(testimonies);
            }
            else
            {
                return View(null);
            }
        }
    }
}
