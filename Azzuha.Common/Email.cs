using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Text;

namespace Azzuha.Common
{
    public class Email
    {
        public static bool SendEmail(string subject, string body, string from, string password, string to, byte[] attachment, string fileName)
        {
            try
            {
                bool res = false;

                string[] tempfrom = from.Split('@');

                MailMessage mail = new MailMessage();
                mail.To.Add(to);
                mail.From = new MailAddress(from, "Fitcentr", Encoding.UTF8);
                mail.Subject = subject;
                mail.SubjectEncoding = Encoding.UTF8;
                mail.Body = body;
                mail.BodyEncoding = Encoding.UTF8;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;

                //Attachments 
                if (attachment != null)
                {
                    mail.Attachments.Add(new Attachment(new MemoryStream(attachment), fileName + ".pdf", "application/pdf"));
                }

                SmtpClient client = new SmtpClient();
                //Add the Creddentials- use your own email id and password

                client.Credentials = new System.Net.NetworkCredential(from, password);

                // Gmail works on this port
                if (tempfrom[1] == "gmail.com")
                {
                    client.Host = "smtp.gmail.com";
                    client.Port = 587;
                }
                else if (tempfrom[1] == "yahoo.com")
                {
                    client.Port = 465;
                    client.Host = "smtp.mail.yahoo.com";
                }
              
                else
                {
                    client.Host = "mail.fitcentr.com";
                    client.Port = 25;
                }
                //client.EnableSsl = true; //Gmail works on Server Secured Layer
                client.Send(mail);
                res = true;
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
