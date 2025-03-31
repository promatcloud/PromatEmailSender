using System.Net;
using System.Net.Mail;
using System.Net.Security;

namespace Promat.EmailSender.Smtp.Options;

public class SmtpOptions
{
    internal const string Section = "Promat:EmailSender:Smtp";
    internal const string HostKey = Section + ":" + nameof(Host);
    internal const string PortKey = Section + ":" + nameof(Port);
    internal const string TlsEnabledKey = Section + ":" + nameof(TlsEnabled);
    internal const string UserKey = Section + ":" + nameof(User);
    internal const string PasswordKey = Section + ":" + nameof(Password);
    internal const string DefaultFromEmailKey = Section + ":" + nameof(DefaultFromEmailKey);
    internal const string DefaultFromNameKey = Section + ":" + nameof(DefaultFromNameKey);
    internal const string ProxyKey = Section + ":" + nameof(ProxyKey);
    internal const string IgnoreRemoteCertificateChainErrorsKey = Section + ":" + nameof(IgnoreRemoteCertificateChainErrors);
    internal const string IgnoreRemoteCertificateNameMismatchKey = Section + ":" + nameof(IgnoreRemoteCertificateNameMismatch);
    internal const string IgnoreRemoteCertificateNotAvailableKey = Section + ":" + nameof(IgnoreRemoteCertificateNotAvailable);

    /// <summary>
    /// Servidor de salida SMTP que se va a usar
    /// <para>Ejemplo: smtp.server.com</para>
    /// <para><b>Obligatorio</b> si se quiere usar <see cref="EmailSender"/></para>
    /// </summary>
    public string Host { get; set; }
    /// <summary>
    /// Puerto para el envío SMTP
    /// <para>Los puertos por defecto suelen ser el <b>587</b> o <b>25</b></para>
    /// <para><b>Obligatorio</b> si se quiere usar <see cref="EmailSender"/></para>
    /// </summary>
    public int Port { get; set; } = 587;
    /// <summary>
    /// Indica si el <see cref="SmtpClient"/> usado por <see cref="EmailSender"/> utilizará SSL para encriptar la conexión
    /// </summary>
    public bool TlsEnabled { get; set; } = true;
    /// <summary>
    /// Usuario para autenticarse en el servidor SMTP
    /// <para>Ejemplo: user@server.com</para>
    /// <para><b>Obligatorio</b> si se quiere usar <see cref="EmailSender"/></para>
    /// </summary>
    public string User { get; set; }
    /// <summary>
    /// Contraseña para autenticarse en el servidor SMTP
    /// <para><b>Obligatorio</b> si se quiere usar <see cref="EmailSender"/></para>
    /// </summary>
    public string Password { get; set; }
    /// <summary>
    /// Indica si se deben ignorar los errores de certificado del tipo <see cref="SslPolicyErrors.RemoteCertificateChainErrors"/> (causado comúnmente por certificados autofirmados)
    /// </summary>
    public bool IgnoreRemoteCertificateChainErrors { get; set; }
    /// <summary>
    /// Indica si se deben ignorar los errores de certificado del tipo <see cref="SslPolicyErrors.RemoteCertificateNameMismatch"/>
    /// </summary>
    public bool IgnoreRemoteCertificateNameMismatch { get; set; }
    /// <summary>
    /// Indica si se deben ignorar los errores de certificado del tipo <see cref="SslPolicyErrors.RemoteCertificateNotAvailable"/>
    /// </summary>
    public bool IgnoreRemoteCertificateNotAvailable { get; set; }
    /// <summary>
    /// (<b>opcional</b>) Email desde el que se enviarán los correos por defecto
    /// </summary>
    public string DefaultFromEmail { get; set; }
    /// <summary>
    /// (<b>opcional</b>) Nombre por defecto para el <see cref="DefaultFromEmail"/>
    /// </summary>
    public string DefaultFromName { get; set; }
    /// <summary>
    /// (<b>opcional</b>) Url del proxy
    /// </summary>
    public string Proxy { get; set; }
    /// <summary>
    /// Configuración de los protocolos de seguridad utilizados por los objetos <see cref="ServicePoint"/> gestionados por el objeto  <see cref="ServicePointManager"/>
    /// </summary>
    /// <remarks>
    /// Se pueden establecer múltiples protocolos
    /// </remarks>
    public SecurityProtocolOptions SecurityProtocol { get; set; }
}