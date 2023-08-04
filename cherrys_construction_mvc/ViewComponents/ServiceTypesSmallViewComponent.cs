using cherrys_construction_mvc.Interfaces;
using cherrys_construction_mvc.ViewModels.Responce;
using Microsoft.AspNetCore.Mvc;

namespace cherrys_construction_mvc.ViewComponents
{
    public class ServiceTypesSmallViewComponent : ViewComponent
    {
        private readonly IServiceTypeService _serviceType;
        public ServiceTypesSmallViewComponent(IServiceTypeService serviceType)
        {
            _serviceType = serviceType;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<ServiceTypeResponce> services = new();
            var serviceTypesFromDb = await _serviceType.GetServiceTypesAsync();
            if(serviceTypesFromDb.Any())
            {
                services = serviceTypesFromDb.ToList();
                return View(services);
            }
            return View(services);
        }
    }
}
