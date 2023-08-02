using cherrys_construction_mvc.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace cherrys_construction_mvc.ViewComponents
{
    [ViewComponent]
    public class ServicesViewComponent : ViewComponent
    {
        private readonly IServiceService _servicesService;
        public ServicesViewComponent(IServiceService serviceService)
        {
            _servicesService = serviceService; 
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var services = await _servicesService.GetServicessAsync();
            if(services.Any())
            {
                return View(services);
            }
            else
            {
                return View(null);
            }
        }
    }
}
