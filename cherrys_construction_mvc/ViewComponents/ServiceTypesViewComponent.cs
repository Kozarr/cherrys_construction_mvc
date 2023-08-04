using cherrys_construction_mvc.Interfaces;
using cherrys_construction_mvc.ViewModels.Responce;
using Microsoft.AspNetCore.Mvc;

namespace cherrys_construction_mvc.ViewComponents
{
    public class ServiceTypesViewComponent : ViewComponent
    {
        private readonly IServiceTypeService _service;
        public ServiceTypesViewComponent(IServiceTypeService service)
        {
            _service = service;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<ServiceTypeResponce> services = new();
            var servicesFromDb = await _service.GetServiceTypesAsync();
            if(servicesFromDb.Any())
            {
                services = servicesFromDb.ToList();
            }
            return View(services);
        }
    }
}
