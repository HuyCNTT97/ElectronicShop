using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Net;
using System.Net.Mail;

namespace Common
{
    public class MailHelper
    {
        public void SendMail(string toEmailAddress,string subject,string content)
        {
            var fromEmailAddress = ConfigurationManager.AppSettings["FromEmailAddress"].ToString();
            var fromEmailDisplayName = ConfigurationManager.AppSettings["FromEmailDisplayName"].ToString();
            var fromEmailPassword = ConfigurationManager.AppSettings["FromEmailPassword"].ToString();
            var smptHost = ConfigurationManager.AppSettings["SMTPHost"].ToString();
            var smptPort = ConfigurationManager.AppSettings["SMTPPort"].ToString();
            bool enabledSsl = bool.Parse(ConfigurationManager.AppSettings["EnabledSSL"].ToString());
            string body = content;
            MailMessage message = new MailMessage(new MailAddress(fromEmailAddress, fromEmailDisplayName), new MailAddress(toEmailAddress));
            message.Subject = subject;
            message.IsBodyHtml = true;
            message.Body = body;
            var client = new SmtpClient();
            client.Credentials = new NetworkCredential(fromEmailAddress, fromEmailPassword);
            client.Host = smptHost;
            client.EnableSsl = enabledSsl;
            client.Port = !string.IsNullOrEmpty(smptPort) ? Convert.ToInt32(smptPort) : 0;
            client.Send(message);
        }
    }
}
