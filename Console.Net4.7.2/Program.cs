using System.Threading.Tasks;
using Promat.EmailSender;

namespace Console.Net4._7._2
{
    class Program
    {
        private const string SmtpHost = "mail.server.com";
        private const int SmtpPort = 587;
        private const bool SmtpTlsEnabled = true;
        private const string SmtpUser = "user@server.com";
        private const string SmtpPassword = "**********************";
        
        static async Task Main(string[] args)
        {
            await SendMailAsync();
        }

        private static async Task SendMailAsync()
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

            var emailSender = new SmtpSender(SmtpHost, SmtpPort, SmtpUser, SmtpPassword, SmtpTlsEnabled);
            await emailSender.SendEmailAsync(to, subject, null, message);
        }
    }
}
