using cherrys_construction_mvc.Interfaces;
using cherrys_construction_mvc.ViewModels.ComponentVMs;
using Microsoft.AspNetCore.Mvc;

namespace cherrys_construction_mvc.ViewComponents
{
    public class QualitiesViewComponent : ViewComponent
    {
        private readonly ICompanyQualitiySettingService _settingService;
        private readonly ICompanyQualityService _qualityService;
        public QualitiesViewComponent(ICompanyQualitiySettingService settingService,
            ICompanyQualityService qualityService)
        {
            _qualityService = qualityService;
            _settingService = settingService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            QualitiesCVM component = new();
            var settingList = await _settingService.GetCompanyQualitiesSettingsAsync();
            if(settingList.Any())
            {
                component.CompanyQualitiesSettings = settingList.ToList().FirstOrDefault();
            }
            var qualities = await _qualityService.GetCompanyQualitiesAsync();
            if (qualities.Any())
            {
                component.Qualities = qualities;
            }
            return View(component);
        }
    }
}
