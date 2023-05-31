using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Promat.EmailSender.Extensions;
using Promat.EmailSender.Interfaces;
using Promat.EmailSender.MailTemplate;
using Promat.EmailSender.MailTemplate.Enums;
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
                await EmailTemplateTest();
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
                    .ConfigureServices((context, services) =>
                    {
                        services.AddPromatEmailSenderSmtp(builder.Build());
                    })
                    .ConfigureLogging((context, loggingBuilder) => loggingBuilder.AddSerilog())
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
        static async Task EmailTemplateTest()
        {

            string[] left = new[]
            {
                "Columna izquierda fila 1", 
                "Columna izquierda fila 2", 
                "Columna izquierda fila 3"
            };
            string[] right = new[] 
            {
                "Columna derecha fila 1",
                "Columna derecha fila 2",
                "Columna derecha fila 3"
            };

            var mailMaker = MailMaker.New()

                    .ConfigureMail()
                    .BackgroundTitle("#F00")
                    .BackgroundOddLine("#808080")
                    .BackgroundEvenLine("rgb(255,255,0)")
                    .SetPathPicture("https://raw.githubusercontent.com/promatcloud/Branding/master/icons/org/promat.512.png")
                    .IsToggleColor(false)
                    .EndMailConfigurator()

                    .TitleHeader("Titulo", HtmlHeaderEnum.H1)
                    .AddLine("Texto de la línea", true, true, HtmlTextAlignEnum.Center)

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
            string htmlmailMaker = mailMaker.GetHtml();
            System.Console.WriteLine(htmlmailMaker);
            //await mailMaker.SendMailAsync("correo@correo.com", "Prueba de correo", new []{"copia@correo.com", "copia@correo.com"});

        }
    }
}
