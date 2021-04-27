using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using SageraLoans.Models.AppSettingModels;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SageraLoans.Core
{
    public interface IEmailing
    {
        Task sendEmail(string recepient, string message, string msgSubject);
    }

    public class Emailing : IEmailing
    {
        private readonly MailSettingsModel _mailSettings;

        public Emailing(IOptions<MailSettingsModel>mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }
        public async Task sendEmail(string recepient, string message, string msgSubject )
        {
            var senderEmail = _mailSettings.SenderEmail;
            var apiKey = _mailSettings.SendGrid_API_KEY;
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(senderEmail, "Sayo");
            var subject = msgSubject;
            var to = new EmailAddress(recepient, "Folu");
            var plainTextContent = message;
            var htmlContent = $"<strong>{message}</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg).ConfigureAwait(false);
        }
    }
}
