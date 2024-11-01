﻿using System.Net.Mail;
using System.Net;

namespace Iot_WebApp.Common.Utilities
{
    public static class SendMail
    {
        public static async Task SendAsync(string to, string subject, string body)
        {
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress("prozheali@gmail.com", "سامانه مانیتورینگ دما و رطوبت");
                mail.To.Add(to);
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;

                using (SmtpClient smtpServer = new SmtpClient("smtp.gmail.com"))
                {
                    smtpServer.UseDefaultCredentials = false;
                    smtpServer.Port = 587;
                    smtpServer.Credentials = new NetworkCredential("Replace_Your_Gmail", "Replace_Your_Pass");
                    smtpServer.EnableSsl = true;

                    try
                    {
                        await smtpServer.SendMailAsync(mail);
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }
            }
        }
    }
}
