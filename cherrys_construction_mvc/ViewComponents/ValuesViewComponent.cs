using cherrys_construction_mvc.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace cherrys_construction_mvc.ViewComponents
{
    public class ValuesViewComponent : ViewComponent
    {
        private readonly ICompanyValueService _valuesService;
        public ValuesViewComponent(ICompanyValueService valuesService)
        {
            _valuesService = valuesService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _valuesService.GetCompanyValuesAsync();
            if(values.Any())
            {
                return View(values);
            }
            return View(null);
        }
    }
}
