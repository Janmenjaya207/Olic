using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace BEL
{
    public static class MailHelper
    {
        public static bool SendMail(string to, string body, string subject)
        {
            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            mail.To.Add(to);
            mail.From = new MailAddress("hroasystspl@gmail.com");
            mail.Subject = subject;// "Registration successful with OSBC";
            string Body = body;
            mail.Body = Body;
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = new System.Net.NetworkCredential("hroasystspl@gmail.com", "ijqojuipqpxmlqha"); // Enter seders User name and password       
            smtp.EnableSsl = true;
            smtp.Send(mail);

            return true;
        }
    }
}