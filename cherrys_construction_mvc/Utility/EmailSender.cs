using Microsoft.AspNetCore.Identity.UI.Services;
using MimeKit;
using MailKit.Net.Smtp;
using cherrys_construction_mvc.Models;

namespace cherrys_construction_mvc.Utility
{
    public class EmailSender : IEmailSender
    {
         public Task SendEmailAsync(string email, string subject, string htmlMessage)
         {
            try
            {
                // Building Email Contents
                var emailToSend = new MimeMessage();
                emailToSend.From.Add(MailboxAddress.Parse("xurnet.test@outlook.com"));
                emailToSend.To.Add(MailboxAddress.Parse(email));
                emailToSend.Subject = subject;
                emailToSend.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = htmlMessage };

                // Send Email
                using (var emailClient = new SmtpClient())
                {
                    // configurating the connection to gmail server 
                    emailClient.Connect("smtp.office365.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                    emailClient.Authenticate("xurnet.test@outlook.com", "%123Test$789*Email!@");
                    emailClient.Send(emailToSend);
                    emailClient.Disconnect(true);
                }

                return Task.CompletedTask;
            }
            catch (Exception)
            {
                throw;
            }
         }
    }
}
