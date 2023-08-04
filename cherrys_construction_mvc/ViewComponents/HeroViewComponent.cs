using cherrys_construction_mvc.Interfaces;
using cherrys_construction_mvc.ViewModels.Responce;
using Microsoft.AspNetCore.Mvc;

namespace cherrys_construction_mvc.ViewComponents
{
    public class HeroViewComponent : ViewComponent
    {
        private readonly IHeroSliderService _hero;
        public HeroViewComponent(IHeroSliderService hero)
        {
            _hero = hero;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            HeroSliderResponce hero = new();
            var heroesFromDb = await _hero.GetHeroSlidersAsync();
            if(heroesFromDb.Any())
            {
                var pickedHero = heroesFromDb.FirstOrDefault();
                if(pickedHero != null)
                {
                    hero = pickedHero;
                }
            }
            return View(hero);
        }
    }
}
