using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Promat.EmailSender.Common.Interfaces;
using Promat.EmailSender.Smtp.Options;
using System;
using System.Net;

namespace Promat.EmailSender.Smtp.Extensions;

public static class ExtensionMethods
{
    /// <summary>
    /// Añade los servicios necesarios para poder enviar correos mediante SMTP resolviendo el servicio <see cref="IEmailSender"/>
    /// <para>Configura las opciones <see cref="SmtpOptions"/>  que podrán ser resueltas por el inyector de dependencias mediante <see cref="IOptions{TOptions}"/> donde TOptions sea <see cref="SmtpOptions"/></para>
    /// </summary>
    /// <param name="services">Colección de servicios</param>
    /// <param name="configuration">Configuración de la aplicación</param>
    /// <returns></returns>
    public static IServiceCollection AddPromatEmailSenderSmtp(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<SmtpOptions>(options =>
        {
            MapSmtpOptions(options, configuration);
            options.SecurityProtocol = new SecurityProtocolOptions();
            MapSecurityProtocolOptions(options.SecurityProtocol, configuration);
        });
        services.Configure<SmtpOptions>(options => MapSmtpOptions(options, configuration));
        services.Configure<SecurityProtocolOptions>(options => MapSecurityProtocolOptions(options, configuration));

        services.AddTransient<IEmailSender, EmailSender>(provider =>
        {
            var options = provider.GetRequiredService<IOptions<SmtpOptions>>().Value;
            var smtpSender = new EmailSender(provider.GetService<ILogger<EmailSender>>(), 
                options.Host, options.Port, 
                options.User, options.Password, 
                options.TlsEnabled, 
                options.DefaultFromEmail, options.DefaultFromName, 
                options.IgnoreRemoteCertificateChainErrors, 
                options.IgnoreRemoteCertificateNameMismatch, 
                options.IgnoreRemoteCertificateNotAvailable);
            if (options.SecurityProtocol.Ssl3)
            {
                smtpSender.EnableSll3SecurityProtocol();
            }
            if (options.SecurityProtocol.Tls)
            {
                smtpSender.EnableTlsSecurityProtocol();
            }
            if (options.SecurityProtocol.Tls11)
            {
                smtpSender.EnableTls11SecurityProtocol();
            }
            if (options.SecurityProtocol.Tls12)
            {
                smtpSender.EnableTls12SecurityProtocol();
            }
            if (!string.IsNullOrWhiteSpace(options.Proxy))
            {
                smtpSender.SetWebProxy(new WebProxy
                {
                    Address = new Uri(options.Proxy),
                    UseDefaultCredentials = true
                });
            }
            return smtpSender;
        });
        return services;
    }
    
    private static void MapSmtpOptions(SmtpOptions options, IConfiguration configuration)
    {
        options.Host = configuration[SmtpOptions.HostKey];
        options.User = configuration[SmtpOptions.UserKey];
        options.Password = configuration[SmtpOptions.PasswordKey];
        options.DefaultFromEmail = configuration[SmtpOptions.DefaultFromEmailKey];
        options.DefaultFromName = configuration[SmtpOptions.DefaultFromNameKey];
        options.Proxy = configuration[SmtpOptions.ProxyKey];

        if (int.TryParse(configuration[SmtpOptions.PortKey], out var port))
        {
            options.Port = port;
        }
        if (bool.TryParse(configuration[SmtpOptions.TlsEnabledKey], out var tlsEnabled))
        {
            options.TlsEnabled = tlsEnabled;
        }
        if (bool.TryParse(configuration[SmtpOptions.IgnoreRemoteCertificateChainErrorsKey], out var ignoreRemoteCertificateChainErrors))
        {
            options.IgnoreRemoteCertificateChainErrors = ignoreRemoteCertificateChainErrors;
        }
        if (bool.TryParse(configuration[SmtpOptions.IgnoreRemoteCertificateNameMismatchKey], out var ignoreRemoteCertificateNameMismatch))
        {
            options.IgnoreRemoteCertificateNameMismatch = ignoreRemoteCertificateNameMismatch;
        }
        if (bool.TryParse(configuration[SmtpOptions.IgnoreRemoteCertificateNotAvailableKey], out var ignoreRemoteCertificateNotAvailable))
        {
            options.IgnoreRemoteCertificateNotAvailable = ignoreRemoteCertificateNotAvailable;
        }
    }
    private static void MapSecurityProtocolOptions(SecurityProtocolOptions options, IConfiguration configuration)
    {
        if (bool.TryParse(configuration[SecurityProtocolOptions.Ssl3Key], out var ssl3))
        {
            options.Ssl3 = ssl3;
        }
        if (bool.TryParse(configuration[SecurityProtocolOptions.TlsKey], out var tls))
        {
            options.Tls = tls;
        }
        if (bool.TryParse(configuration[SecurityProtocolOptions.Tls11Key], out var tls11))
        {
            options.Tls11 = tls11;
        }
        if (bool.TryParse(configuration[SecurityProtocolOptions.Tls12Key], out var tls12))
        {
            options.Tls12 = tls12;
        }
    }
}