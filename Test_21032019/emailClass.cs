using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Configuration;
using System.Net.Mail;
using System.Web;
using System.Web.Configuration;

namespace Test_21032019
{
    public class emailClass
    {
        public static void mailSend(string toWho, string body, string subject)
        {

            try
            {
                System.Configuration.Configuration config = WebConfigurationManager.OpenWebConfiguration(
                HttpContext.Current.Request.ApplicationPath);
                MailSettingsSectionGroup settings =
                    (MailSettingsSectionGroup)config.GetSectionGroup("system.net/mailSettings");

                SmtpClient smtpClient = new SmtpClient();
                smtpClient.Credentials = new NetworkCredential(settings.Smtp.Network.UserName, settings.Smtp.Network.Password);
                MailMessage msg = new MailMessage();
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