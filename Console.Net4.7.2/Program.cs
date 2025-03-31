using Promat.EmailSender.MailMaker;
using Promat.EmailSender.MailMaker.Enums;
using Promat.EmailSender.Smtp;
using System;
using System.Drawing;
using System.Threading.Tasks;

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

            var emailSender = new EmailSender(SmtpHost, SmtpPort, SmtpUser, SmtpPassword, SmtpTlsEnabled);
            emailSender.EnableTls11SecurityProtocol();
            emailSender.EnableTls12SecurityProtocol();
            await emailSender.SendEmailAsync(to, subject, null, message);
        }
        private static async Task EmailTemplateTest()
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
            var mailMaker = MailMaker.New(new EmailSender(SmtpHost, SmtpPort, SmtpUser, SmtpPassword, SmtpTlsEnabled))

                    .Configure()
                    .BackgroundEvenLine("#CCC")
                    .BackgroundOddLine(Color.Beige)
                    .BackgroundTitle(color)
                    .SetPathPicture("https://raw.githubusercontent.com/promatcloud/Branding/master/icons/org/promat.512.png")
                    .SetPathPicture("https://raw.githubusercontent.com/promatcloud/Branding/master/AnimalFeeding/AnimalFeeding_512.png")
                    .SetImageSize(200, 200)
                    .SetPercentageTwoColumn(20)
                    .SetPercentageThreeColumn(50, 50)
                    .SetCorreoWidth(500)


                    .EndConfiguration()

                    .TitleHeader("Titulo", HtmlHeaderEnum.H1, HtmlTextAlignEnum.Left)

                    .AddLine("Seccion1 link", isLinkText: true)
                    .AddLine("Seccion2 link", isLinkText: true)

                    .AddLineWithImage(
                        "https://raw.githubusercontent.com/promatcloud/Branding/master/AnimalFeeding/CirculoVerdeClaro_032.png",
                        "Texto imagen izquierda", "Texto imagen derecha", 20, true, false)

                ;
            var htmlMailMaker = mailMaker.GetHtml();
            System.Console.WriteLine(htmlMailMaker);
            await Task.CompletedTask;
            //await mailMaker.SendMailAsync("correo@correo.com", "Prueba de correo", new []{"copia@correo.com", "copia@correo.com"});
        }
    }
}
