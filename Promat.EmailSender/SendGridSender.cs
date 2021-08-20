using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Promat.EmailSender.Interfaces;
using SendGrid;
using SendGrid.Helpers.Mail;
using Attachment = System.Net.Mail.Attachment;

namespace Promat.EmailSender
{
    public class SendGridSender : IEmailSender
    {
        private readonly string _fromEmail;
        private readonly string _fromName;
        private readonly bool _disposeHttpClient = true;
        private IWebProxy _webProxy;
        private ILogger<SendGridSender> _logger;
        private HttpClient _httpClient;
        private string _apiKey;

        /// <summary>
        /// Nueva instancia de <see cref="SendGridSender"/>
        /// </summary>
        /// <param name="apiKey">API Key de SendGrid</param>
        public SendGridSender(string apiKey)
        {
            Initialize(apiKey);
        }
        /// <summary>
        /// Nueva instancia de <see cref="SendGridSender"/>
        /// </summary>
        /// <param name="apiKey">API Key de SendGrid</param>
        /// <param name="fromEmail">(opcional) Dirección desde la que se manda el correo</param>
        /// <param name="fromName">(opcional) Nombre a mostrar para la dirección <see cref="fromEmail"/></param>
        public SendGridSender(string apiKey, string fromEmail, string fromName)
        {
            _fromEmail = fromEmail;
            _fromName = fromName;
            Initialize(apiKey);
        }
        /// <summary>
        /// Nueva instancia de <see cref="SendGridSender"/>
        /// </summary>
        /// <param name="apiKey">API Key de SendGrid</param>
        /// <param name="logger">(opcional) Logger</param>
        public SendGridSender(string apiKey, ILogger<SendGridSender> logger)
        {
            Initialize(apiKey, logger);
        }
        /// <summary>
        /// Nueva instancia de <see cref="SendGridSender"/>
        /// </summary>
        /// <param name="apiKey">API Key de SendGrid</param>
        /// <param name="logger">(opcional) Logger</param>
        /// <param name="fromEmail">(opcional) Dirección desde la que se manda el correo</param>
        /// <param name="fromName">(opcional) Nombre a mostrar para la dirección <see cref="fromEmail"/></param>
        /// <param name="webProxy">(opcional) Proxy a través del cual realizar los envíos</param>
        public SendGridSender(string apiKey, ILogger<SendGridSender> logger, string fromEmail, string fromName, IWebProxy webProxy = null)
        {
            _fromEmail = fromEmail;
            _fromName = fromName;
            _webProxy = webProxy;
            Initialize(apiKey, logger);
        }
        /// <summary>
        /// Nueva instancia de <see cref="SendGridSender"/>
        /// </summary>
        /// <param name="apiKey">API Key de SendGrid</param>
        /// <param name="logger">(opcional) Logger</param>
        /// <param name="webProxy">(opcional) Proxy a través del cual realizar los envíos</param>
        /// <param name="httpClient">Cliente Http que se desea usar para los envíos a SendGrid</param>
        public SendGridSender(string apiKey, ILogger<SendGridSender> logger, IWebProxy webProxy, HttpClient httpClient)
        {
            _webProxy = webProxy;
            _logger = logger;
            _httpClient = httpClient;
            _apiKey = apiKey;
            _disposeHttpClient = false;
        }

        /// <summary>
        /// Envía un email según los parámetros configurados
        /// </summary>
        /// <param name="toEmail">Dirección de destino</param>
        /// <param name="subject">Asunto del correo</param>
        /// <param name="htmlMessage">Cuerpo Html del correo</param>
        /// <returns></returns>
        public Task SendEmailAsync(string toEmail, string subject, string htmlMessage) =>
                ProtectedSendEmailAsync(toEmail, subject, htmlMessage);
        /// <summary>
        /// Envía un email según los parámetros configurados
        /// </summary>
        /// <param name="toEmail">Dirección de destino</param>
        /// <param name="subject">Asunto del correo</param>
        /// <param name="htmlMessage">Cuerpo Html del correo</param>
        /// <param name="plainTextMessage">Texto plano del correo</param>
        /// <returns></returns>
        public Task SendEmailAsync(string toEmail, string subject, string htmlMessage, string plainTextMessage) =>
                ProtectedSendEmailAsync(toEmail, subject, htmlMessage, plainTextMessage);
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
        public Task SendEmailAsync(string toEmail, IEnumerable<string> cc, string subject, string htmlMessage, string plainTextMessage, string fromEmail, string fromName, params Attachment[] attachments) =>
                ProtectedSendEmailAsync(toEmail, subject, htmlMessage, plainTextMessage, fromEmail, fromName, cc, attachments);
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
        public Task SendEmailAsync(string toEmail, string subject, string plainTextMessage, string htmlMessage, string fromEmail, string fromName) =>
                ProtectedSendEmailAsync(toEmail, subject, htmlMessage, plainTextMessage, fromEmail, fromName);
        /// <summary>
        /// Envía un email según los parámetros configurados
        /// </summary>
        /// <param name="mailMessage">Instancia de <see cref="MailMessage"/> a enviar</param>
        /// <returns></returns>
        public Task SendEmailAsync(MailMessage mailMessage)
        {
            var htmlMessage = mailMessage.IsBodyHtml ? mailMessage.Body : string.Empty;
            var plainMessage = mailMessage.IsBodyHtml ? string.Empty : mailMessage.Body;
            return ProtectedSendEmailAsync(mailMessage.To.First().Address,
                                           mailMessage.Subject,
                                           htmlMessage,
                                           plainMessage,
                                           mailMessage.From.Address,
                                           mailMessage.From.DisplayName,
                                           mailMessage.CC.Select(x => x.Address),
                                           mailMessage.Attachments.ToArray());
        }
        /// <summary>
        /// Establece un <see cref="IWebProxy"/> a través del cual se enviará
        /// </summary>
        /// <param name="webProxy"></param>
        public void SetWebProxy(IWebProxy webProxy)
        {
            _webProxy = webProxy;
            if (_webProxy != null)
            {
                if (_webProxy is WebProxy proxy)
                {
                    _logger?.LogDebug("Se establece un proxy => Url: {url}, UseDefaultCredentials: {defaultCredentials}",
                                      proxy.Address,
                                      proxy.UseDefaultCredentials);
                }
                else
                {
                    _logger?.LogDebug("Se establece un proxy");
                }
            }
            else
            {
                _logger?.LogDebug("Se establece el proxy a null");
            }
            _httpClient = CreateHttpClient();
        }
        public void Dispose()
        {
            if(_disposeHttpClient)
            {
                _httpClient?.Dispose();
            }
        }

        private async Task ProtectedSendEmailAsync(string toEmail, string subject, string htmlMessage, string plainTextMessage = null, string fromEmail = null, string fromName = null, IEnumerable<string> cc = null, Attachment[] attachments = null, CancellationToken cancellationToken = default)
        {
            var client = new SendGridClient(_httpClient, _apiKey);
            var from = new EmailAddress(fromEmail ?? _fromEmail, fromName ?? _fromName);
            var tos = new List<EmailAddress> { new EmailAddress(toEmail) };
            var ccArray = (cc as string[] ?? cc?.ToArray()) ?? new string[0];
            if (ccArray.Any())
            {
                tos.AddRange(ccArray.Select(address => new EmailAddress(address)));
            }
            var plainTextContent = plainTextMessage ?? Regex.Replace(htmlMessage, "<[^>]*>", "");
            var msg = MailHelper.CreateSingleEmailToMultipleRecipients(from, tos, subject, plainTextContent, htmlMessage);
            if (attachments != null)
            {
                foreach (var attachment in attachments)
                {
                    await msg.AddAttachmentAsync(attachment.Name, attachment.ContentStream, attachment.ContentType.MediaType, attachment.ContentDisposition.DispositionType, attachment.ContentId, cancellationToken);
                }
            }

            _logger?.LogDebug("Se envía un correo con el asunto {asunto} a la dirección {to} desde {from}", msg.Subject, msg.Personalizations.SelectMany(p => p.Tos).Select(t => t.Email), msg.From.Email);
            var response = await client.SendEmailAsync(msg, cancellationToken);

            if (response.StatusCode != HttpStatusCode.Accepted)
            {
                _logger?.LogWarning("Respuesta de SendGrid: {StatusCode}", response.StatusCode);
                var json = await response.Body.ReadAsStringAsync();
                var myJObject = JObject.Parse(json);
                var message = "Response:";
                foreach (var pair in myJObject)
                {
                    message += $"{Environment.NewLine} - {pair.Key}: {pair.Value}";
                }
                _logger?.LogWarning(message);
            }
            else
            {
                _logger?.LogDebug("Respuesta de SendGrid: {StatusCode}", response.StatusCode);
            }
        }
        private HttpClient CreateHttpClient()
        {
            try
            {
                if (_webProxy == null)
                {
                    return new HttpClient();
                }

                var httpClientHandler = new HttpClientHandler
                {
                    Proxy = _webProxy
                };

                return new HttpClient(handler: httpClientHandler, disposeHandler: true);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error creando el cliente Http");
            }

            return null;
        }
        private void Initialize(string apiKey, ILogger<SendGridSender> logger = null)
        {
            _apiKey = apiKey;
            _logger = logger;
            _httpClient = CreateHttpClient();
        }
    }
}