using System;
using System.Linq;
using System.Threading.Tasks;
using APSystem.Configuration.Settings;
using APSystem.Models.Email;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace APSystem.Services.Email
{
    public class EmailService: IEmailService
    {
        private readonly IOptions<EmailSettings> _emailSettings;
        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings;
        }
        async Task IEmailService.SendEmailAsync(EmailRequest emailRequest)
        {
            var mailMessage = CreateEmailMessage(emailRequest);
            await SendAsync(mailMessage);
        }
        private MimeMessage CreateEmailMessage(EmailRequest emailRequest)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(MailboxAddress.Parse(_emailSettings.Value.Sender));
            emailMessage.To.AddRange(emailRequest.To);
            emailMessage.Subject = emailRequest.Subject;
            var bodyBuilder = new BodyBuilder { HtmlBody = emailRequest.Content };
            if (emailRequest.Attachments != null && emailRequest.Attachments.Any())
            {
                foreach (var item in emailRequest.Attachments)
                {
                    bodyBuilder.Attachments.Add(item);
                }
            }
            emailMessage.Body = bodyBuilder.ToMessageBody();
            return emailMessage;
        }
        private async Task SendAsync(MimeMessage mailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    await client.ConnectAsync(_emailSettings.Value.MailServer, _emailSettings.Value.MailPort, SecureSocketOptions.Auto);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    await client.AuthenticateAsync(_emailSettings.Value.Sender, _emailSettings.Value.Password);
                    await client.SendAsync(mailMessage);
                }
                catch (Exception ex)
                {
                    //log an error message or throw an exception, or both.
                    throw new InvalidOperationException(ex.Message);
                }
                finally
                {
                    await client.DisconnectAsync(true);
                    client.Dispose();
                }
            }
        }
    }
}