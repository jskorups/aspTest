using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace Test_21032019
{
    public class emailClass
    {
        public static void mailSend(string toWho, string body, string subject)
        {

            try
            {
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                smtpClient.EnableSsl = true;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.UseDefaultCredentials = true;
                smtpClient.Credentials = new NetworkCredential("kowalski.jan567890@gmail.com", "alamakota123");
                MailMessage msg = new MailMessage();
                //string path = HttpContext.Server.MapPath("~/Content/template/guidMailBody.html");
                //string body = System.IO.File.ReadAllText(path);
                //body = body.Replace("{guid}", nowyGuid.ToString());
                msg.Body = body;
                msg.IsBodyHtml = true;
                msg.Subject = subject;
                msg.From = new MailAddress("kowalski.jan567890@gmail.com");
#if DEBUG
                msg.To.Add("kowalski.jan567890@gmail.com");
#else 
                msg.To.Add(toWho);
#endif

                smtpClient.Send(msg);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


    }
}