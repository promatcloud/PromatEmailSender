using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Promat.EmailSender.Common.Interfaces;
using Promat.EmailSender.MailMaker.Interfaces;

namespace Promat.EmailSender.MailMaker.Extensions;

public static class ExtensionMethods
{
    /// <summary>
    /// Añade los servicios necesarios para poder utilizar la plantilla resolviendo el servicio <see cref="IMailMaker"/>
    /// </summary>
    /// <param name="services">Colección de servicios</param>
    /// <returns><see cref="IServiceCollection"/></returns>
    public static IServiceCollection AddMailMaker(this IServiceCollection services)
    {
        services.AddTransient<IMailConfigurator, MailConfigurator>();
        services.AddTransient<IMailMaker, MailMaker>(provider =>
        {
            var mailMaker = MailMaker.New(provider.GetRequiredService<IMailConfigurator>(),
                provider.GetService<ILogger<MailMaker>>(),
                provider.GetService<IEmailSender>());
            return mailMaker;
        });
        return services;
    }
}