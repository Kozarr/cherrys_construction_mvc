using cherrys_construction_mvc.Interfaces;
using cherrys_construction_mvc.ViewModels.FooterComponentVM;
using Microsoft.AspNetCore.Mvc;

namespace cherrys_construction_mvc.ViewComponents
{
    public class FooterViewComponent : ViewComponent
    {
        private readonly ICompanyInfoService _companyInfoService;
        private readonly ILegalDocumentService _legal;

        public FooterViewComponent(ICompanyInfoService companyInfoService, ILegalDocumentService legal)
        {
            _companyInfoService = companyInfoService;
            _legal = legal;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var compInfo = await _companyInfoService.GetCompanyInfosAsync();
            var legalDocs = await _legal.GetLegalDocumentsAsync();
            FooterCVM Footer = new();
            if (compInfo.Any())
            {
                Footer.CompanyInfo = compInfo.ToList().First();              
            }
            if (legalDocs.Any())
            {
                Footer.LegalDocuments = legalDocs;
            }

            return View(Footer);
        }
    }
}
