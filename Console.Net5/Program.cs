using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Promat.EmailSender.Extensions;
using Promat.EmailSender.Interfaces;
using Serilog;

// DI, Serilog, Settings

namespace Console.Net5
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var builder = new ConfigurationBuilder();
            BuildConfiguration(builder);
            CreateLogger(builder);

            var host = Host.CreateDefaultBuilder()
                    .ConfigureServices((context, services) =>
                    {
                        services.AddTransient<ISomeService, SomeService>();
                        services.AddPromatEmailSenderSmtp(builder.Build());
                    })
                    .ConfigureLogging((context, loggingBuilder) => loggingBuilder.AddSerilog())
                    .UseSerilog()
                    .Build();

            host.Services.GetRequiredService<ISomeService>().Run();

            var emailSender = host.Services.GetService<IEmailSender>();

            await Task.CompletedTask;
        }

        static void BuildConfiguration(IConfigurationBuilder builder)
        {
            builder.SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production")}.json", optional: true, reloadOnChange: true)
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
