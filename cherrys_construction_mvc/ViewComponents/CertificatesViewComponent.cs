using cherrys_construction_mvc.Interfaces;
using cherrys_construction_mvc.ViewModels.ComponentVMs;
using Microsoft.AspNetCore.Mvc;

namespace cherrys_construction_mvc.ViewComponents
{
    public class CertificatesViewComponent : ViewComponent
    {
        private readonly ICompanyCertificateService _certificateService;
        private readonly ICompanyCertificateSettingService _certificateSettingService;
        public CertificatesViewComponent(ICompanyCertificateService certificateService,
            ICompanyCertificateSettingService certificateSettingService)
        {
            _certificateService = certificateService;
            _certificateSettingService = certificateSettingService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            CertificateCVM CVM = new();
            var certList = await _certificateSettingService.GetCompanyCertificateSettingsAsync();
            var certificates = await _certificateService.GetCertificatesAsync();           
            if (certList.Any())
            {
                var certSetting = certList.ToList().FirstOrDefault();
                CVM.CertSettings = certSetting;
            }

            if (certificates.Any())
            {
                CVM.CompanyCertificates = certificates;
            }
            return View(CVM);

        }
    }
}
