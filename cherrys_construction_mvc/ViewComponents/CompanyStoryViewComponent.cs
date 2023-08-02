using cherrys_construction_mvc.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace cherrys_construction_mvc.ViewComponents
{
    [ViewComponent]
    public class CompanyStoryViewComponent : ViewComponent
    {
        private readonly ICompanyStoryService _storyService;
        public CompanyStoryViewComponent(ICompanyStoryService storyService)
        {
            _storyService = storyService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var stories = await _storyService.GetCompanyStoriesAsync();
            if(stories.Any())
            {
                var story = stories.FirstOrDefault();
                return View(story);
            }
            else
            {
                return View(null);
            }
        }
    }
}
