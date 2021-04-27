using System;
using System.Net.Mail;
using System.Threading.Tasks;

namespace SageraLoans.Utilities
{
    public class EmailHelper
    {
        // MailKit and MimeKit to send message in C#
        public async Task<bool> SendEmail(string userEmail, string Link)
        {
            MailMessage mailMessage = new MailMessage { From = new MailAddress("physionode@gmail.com") };
            mailMessage.To.Add(new MailAddress(userEmail));
            mailMessage.Subject = "Password Reset";
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = Link;
            SmtpClient client = new SmtpClient
            {
                UseDefaultCredentials = false,
                EnableSsl = true,
                Host = "smtp.gmail.com",
                Port = 587,
                Credentials = new System.Net.NetworkCredential("physionode@gmail.com", "january27@@"),
                DeliveryMethod = SmtpDeliveryMethod.Network
            };
            try
            {
                client.Send(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                // log exception
                Console.WriteLine(ex.Message);
            }
            return false;
        }
    }
}
