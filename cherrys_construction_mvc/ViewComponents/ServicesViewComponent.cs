using cherrys_construction_mvc.Interfaces;
using cherrys_construction_mvc.ViewModels.Responce;
using Microsoft.AspNetCore.Mvc;

namespace cherrys_construction_mvc.ViewComponents
{
    public class ServicesViewComponent : ViewComponent
    {
        private readonly IServiceService _service;
        public ServicesViewComponent(IServiceService service)
        {
            _service = service;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<ServiceResponce> services = new();
            var servicesFromDb = await _service.GetServicesAsync();
            if(servicesFromDb.Any()) 
            {
                services = servicesFromDb.ToList();
            }
            return View(services);
        }
    }
}
