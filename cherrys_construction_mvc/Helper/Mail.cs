using cherrys_construction_mvc.Interfaces;
using cherrys_construction_mvc.Utility;
using cherrys_construction_mvc.ViewModels.Responce;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace cherrys_construction_mvc.Helper
{
    public class Mail
    {
        private readonly ICompanyInfoService _companyInfoService;
        private readonly ILogger<Mail> _logger;
        public Mail(
            ICompanyInfoService companyInfoService, 
            ILogger<Mail> logger)
        {
            _companyInfoService = companyInfoService;
            _logger = logger;
        }

        // Send Grid Send Email

        public async Task SendGridEmail(IFormFile formFile, CallToActionMessageResponce message, CompanyInfoResponce passedInfo)
        {
            CompanyInfoResponce info = new();

            if (passedInfo == null)
            {
                var checker = await _companyInfoService.GetCompanyInfosAsync();
                if (checker.Any())
                {
                    info = checker.First();
                }
            }
            else
            {
                info = passedInfo;
            }

            try
            {
                var client = new SendGridClient(StaticDetails.SendGridKey);
                SendGridMessage msg = new SendGridMessage();

                var to = new EmailAddress(info.CompanyEmail, info.CompanyName);
                var from = new EmailAddress(info.CompanyEmail, message.Name);
                msg.AddTo(to);
                msg.From = from;
                msg.Subject = message.Subject;
                msg.HtmlContent = message.Body;
                var replyTo = new EmailAddress(message.Email, message.Name);
                msg.ReplyTo = replyTo;
                var response = await client.SendEmailAsync(msg);
            }
            catch (Exception)
            {
                _logger.LogWarning("Failed to send Mail with send grid in - Mail Helper");
                throw;
            }
        }

    }
}
