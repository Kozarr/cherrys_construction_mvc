using cherrys_construction_mvc.Interfaces;
using cherrys_construction_mvc.ViewModels.Responce;
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
            List<CompanyValueResponce> values = new();
            var valuesFromDb = await _valuesService.GetCompanyValuesAsync();
            if(valuesFromDb.Any())
            {
                values = valuesFromDb.ToList();
            }
            return View(values);
        }
    }
}
