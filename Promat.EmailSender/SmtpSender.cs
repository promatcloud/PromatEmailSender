using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Promat.EmailSender.Interfaces;

namespace Promat.EmailSender
{
    public class SmtpSender : IEmailSender
    {
        private readonly bool _disposeSmtpClient = true;
        private IWebProxy _webProxy;
        private ILogger<SmtpSender> _logger;
        private string _host, _user, _password, _fromEmail, _fromName;
        private int _port;

        private bool _tlsEnabled,
            _ignoreRemoteCertificateChainErrors,
            _ignoreRemoteCertificateNameMismatch,
            _ignoreRemoteCertificateNotAvailable,
            _ssl3,
            _tls,
            _tls11,
            _tls12;
        private SmtpClient _client;

        private bool IgnoreSomeServerCertificateError => _ignoreRemoteCertificateChainErrors || _ignoreRemoteCertificateNameMismatch || _ignoreRemoteCertificateNotAvailable;
        private bool EstablishedSomeProtocol() => _ssl3 || _tls || _tls11 || _tls12;

        public SmtpSender(SmtpClient smtpClient)
        {
            Initialize(smtpClient: smtpClient);
            _disposeSmtpClient = false;
        }
        public SmtpSender(ILogger<SmtpSender> logger, SmtpClient smtpClient)
        {
            Initialize(logger, smtpClient: smtpClient);
            _disposeSmtpClient = false;
        }
        public SmtpSender(string host, int port, string user, string password, bool tlsEnabled = default)
        {
            Initialize(null, host, user, password, port, tlsEnabled);
        }
        public SmtpSender(string host, int port, string user, string password, bool tlsEnabled = default, string defaultFromEmail = default, string defaultFromName = default, bool ignoreRemoteCertificateChainErrors = false, bool ignoreRemoteCertificateNameMismatch = false, bool ignoreRemoteCertificateNotAvailable = false)
        {
            Initialize(null, host, user, password, port, tlsEnabled, defaultFromEmail, defaultFromName, null, ignoreRemoteCertificateChainErrors, ignoreRemoteCertificateNameMismatch, ignoreRemoteCertificateNotAvailable);
        }
        public SmtpSender(ILogger<SmtpSender> logger, string host, int port, string user, string password, bool tlsEnabled = default, string defaultFromEmail = default, string defaultFromName = default, bool ignoreRemoteCertificateChainErrors = false, bool ignoreRemoteCertificateNameMismatch = false, bool ignoreRemoteCertificateNotAvailable = false)
        {
            Initialize(logger, host, user, password, port, tlsEnabled, defaultFromEmail, defaultFromName, null, ignoreRemoteCertificateChainErrors, ignoreRemoteCertificateNameMismatch, ignoreRemoteCertificateNotAvailable);
        }

        /// <summary>
        /// Specifies the Secure Socket Layer (SSL) 3.0 security protocol. SSL 3.0 has been superseded by the Transport Layer Security (TLS) protocol and is provided for backward compatibility only.
        /// </summary>
        public void EnableSll3SecurityProtocol() => _ssl3 = true;
        /// <summary>
        /// Specifies the Transport Layer Security (TLS) 1.0 security protocol. The TLS 1.0 protocol is defined in IETF RFC 2246.
        /// </summary>
        public void EnableTlsSecurityProtocol() => _tls = true;
        /// <summary>
        /// Specifies the Transport Layer Security (TLS) 1.1 security protocol. The TLS 1.1 protocol is defined in IETF RFC 4346. On Windows systems, this value is supported starting with Windows 7.
        /// </summary>
        public void EnableTls11SecurityProtocol() => _tls11 = true;
        /// <summary>
        /// Specifies the Transport Layer Security (TLS) 1.2 security protocol. The TLS 1.2 protocol is defined in IETF RFC 5246. On Windows systems, this value is supported starting with Windows 7.
        /// </summary>
        public void EnableTls12SecurityProtocol() => _tls12 = true;

        private void Initialize(ILogger<SmtpSender> logger = null, string host = default, string user = default, string password = default, int port = -1, bool tlsEnabled = default, string fromEmail = null, string fromName = null, SmtpClient smtpClient = null, bool ignoreRemoteCertificateChainErrors = false, bool ignoreRemoteCertificateNameMismatch = false, bool ignoreRemoteCertificateNotAvailable = false)
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
            _ignoreRemoteCertificateChainErrors = ignoreRemoteCertificateChainErrors;
            _ignoreRemoteCertificateNameMismatch = ignoreRemoteCertificateNameMismatch;
            _ignoreRemoteCertificateNotAvailable = ignoreRemoteCertificateNotAvailable;

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
        }
        /// <summary>
        /// Envía un email según los parámetros configurados
        /// </summary>
        /// <param name="toEmail">Dirección de destino</param>
        /// <param name="subject">Asunto del correo</param>
        /// <param name="htmlMessage">Cuerpo Html del correo</param>
        /// <returns></returns>
        public Task SendEmailAsync(string toEmail, string subject, string htmlMessage) =>
                SendEmailAsync(toEmail, null, subject, htmlMessage, null, null, null);
        /// <summary>
        /// Envía un email según los parámetros configurados
        /// </summary>
        /// <param name="toEmail">Dirección de destino</param>
        /// <param name="subject">Asunto del correo</param>
        /// <param name="htmlMessage">Cuerpo Html del correo</param>
        /// <param name="plainTextMessage">Texto plano del correo</param>
        /// <returns></returns>
        public Task SendEmailAsync(string toEmail, string subject, string htmlMessage, string plainTextMessage) =>
                SendEmailAsync(toEmail, null, subject, htmlMessage, plainTextMessage, null, null);
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
                SendEmailAsync(toEmail, null, subject, htmlMessage, plainTextMessage, fromEmail, fromName);
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

            var isBodyHtml = !string.IsNullOrWhiteSpace(htmlMessage);
            var mensaje = isBodyHtml ? htmlMessage : plainTextMessage;

            var mailMessage = new MailMessage
            {
                From = string.IsNullOrWhiteSpace(fromName) ? new MailAddress(fromEmail) : new MailAddress(fromEmail, fromName),
                To = { toEmail },
                Subject = subject,
                Body = mensaje,
                IsBodyHtml = isBodyHtml
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
        /// <summary>
        /// Envía un email según los parámetros configurados
        /// </summary>
        /// <param name="mailMessage">Instancia de <see cref="MailMessage"/> a enviar</param>
        /// <returns></returns>
        public async Task SendEmailAsync(MailMessage mailMessage)
        {
            IWebProxy defaultProxy = null;
            if (_webProxy != null)
            {
                defaultProxy = WebRequest.DefaultWebProxy;
                WebRequest.DefaultWebProxy = _webProxy;
            }

            _logger?.LogDebug("Se envía un correo con el asunto {asunto} a la dirección {to} desde {from}", mailMessage.Subject, mailMessage.To.FirstOrDefault()?.Address, mailMessage.From.Address);
            RemoteCertificateValidationCallback originalCallback = null;
            SecurityProtocolType originalProtocolType = 0;

            try
            {
                if (IgnoreSomeServerCertificateError)
                {
                    originalCallback = ServicePointManager.ServerCertificateValidationCallback;
                    ServicePointManager.ServerCertificateValidationCallback = ServerCertificateValidationCallback;
                }
                if (EstablishedSomeProtocol())
                {
                    originalProtocolType = ServicePointManager.SecurityProtocol;
                    SecurityProtocolType newProtocol = 0;
                    if (_ssl3)
                    {
                        newProtocol |= SecurityProtocolType.Ssl3;
                    }
                    if (_tls)
                    {
                        newProtocol |= SecurityProtocolType.Tls;
                    }
                    if (_tls11)
                    {
                        newProtocol |= SecurityProtocolType.Tls11;
                    }
                    if (_tls12)
                    {
                        newProtocol |= SecurityProtocolType.Tls12;
                    }

                    ServicePointManager.SecurityProtocol = newProtocol;
                }

                await _client.SendMailAsync(mailMessage);
            }
            catch (Exception e)
            {
                _logger?.LogError(e, "Error enviando correo con SmtpSender");
                throw;
            }
            finally
            {
                if (IgnoreSomeServerCertificateError)
                {
                    ServicePointManager.ServerCertificateValidationCallback = originalCallback;
                }
                if (EstablishedSomeProtocol())
                {
                    ServicePointManager.SecurityProtocol = originalProtocolType;
                }
                if (_webProxy != null)
                {
                    WebRequest.DefaultWebProxy = defaultProxy;
                }
            }
        }
        public void Dispose()
        {
            if (_disposeSmtpClient)
            {
                _client?.Dispose();
            }
        }

        private bool ServerCertificateValidationCallback(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslpolicyerrors)
        {
            var ignoredErrors = SslPolicyErrors.None;
            if (_ignoreRemoteCertificateChainErrors)// self-signed
            {
                ignoredErrors |= SslPolicyErrors.RemoteCertificateChainErrors;
            }
            if (_ignoreRemoteCertificateNameMismatch)// name mismatch
            {
                ignoredErrors |= SslPolicyErrors.RemoteCertificateNameMismatch;
            }
            if (_ignoreRemoteCertificateNotAvailable)
            {
                ignoredErrors |= SslPolicyErrors.RemoteCertificateNotAvailable;
            }

            var valid = (sslpolicyerrors & ~ignoredErrors) == SslPolicyErrors.None;
            _logger?.LogDebug("{method} invoked\r\nCertificate data: {subject}\r\nSSL errors: {errors}\r\nIgnored errors: {ignoredErrors}\r\nValidation result: {result}",
                              nameof(ServerCertificateValidationCallback),
                              new { certificate.Subject, certificate.Issuer },
                              sslpolicyerrors,
                              ignoredErrors,
                              valid);

            return valid;
        }
    }
}
