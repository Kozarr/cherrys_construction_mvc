using cherrys_construction_mvc.Models;
using cherrys_construction_mvc.ViewModels.Responce;

namespace cherrys_construction_mvc.ViewModels.Contact
{
    public class ContactViewModel
    {
        public CompanyInfoResponce? CompanyInfo { get; set; }
        public CallToActionMessageResponce? Message { get; set; }  
        public IFormFile? Attachment { get; set; }

    }
}
