using cherrys_construction_mvc.Interfaces;
using cherrys_construction_mvc.ViewModels.Responce;
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
            List<TestimonyResponce> reviews = new();
            var testimonies = await _testimonyService.GetTestimoniesAsync();
            if(testimonies.Any())
            {
                reviews = testimonies.ToList();
            }
            return View(reviews);
        }
    }
}
