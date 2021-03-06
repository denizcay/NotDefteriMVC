using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace NotDefteriMVC.Services
{
    public class EmailSender : IEmailSender
    {
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            MailAddress gonderen = new MailAddress("mail@rasceta.net", "Deniz Çay");
            MailAddress to = new MailAddress(email);

            MailMessage posta = new MailMessage(gonderen, to);
            posta.Subject = subject;
            posta.Body = htmlMessage;
            posta.IsBodyHtml = true;

            using (SmtpClient smtp = new SmtpClient("rasceta.net", 587))
            {
                smtp.Credentials = new NetworkCredential("mail@rasceta.net", "password");
                smtp.EnableSsl = true;
                await smtp.SendMailAsync(posta);
            }
        }

    }
}
