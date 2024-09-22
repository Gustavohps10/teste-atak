using System.Net.Mail;
using System.Threading.Tasks;
using teste_atak.Domain.Contracts;
using teste_atak.Infra.Data.Config;

namespace teste_atak.Infra.Data.Repositories
{
    public class MailerRepository : IMailerRepository
    {
        private readonly SmtpConfig _smtpConfig;

        public MailerRepository(SmtpConfig smtpConfig)
        {
            _smtpConfig = smtpConfig;
        }

        public async Task Send(string to, string subject, string body, string? attachmentPath)
        {
            using var smtpClient = new SmtpClient(_smtpConfig.Host, _smtpConfig.Port)
            {
                Credentials = new System.Net.NetworkCredential(_smtpConfig.Username, _smtpConfig.Password),
                EnableSsl = true
            };

            var mailMessage = new MailMessage(_smtpConfig.From, to, subject, body);

            if (!string.IsNullOrEmpty(attachmentPath) && File.Exists(attachmentPath))
            {
                var attachment = new Attachment(attachmentPath);
                mailMessage.Attachments.Add(attachment);
            }

            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}
