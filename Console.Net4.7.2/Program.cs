using System;
using System.Threading.Tasks;
using Promat.EmailSender;
using Promat.EmailSender.MailTemplate;
using Promat.EmailSender.MailTemplate.Enums;

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
            string userResponse;
            do
            {
                try
                {
                    await EmailTemplateTest();
                    await SendMailAsync();
                    System.Console.WriteLine("Email enviado");
                }
                catch (Exception e)
                {
                    System.Console.WriteLine(e);
                    System.Console.WriteLine();
                }
                System.Console.Write("¿quieres mandar otro? (y/n): ");

                userResponse = System.Console.ReadLine();
            } while (userResponse == "y");
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
            emailSender.EnableTls11SecurityProtocol();
            emailSender.EnableTls12SecurityProtocol();
            await emailSender.SendEmailAsync(to, subject, null, message);
        }
        private static async Task EmailTemplateTest()
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

                    //.ConfigureMail()
                    //.BackgroundTitle("#F00")
                    //.BackgroundOddLine("#808080")
                    //.BackgroundEvenLine("rgb(255,255,0)")
                    //.SetPathPicture("https://raw.githubusercontent.com/promatcloud/Branding/master/icons/org/promat.512.png")
                    //.IsToggleColor(false)
                    //.EndMailConfigurator()

                    .TitleHeader("Titulo", HtmlHeaderEnum.H1)
                    .AddLine("Texto de línea", true, true, HtmlTextAlignEnum.Center)

                    .AddLineWithImage(
                        "https://raw.githubusercontent.com/promatcloud/Branding/master/AnimalFeeding/CirculoVerdeClaro_032.png",
                        "Texto imagen",30)
                    .AddLineWithImage(
                        "https://raw.githubusercontent.com/promatcloud/Branding/master/AnimalFeeding/AnimalFeeding_512.png",
                        "Texto imagen")

                    .AddLine("Probar letra negrita", "texto", false, true)
                    .AddLine(left, right)
                    .AddLine(left, right, true)
                ;
            string htmlmailMaker = mailMaker.GetHtml();
            System.Console.WriteLine(htmlmailMaker);
            //await mailMaker.SendMailAsync("correo@correo.com", "Prueba de correo", new []{"copia@correo.com", "copia@correo.com"});

        }
    }
}
