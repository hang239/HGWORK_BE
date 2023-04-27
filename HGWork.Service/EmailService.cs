using HGWork.Model;
using HGWork.Service.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HGWork.Service
{
    public class EmailService : IEmailService
    {
        SmtpClient _smtpClient;

        void OpenConnect()
        {
            _smtpClient = new SmtpClient();
            _smtpClient.Connect("smtp-relay.sendinblue.com", 587, SecureSocketOptions.Auto);
            _smtpClient.Authenticate("connit63@gmail.com", "MxzvHap7S93DRYyQ");
        }

        void Disconnect()
        {
            _smtpClient.Disconnect(true);
        }

        public async Task<bool> SendMail(Email email)
        {
            OpenConnect();
            var emailMessage = new MimeMessage();
            var from = string.IsNullOrEmpty(email.From) ? "connit63@gmail.com" : email.From;

            emailMessage.From.Add(MailboxAddress.Parse(from));
            emailMessage.To.Add(MailboxAddress.Parse(email.To));
            if (email.Cc != null && email.Cc.Count > 0)
            {
                foreach (var cc in email.Cc)
                {
                    emailMessage.Cc.Add(MailboxAddress.Parse(cc));
                }
            }
            emailMessage.Subject = email.Subject;
            emailMessage.Body = new TextPart("html") { Text = email.EmailContent };

            var res = await _smtpClient.SendAsync(emailMessage);
            Disconnect();

            return true;
        }
    }
}
