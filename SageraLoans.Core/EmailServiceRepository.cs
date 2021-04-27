using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SageraLoans.Core
{
    public class EmailServiceRepository:IEmailServiceRepository
    {
        // MailKit and MimeKit to send message in C#
        public bool SendEmailToConfirm(string userEmail, string confirmationLink)
        {
            //instantiates the MailMessage class
            //gets the system email and user email
            MailMessage mailMessage = new MailMessage {From = new MailAddress("angeloakuhwak@gmail.com") };
            mailMessage.To.Add(new MailAddress(userEmail));


            //sets email content
            mailMessage.Subject = "Confirm your email";
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = confirmationLink;

            //sets up smtp client
            SmtpClient client = new SmtpClient
            {
                UseDefaultCredentials = false,
                EnableSsl = true,
                Host = "smtp.gmail.com",
                Port = 587,
                Credentials = new System.Net.NetworkCredential("angeloakuhwak@gmail.com", "january27@@"),
                DeliveryMethod = SmtpDeliveryMethod.Network
            };

            try
            {
                //send mail
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

        // MailKit and MimeKit to send message in C#
        public bool SendEmailToReset(string userEmail, string Link)
        {
            MailMessage mailMessage = new MailMessage { From = new MailAddress("angeloakuhwak@gmail.com") };
            mailMessage.To.Add(new MailAddress(userEmail));
            mailMessage.Subject = "Click on the link to Reset your password";
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = Link;
            SmtpClient client = new SmtpClient
            {
                UseDefaultCredentials = false,
                EnableSsl = true,
                Host = "smtp.gmail.com",
                Port = 587,
                Credentials = new System.Net.NetworkCredential("angeloakuhwak@gmail.com", "january27@@"),
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
