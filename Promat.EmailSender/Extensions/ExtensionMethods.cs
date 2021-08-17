using System;
using System.Net;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Promat.EmailSender.Interfaces;
using Promat.EmailSender.Options;

namespace Promat.EmailSender.Extensions
{
    public static class ExtensionMethods
    {
        public static IServiceCollection AddPromatEmailSenderSmtp(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<PromatEmailSenderOptions>(options =>
            {
                MapPromatEmailSenderOptions(options, configuration);
                options.Smtp = new SmtpOptions();
                MapSmtpOptions(options.Smtp, configuration);
            });
            services.Configure<SmtpOptions>(options => MapSmtpOptions(options, configuration));

            services.AddTransient<IEmailSender, SmtpSender>(provider =>
            {
                var options = provider.GetRequiredService<IOptions<PromatEmailSenderOptions>>().Value;
                var smtpSender = new SmtpSender(provider.GetService<ILogger<SmtpSender>>(), options.Smtp.Host, options.Smtp.Port, options.Smtp.User, options.Smtp.Password, options.Smtp.TlsEnabled, options.DefaultFromEmail, options.DefaultFromName);
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
        public static IServiceCollection AddPromatEmailSenderSendGrid(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<PromatEmailSenderOptions>(options =>
            {
                MapPromatEmailSenderOptions(options, configuration);
                options.SendGrid = new SendGridOptions();
                MapSendGridOptions(options.SendGrid, configuration);
            });
            services.Configure<SendGridOptions>(options => MapSendGridOptions(options, configuration));

            services.AddTransient<IEmailSender, SendGridSender>(provider =>
            {
                WebProxy proxy = null;
                var options = provider.GetRequiredService<IOptions<PromatEmailSenderOptions>>().Value;
                if (!string.IsNullOrWhiteSpace(options.Proxy))
                {
                    proxy = new WebProxy
                    {
                        Address = new Uri(options.Proxy),
                        UseDefaultCredentials = true
                    };
                }
                return new SendGridSender(options.SendGrid.ApiKey, provider.GetService<ILogger<SendGridSender>>(), options.DefaultFromEmail, options.DefaultFromName, proxy);
            });
            return services;
        }

        private static void MapPromatEmailSenderOptions(PromatEmailSenderOptions options, IConfiguration configuration)
        {
            options.DefaultFromEmail = configuration[PromatEmailSenderOptions.DefaultFromEmailKey];
            options.DefaultFromName = configuration[PromatEmailSenderOptions.DefaultFromNameKey];
            options.Proxy = configuration[PromatEmailSenderOptions.ProxyKey];
        }
        private static void MapSmtpOptions(SmtpOptions options, IConfiguration configuration)
        {
            options.Host = configuration[SmtpOptions.HostKey];
            options.User = configuration[SmtpOptions.UserKey];
            options.Password = configuration[SmtpOptions.PasswordKey];

            if (int.TryParse(configuration[SmtpOptions.PortKey], out var port))
            {
                options.Port = port;
            }
            if (bool.TryParse(configuration[SmtpOptions.TlsEnabledKey], out var tlsEnabled))
            {
                options.TlsEnabled = tlsEnabled;
            }
        }
        private static void MapSendGridOptions(SendGridOptions options, IConfiguration configuration)
        {
            options.ApiKey = configuration[SendGridOptions.ApiKeyKey];
        }
    }
}
