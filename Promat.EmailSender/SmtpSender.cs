using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Promat.EmailSender.Interfaces;

namespace Promat.EmailSender
{
    public class SmtpSender : IEmailSender
    {
        private IWebProxy _webProxy;
        private ILogger<SmtpSender> _logger;
        private string _host, _user, _password, _fromEmail, _fromName;
        private int _port;
        private bool _tlsEnabled;
        private SmtpClient _client;

        public SmtpSender(SmtpClient smtpClient)
        {
            Initialize(smtpClient: smtpClient);
        }
        public SmtpSender(ILogger<SmtpSender> logger, SmtpClient smtpClient)
        {
            Initialize(logger, smtpClient: smtpClient);
        }
        public SmtpSender(string host, int port, string user, string password, bool tlsEnabled = default)
        {
            Initialize(null, host, user, password, port, tlsEnabled);
        }
        public SmtpSender(ILogger<SmtpSender> logger, string host, int port, string user, string password, bool tlsEnabled = default, string defaultFromEmail = default, string defauiFromName = default)
        {
            Initialize(logger, host, user, password, port, tlsEnabled, defaultFromEmail, defauiFromName);
        }

        private void Initialize(ILogger<SmtpSender> logger = null, string host = default, string user = default, string password = default, int port = -1, bool tlsEnabled = default, string fromEmail = null, string fromName = null, SmtpClient smtpClient = null)
        {
            _logger = logger;
            _logger?.LogTrace("Se ha creado una nueva instancia de {sender}", nameof(SmtpSender));
            _host = host;
            _user = user;
            _password = password;
            _port = port;
            _tlsEnabled = tlsEnabled;
            _client = smtpClient;
            _fromEmail = fromEmail;
            _fromName = fromName;

            if (string.IsNullOrWhiteSpace(_host) ||
                string.IsNullOrWhiteSpace(_user) ||
                string.IsNullOrWhiteSpace(_password) ||
                _port < 0)
            {
                if (_client != null)
                {
                    _logger?.LogTrace("El host, puerto, user, password y tlsEnabled se obtiene del SmtpClient facilitado");
                    _host = _client.Host;
                    _port = _client.Port;
                    _tlsEnabled = _client.EnableSsl;
                    _user = _client.Credentials.GetCredential(_host, _port, "").UserName;
                    _password = _client.Credentials.GetCredential(_host, _port, "").Password;
                }
                else
                {
                    _logger?.LogError("Imposible inicializar {SmtpSender}. Es necesario proveer un {SmtpClient}, ó un Host, Puerto, User y Password", nameof(SmtpSender), nameof(SmtpClient));
                    throw new ArgumentException($"Imposible inicializar {nameof(SmtpSender)}. Es necesario proveer un {nameof(SmtpClient)}, ó un Host, Puerto, User y Password");
                }
            }
            else
            {
                _client = new SmtpClient(_host, _port)
                {
                    EnableSsl = _tlsEnabled,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(_user, _password)
                };
            }
            _logger?.LogDebug("Se construye {clase} con los siguientes parámetros => Host: {host}, Port: {port}, TLS Enabled: {tls}, User: {user}, Pass: {pass}, FromEmail: {fromEmail}, FromName: {fromName}",
                             nameof(SmtpSender),
                             _host,
                             _port,
                             _tlsEnabled,
                             _user,
                             _password,
                             _fromEmail,
                             _fromName);
        }

        public void SetWebProxy(IWebProxy webProxy)
        {
            _webProxy = webProxy;
            if (_webProxy != null)
            {
                if (_webProxy is WebProxy proxy)
                {
                    _logger?.LogDebug("Se establece un proxy => Url: {url}, DefaultCredentials: {defaultCredentials}",
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
        }
        public Task SendEmailAsync(string toEmail, string subject, string htmlMessage) =>
                SendEmailAsync(toEmail, null, subject, htmlMessage, null, null, null);
        public Task SendEmailAsync(string toEmail, string subject, string htmlMessage, string plainTextMessage) =>
                SendEmailAsync(toEmail, null, subject, htmlMessage, plainTextMessage, null, null);
        public Task SendEmailAsync(string toEmail, string subject, string plainTextMessage, string htmlMessage, string fromEmail, string fromName) =>
                SendEmailAsync(toEmail, null, subject, htmlMessage, plainTextMessage, fromEmail, fromName);
        public Task SendEmailAsync(string toEmail, IEnumerable<string> cc, string subject, string htmlMessage, string plainTextMessage, string fromEmail, string fromName, params Attachment[] attachments)
        {
            if (fromEmail == null)
            {
                fromEmail = _fromEmail;
            }
            if (fromEmail == null)
            {
                fromEmail = _user;
            }
            if (fromName == null)
            {
                fromName = _fromName;
            }

            var esHtlm = !string.IsNullOrWhiteSpace(htmlMessage);
            var mensaje = esHtlm ? htmlMessage : plainTextMessage;

            var mailMessage = new MailMessage
            {
                From = string.IsNullOrWhiteSpace(fromName) ? new MailAddress(fromEmail) : new MailAddress(fromEmail, fromName),
                To = { toEmail },
                Subject = subject,
                Body = mensaje,
                IsBodyHtml = esHtlm
            };

            if (cc != null)
            {
                foreach (var address in cc)
                {
                    mailMessage.CC.Add(address);
                }
            }
            if (attachments != null)
            {
                foreach (var attachment in attachments)
                {
                    mailMessage.Attachments.Add(attachment);
                }
            }

            return SendEmailAsync(mailMessage);
        }
        public async Task SendEmailAsync(MailMessage mailMessage)
        {
            IWebProxy defaultProxy = null;
            if (_webProxy != null)
            {
                defaultProxy = WebRequest.DefaultWebProxy;
                WebRequest.DefaultWebProxy = _webProxy;
            }

            _logger?.LogDebug("Se envía un correo con el asunto {asunto} a la dirección {to} desde {from}", mailMessage.Subject, mailMessage.To.FirstOrDefault()?.Address, mailMessage.From.Address);
            await _client.SendMailAsync(mailMessage);

            if (_webProxy != null)
            {
                WebRequest.DefaultWebProxy = defaultProxy;
            }
        }
    }
}
