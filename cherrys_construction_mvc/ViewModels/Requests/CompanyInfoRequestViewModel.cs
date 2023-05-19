namespace cherrys_construction_mvc.ViewModels.Requests
{
    public class CompanyInfoRequestViewModel
    {
        public CompanyInfoRequest? CompanyInfo { get; set; }
        public IFormFile? NavigationImage { get; set; }
        public IFormFile? FooterImage { get; set; }
    }
}
