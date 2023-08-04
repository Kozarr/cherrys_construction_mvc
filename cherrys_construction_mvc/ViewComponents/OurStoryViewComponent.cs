using cherrys_construction_mvc.Interfaces;
using cherrys_construction_mvc.ViewModels.Responce;
using Microsoft.AspNetCore.Mvc;

namespace cherrys_construction_mvc.ViewComponents
{
    [ViewComponent]
    public class OurStoryViewComponent : ViewComponent
    {
        private readonly ICompanyStoryService _storyService;
        public OurStoryViewComponent(ICompanyStoryService storyService)
        {
            _storyService = storyService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            CompanyStoryResponce story = new();
            var stories = await _storyService.GetCompanyStoriesAsync();
            if (stories.Any())
            {
                var pickedStory = stories.FirstOrDefault();
                if (pickedStory != null)
                {
                    story = pickedStory;
                }
            }
            return View(story);
        }
    }
}
