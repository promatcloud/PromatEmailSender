using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Promat.EmailSender.Extensions;
using Promat.EmailSender.Interfaces;
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
    }
}
