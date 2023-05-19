using Ardalis.Specification;
using cherrys_construction_mvc.Models;

namespace cherrys_construction_mvc.Specification.HeroSliderSpec
{
    public class HeroSliderIncludeFullInfoSpecification : Specification<HeroSlider>, ISingleResultSpecification<HeroSlider>
    {
        public HeroSliderIncludeFullInfoSpecification()
        {
            Query.Include(p => p.Images).AsSplitQuery();
        }
        public HeroSliderIncludeFullInfoSpecification(int id)
        {
            Query.Where(a=> id == a.Id).Include(p => p.Images).AsSplitQuery();
        }
    }
}
