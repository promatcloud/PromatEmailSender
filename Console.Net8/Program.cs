using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System.Drawing;
using Promat.EmailSender.Common.Interfaces;
using Promat.EmailSender.MailMaker.Enums;
using Promat.EmailSender.MailMaker.Extensions;
using Promat.EmailSender.MailMaker.Interfaces;
using Promat.EmailSender.Smtp.Extensions;

var builder = new ConfigurationBuilder();
BuildConfiguration(builder);
CreateLogger(builder);
var host = CreateHost(builder);

string? userResponse;
do
{
    await EmailTemplateTest(host.Services);
    await SendMailAsync(host.Services);
    Console.WriteLine("Email enviado");
    Console.Write("¿quieres mandar otro? (y/n): ");
    userResponse = Console.ReadLine();
} while (userResponse == "y");

return;

void BuildConfiguration(IConfigurationBuilder configurationBuilder)
{
    configurationBuilder.SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("NETCOREAPP_ENVIRONMENT") ?? "Production"}.json", optional: true, reloadOnChange: true)
            .AddUserSecrets<Program>(true)
            .AddEnvironmentVariables();
}

IHost CreateHost(ConfigurationBuilder configurationBuilder)
{
    return Host.CreateDefaultBuilder()
            .ConfigureServices((_, services) =>
            {
                services.AddPromatEmailSenderSmtp(configurationBuilder.Build());
                services.AddMailMaker();
            })
            .ConfigureLogging((_, loggingBuilder) => loggingBuilder.AddSerilog())
            .UseSerilog()
            .Build();
}

void CreateLogger(IConfigurationBuilder configurationBuilder)
{
    Log.Logger = new LoggerConfiguration()
          .ReadFrom.Configuration(configurationBuilder.Build())
          .Enrich.FromLogContext()
          .WriteTo.Console()
          .CreateLogger();
}

async Task EmailTemplateTest(IServiceProvider services)
{
    var left = new[]
    {
                "Columna izquierda fila 1",
                "Columna izquierda fila 2",
                "Columna izquierda fila 3"
            };
    var right = new[]
    {
                "Columna derecha fila 1",
                "Columna derecha fila 2",
                "Columna derecha fila 3"
            };

    var color = Color.FromArgb(235, 199, 127);
    var mailMaker = services.GetRequiredService<IMailMaker>()

            .Configure()
            .BackgroundEvenLine("#CCC")
            .BackgroundOddLine(Color.Beige)
            .BackgroundTitle(color)
            .SetPathPicture("https://raw.githubusercontent.com/promatcloud/Branding/master/icons/org/promat.512.png")
            .SetPathPicture("https://raw.githubusercontent.com/promatcloud/Branding/master/AnimalFeeding/AnimalFeeding_512.png")
            .SetImageSize(150, 150)
            .SetCorreoWidth(500)
            .EndConfiguration()

            .TitleHeader("Titulo", HtmlHeaderEnum.H1)

            .AddLineWithImage(
                "https://raw.githubusercontent.com/promatcloud/Branding/master/AnimalFeeding/CirculoVerdeClaro_032.png",
                "Texto imagen")
            .AddLineWithImage(
                "https://raw.githubusercontent.com/promatcloud/Branding/master/AnimalFeeding/AnimalFeeding_512.png",
                "Texto imagen")

            .AddLine("Probar letra en negrita", "texto", false, true)
            .AddLine(left, right)
        ;
    var htmlMailMaker = mailMaker.GetHtml();
    Console.WriteLine(htmlMailMaker);
    await Task.CompletedTask;
    //await mailMaker.SendMailAsync("correo@correo.com", "Prueba de correo", new []{"copia@correo.com", "copia@correo.com"});
}

async Task SendMailAsync(IServiceProvider services)
{
    Console.WriteLine("Introduzca los datos para enviar el correo");
    Console.WriteLine("==========================================");
    Console.WriteLine("");
    Console.Write("Para: ");
    var to = Console.ReadLine();
    Console.Write("Asunto: ");
    var subject = Console.ReadLine();
    Console.Write("Mensaje: ");
    var message = Console.ReadLine();

    var emailSender = services.GetRequiredService<IEmailSender>();
    await emailSender.SendEmailAsync(to, subject, null, message);
}