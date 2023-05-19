using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace cherrys_construction_mvc.ViewModels.Requests
{
    public class CompanyInfoRequestViewModel
    {
        public CompanyInfoRequest? CompanyInfo { get; set; }
        [ValidateNever]
        public IFormFile? NavigationImage { get; set; }
        [ValidateNever]
        public IFormFile? FooterImage { get; set; }
    }
}
