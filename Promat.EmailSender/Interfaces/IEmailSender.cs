using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Promat.EmailSender.Interfaces
{
    public interface IEmailSender
    {
        /// <summary>
        /// Envía un email según los parámetros configurados
        /// </summary>
        /// <param name="toEmail">Dirección de destino</param>
        /// <param name="subject">Asunto del correo</param>
        /// <param name="htmlMessage">Cuerpo Html del correo</param>
        /// <returns></returns>
        Task SendEmailAsync(string toEmail, string subject, string htmlMessage);
        /// <summary>
        /// Envía un email según los parámetros configurados
        /// </summary>
        /// <param name="toEmail">Dirección de destino</param>
        /// <param name="subject">Asunto del correo</param>
        /// <param name="htmlMessage">Cuerpo Html del correo</param>
        /// <param name="plainTextMessage">Texto plano del correo</param>
        /// <returns></returns>
        Task SendEmailAsync(string toEmail, string subject, string htmlMessage, string plainTextMessage);
        /// <summary>
        /// Envía un email según los parámetros configurados
        /// </summary>
        /// <param name="toEmail">Dirección de destino</param>
        /// <param name="cc">(opcional) Direcciones a las que mandar copia</param>
        /// <param name="subject">Asunto del correo</param>
        /// <param name="htmlMessage">Cuerpo Html del correo</param>
        /// <param name="plainTextMessage">Texto plano del correo</param>
        /// <param name="fromEmail">(opcional) Dirección desde la que se manda el correo</param>
        /// <param name="fromName">(opcional) Nombre a mostrar para la dirección <see cref="fromEmail"/></param>
        /// <param name="attachments">(opcional) Adjuntos a mandar con el correo.</param>
        /// <returns></returns>
        Task SendEmailAsync(string toEmail, IEnumerable<string> cc, string subject, string htmlMessage, string plainTextMessage, string fromEmail, string fromName, params Attachment[] attachments);
        /// <summary>
        /// Envía un email según los parámetros configurados
        /// </summary>
        /// <param name="toEmail">Dirección de destino</param>
        /// <param name="subject">Asunto del correo</param>
        /// <param name="htmlMessage">Cuerpo Html del correo</param>
        /// <param name="plainTextMessage">Texto plano del correo</param>
        /// <param name="fromEmail">(opcional) Dirección desde la que se manda el correo</param>
        /// <param name="fromName">(opcional) Nombre a mostrar para la dirección <see cref="fromEmail"/></param>
        /// <returns></returns>
        Task SendEmailAsync(string toEmail, string subject, string plainTextMessage, string htmlMessage, string fromEmail, string fromName);
        /// <summary>
        /// Envía un email según los parámetros configurados
        /// </summary>
        /// <param name="mailMessage">Instancia de <see cref="MailMessage"/> a enviar</param>
        /// <returns></returns>
        Task SendEmailAsync(MailMessage mailMessage);

        /// <summary>
        /// Establece un <see cref="IWebProxy"/> a través del cual se enviará
        /// </summary>
        /// <param name="webProxy"></param>
        void SetWebProxy(IWebProxy webProxy);
    }
}
