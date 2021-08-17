using System.Collections.Generic;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Promat.EmailSender.Interfaces
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string toEmail, string subject, string htmlMessage);
        Task SendEmailAsync(string toEmail, string subject, string htmlMessage, string plainTextMessage);
        Task SendEmailAsync(string toEmail, IEnumerable<string> cc, string subject, string htmlMessage, string plainTextMessage, string fromEmail, string fromName, params Attachment[] attachments);
        Task SendEmailAsync(string toEmail, string subject, string plainTextMessage, string htmlMessage, string fromEmail, string fromName);
        Task SendEmailAsync(MailMessage mailMessage);
    }
}
