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
        private IWebProxy _webProxy;
        private ILogger<SendGridSender> _logger;
        private HttpClient _httpClient;
        private string _apiKey;

        public SendGridSender(string apiKey)
        {
            Initialize(apiKey);
        }
        public SendGridSender(string apiKey, string fromEmail, string fromName)
        {
            _fromEmail = fromEmail;
            _fromName = fromName;
            Initialize(apiKey);
        }
        public SendGridSender(string apiKey, ILogger<SendGridSender> logger)
        {
            Initialize(apiKey, logger);
        }
        public SendGridSender(string apiKey, ILogger<SendGridSender> logger, string fromEmail, string fromName)
        {
            _fromEmail = fromEmail;
            _fromName = fromName;
            Initialize(apiKey, logger);
        }
        public SendGridSender(IWebProxy webProxy, ILogger<SendGridSender> logger, HttpClient httpClient, string apiKey)
        {
            _webProxy = webProxy;
            _logger = logger;
            _httpClient = httpClient;
            _apiKey = apiKey;
        }

        public Task SendEmailAsync(string toEmail, string subject, string htmlMessage) =>
                ProtectedSendEmailAsync(toEmail, subject, htmlMessage);
        public Task SendEmailAsync(string toEmail, string subject, string htmlMessage, string plainTextMessage) =>
                ProtectedSendEmailAsync(toEmail, subject, htmlMessage, plainTextMessage);
        public Task SendEmailAsync(string toEmail, IEnumerable<string> cc, string subject, string htmlMessage, string plainTextMessage, string fromEmail, string fromName, params Attachment[] attachments) =>
                ProtectedSendEmailAsync(toEmail, subject, htmlMessage, plainTextMessage, fromEmail, fromName, cc, attachments);
        public Task SendEmailAsync(string toEmail, string subject, string plainTextMessage, string htmlMessage, string fromEmail, string fromName) =>
                ProtectedSendEmailAsync(toEmail, subject, htmlMessage, plainTextMessage, fromEmail, fromName);
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
        public void SetWebProxy(IWebProxy webProxy)
        {
            _webProxy = webProxy;
            _httpClient = CreateHttpClient();
        }

        protected async Task ProtectedSendEmailAsync(string toEmail, string subject, string htmlMessage, string plainTextMessage = null, string fromEmail = null, string fromName = null, IEnumerable<string> cc = null, Attachment[] attachments = null, CancellationToken cancellationToken = default)
        {
            //var apiKey = GetSendGridApiKey();
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

            _logger.LogDebug("");
            var response = await client.SendEmailAsync(msg, cancellationToken);

            if (response.StatusCode != HttpStatusCode.Accepted)
            {
                _logger.LogWarning("Respuesta de SendEMail: {StatusCode}", response.StatusCode);
                var json = await response.Body.ReadAsStringAsync();
                var myJObject = JObject.Parse(json);
                var message = "Response:";
                foreach (var pair in myJObject)
                {
                    message += $"{Environment.NewLine} - {pair.Key}: {pair.Value}";
                }
                _logger.LogWarning(message);
            }
            else
            {
                _logger.LogDebug("Respuesta de SendEMail: {StatusCode}", response.StatusCode);
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