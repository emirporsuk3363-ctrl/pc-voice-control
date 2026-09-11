using System;
using System.IO;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;

namespace VoiceControlMVP
{
    public class MailService
    {
        private readonly Action<string> _log;
        public MailService(Action<string> log) { _log = log; }

        public async Task SendEmailWithAttachmentAsync(string recipient, string subject, string bodyText, string filePath)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(MailboxAddress.Parse(Config.SmtpUsername));
                message.To.Add(MailboxAddress.Parse(recipient));
                message.Subject = subject;

                var builder = new BodyBuilder { TextBody = bodyText };
                if (File.Exists(filePath))
                    builder.Attachments.Add(filePath);
                message.Body = builder.ToMessageBody();

                using var client = new SmtpClient();
                await client.ConnectAsync(Config.SmtpServer, Config.SmtpPort, Config.SmtpUseSsl);
                // Eğer Gmail kullanacaksanız App Password veya OAuth token girin.
                await client.AuthenticateAsync(Config.SmtpUsername, Config.SmtpPassword);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
                _log("E-posta gönderildi: " + recipient);
            }
            catch (Exception ex)
            {
                _log("Mail gönderme hatası: " + ex.Message);
            }
        }
    }
}
