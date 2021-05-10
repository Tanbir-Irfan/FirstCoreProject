using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using BookProject.Models;
using Microsoft.Extensions.Options;

namespace BookProject.Service
{
    public class EmailService : IEmailService
    {
        private const string templatePath = @"EmailTemplate/{0}.html";
        private readonly SMTPConfigModel _smtpConfig;

        public EmailService(IOptions<SMTPConfigModel> smtpConfig)
        {
            this._smtpConfig = smtpConfig.Value;
        }

        public async Task SendTestEmail(UserEmailOptions userEmailOptions)
        {
            // userEmailOptions.Subject = "Test Email";
            userEmailOptions.Subject = UpdatePlaceHolders("Hello {{Username}}, This is for test email", userEmailOptions.PlaceHolders);
            // userEmailOptions.Body = GetEmailBody("TestEmail");
            userEmailOptions.Body = UpdatePlaceHolders(GetEmailBody("TestEmail"), userEmailOptions.PlaceHolders);

            await SendEmail(userEmailOptions);
        }

        private async Task SendEmail(UserEmailOptions userEmailOptions)
        {
            MailMessage mail = new MailMessage()
            {
                Subject = userEmailOptions.Subject,
                Body = userEmailOptions.Body,
                From = new MailAddress(this._smtpConfig.SenderAddress, this._smtpConfig.SenderDisplayName),
                IsBodyHtml = this._smtpConfig.IsBodyHTML
            };

            foreach (var toEmail in userEmailOptions.toEmails)
            {
                mail.To.Add(toEmail);
            }

            NetworkCredential networkCredential  = new NetworkCredential(this._smtpConfig.UserName, this._smtpConfig.Password);

            SmtpClient smtpClient = new SmtpClient()
            {
                Host = this._smtpConfig.Host,
                Port = this._smtpConfig.Port,
                EnableSsl = this._smtpConfig.EnableSSL,
                UseDefaultCredentials = this._smtpConfig.UseDefaultCredentials,
                Credentials = networkCredential
            };

            mail.BodyEncoding = Encoding.Default;

            await smtpClient.SendMailAsync(mail);
        }

        private string GetEmailBody(string templateName)
        {
            return File.ReadAllText(string.Format(templatePath, templateName));
        }

        private string UpdatePlaceHolders(string text, List<KeyValuePair<string, string>> keyValuePairs)
        {
            if(!string.IsNullOrEmpty(text) && keyValuePairs != null)
            {
                foreach (var placeHolder in keyValuePairs)
                {
                    if(text.Contains(placeHolder.Key))
                    {
                        text = text.Replace(placeHolder.Key, placeHolder.Value);
                    }
                }
            }

            return text;
        }
    }
}