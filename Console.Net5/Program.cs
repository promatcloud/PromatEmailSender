using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Promat.EmailSender.Extensions;
using Promat.EmailSender.Interfaces;
using Promat.EmailSender.MailTemplate.Enums;
using Promat.EmailSender.MailTemplate.Interfaces;
using Serilog;

namespace Console.Net5
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var builder = new ConfigurationBuilder();
            BuildConfiguration(builder);
            CreateLogger(builder);
            var host = CreateHost(builder);

            string userResponse;
            do
            {
                await EmailTemplateTest(host);
                await SendMailAsync(host);
                System.Console.WriteLine("Email enviado");
                System.Console.Write("¿quieres mandar otro? (y/n): ");
                userResponse = System.Console.ReadLine();
            } while (userResponse == "y");
        }
        private static async Task SendMailAsync(IHost host)
        {
            System.Console.WriteLine("Introduzca los datos para enviar el correo");
            System.Console.WriteLine("==========================================");
            System.Console.WriteLine("");
            System.Console.Write("Para: ");
            var to = System.Console.ReadLine();
            System.Console.Write("Asunto: ");
            var subject = System.Console.ReadLine();
            System.Console.Write("Mensaje: ");
            var message = System.Console.ReadLine();

            var emailSender = host.Services.GetRequiredService<IEmailSender>();
            await emailSender.SendEmailAsync(to, subject, null, message);
        }
        private static IHost CreateHost(ConfigurationBuilder builder)
        {
            return Host.CreateDefaultBuilder()
                    .ConfigureServices((_, services) =>
                    {
                        services.AddPromatEmailSenderSmtp(builder.Build());
                        services.AddMailMaker();
                    })
                    .ConfigureLogging((_, loggingBuilder) => loggingBuilder.AddSerilog())
                    .UseSerilog()
                    .Build();
        }
        static void BuildConfiguration(IConfigurationBuilder builder)
        {
            builder.SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true, reloadOnChange: true)
                    .AddUserSecrets<Program>(true)
                    .AddEnvironmentVariables();
        }
        static void CreateLogger(IConfigurationBuilder builder)
        {
            Log.Logger = new LoggerConfiguration()
                  .ReadFrom.Configuration(builder.Build())
                  .Enrich.FromLogContext()
                  .WriteTo.Console()
                  .CreateLogger();
        }
        static async Task EmailTemplateTest(IHost host)
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
            var mailMaker = host.Services.GetRequiredService<IMailMaker>()
                    
                    .Configure()
                    .BackgroundEvenLine("#CCC")
                    .BackgroundOddLine(Color.Beige)
                    .BackgroundTitle(color)
                    .SetPathPicture("https://raw.githubusercontent.com/promatcloud/Branding/master/icons/org/promat.512.png")
                    .SetPathPicture("https://raw.githubusercontent.com/promatcloud/Branding/master/AnimalFeeding/AnimalFeeding_512.png")
                    .SetImageSize(150,150)
                    .SetPercentageColumn(15)
                    .SetCorreoWidth(500)
                    .EndConfiguration()

                    .TitleHeader("Titulo", HtmlHeaderEnum.H1)
                    .AddLine("Texto de la línea", true, true, HtmlTextAlignEnum.Left)

                    .AddLineWithImage(
                        "https://raw.githubusercontent.com/promatcloud/Branding/master/AnimalFeeding/CirculoVerdeClaro_032.png",
                        "Texto imagen")
                    .AddLineWithImage(
                        "https://raw.githubusercontent.com/promatcloud/Branding/master/AnimalFeeding/AnimalFeeding_512.png",
                        "Texto imagen")

                    .AddLine("Probar letra en negrita", "texto", false, true)
                    .AddLine(left, right)
                    .AddLine(left, right, true)
                ;
            var htmlMailMaker = mailMaker.GetHtml();
            System.Console.WriteLine(htmlMailMaker);
            await Task.CompletedTask;
            //await mailMaker.SendMailAsync("correo@correo.com", "Prueba de correo", new []{"copia@correo.com", "copia@correo.com"});
        }
    }
}
