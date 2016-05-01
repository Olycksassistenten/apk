using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;

namespace Olycksassistenten.Helpers
{
    public static class MailHelper
    {
        public static void SendGmail(string toAddress, string subject, string body, string cc = null, List<string> attachments = null)
        {
            var from = new MailAddress("olycksassistenten@gmail.com", "Olycksassistenten");
            var to = new MailAddress(toAddress);

            var mail = new MailMessage(from, to)
            {
                Subject = subject,
                Body = body,
                Priority = MailPriority.High
            };

            if (cc != null && !string.IsNullOrWhiteSpace(cc))
            {
                mail.CC.Add(cc);                
            }

            if (attachments != null && attachments.Any())
            {
                foreach (var attachment in attachments)
                {
                    mail.Attachments.Add(new Attachment(attachment));
                }
            }

            var client = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("olycksassistenten@gmail.com", "o1ycksassistenten")
            };

            client.Send(mail);

            mail.Dispose();
            client.Dispose();
        }

        public static bool IsValid(string emailaddress)
        {
            try
            {
                var address = new MailAddress(emailaddress);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}