using cherrys_construction_mvc.Interfaces;
using cherrys_construction_mvc.ViewModels.Responce;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace cherrys_construction_mvc.Utility
{
    public class EmailSender : IEmailSender
    {
        private readonly ILogger _logger;
        private readonly ICompanyInfoService _companyInfo;

        public EmailSender(IOptions<AuthMessageSenderOptions> optionsAccessor,
                           ILogger<EmailSender> logger,
                           ICompanyInfoService companyInfo)
        {
            Options = optionsAccessor.Value;
            _logger = logger;
            _companyInfo = companyInfo;
        }

        public AuthMessageSenderOptions Options { get; } //Set with Secret Manager.

        public async Task SendEmailAsync(string toEmail, string subject, string message)
        {
            if (string.IsNullOrEmpty(StaticDetails.SendGridKey))
            {
                throw new Exception("Null SendGridKey");
            }
            await Execute(StaticDetails.SendGridKey, subject, message, toEmail);
        }

        public async Task Execute(string apiKey, string subject, string message, string toEmail)
        {
            var webInfo = await _companyInfo.GetCompanyInfosAsync();
            CompanyInfoResponce info = new();
            if (webInfo.Any())
            {
                info = webInfo.First();   
            }
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(info.CompanyEmail, info.CompanyName),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };
            msg.AddTo(new EmailAddress(toEmail));

            // Disable click tracking.
            // See https://sendgrid.com/docs/User_Guide/Settings/tracking.html
            msg.SetClickTracking(false, false);
            var response = await client.SendEmailAsync(msg);
            _logger.LogInformation(response.IsSuccessStatusCode
                                   ? $"Email to {toEmail} queued successfully!"
                                   : $"Failure Email to {toEmail}");
        }
    }
}
