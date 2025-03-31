<h1 align="center">
 MailMaker
</h1>

Plantilla simple para gestionar la información en un envio por mail

MailMaker está diponible por **NuGet [Promat.EmailSender.MailMaker](https://www.nuget.org/packages/Promat.EmailSender.MailMaker/)**

# Generalidades
La Plantilla está implementad en nerstandard 2, por lo que puede ser usada en todo tipo de prroyectos.

# Uso de MailConfigurator
Tenemos diferentes métodos para hacer configuraciones generales desde el objeto MailConfigurator, estas configuraciones son usadas a la hora de solicitar la plantilla al objeto MailMaker. Inicializado el objeto MailMaker tenemos que llamar al método Configure() para empezar las configuraciones generales. Una vez configurado todo lo deseado invocaremos al método EndConfiguration() para salir de las configuraciones. Todo ello se realiza por medio de métodos encadenados.
Ejemplo:

```csharp

private static async Task EmailTemplateTest()
{      
	var mailMaker = MailMaker.New();
           mailMaker.Configure()
                .BackgroundEvenLine("#CCC")
                .BackgroundOddLine(Color.Beige)
                .BackgroundTitle(Color.Yellow)
                .SetPathPicture("https://raw.githubusercontent.com/promatcloud/Branding/master/icons/org/promat.512.png")
                .SetPathPicture("https://raw.githubusercontent.com/promatcloud/Branding/master/AnimalFeeding/AnimalFeeding_512.png")
                .IsToggleColor(true)
                .SetPercentageTwoColumn(30)
                .SetPercentageThreeColumn(30, 35)
                .SetCorreoWidth(500)
                .SetImageSize(150, 150)
                .SetLinksColorsAndTextDecoration(Color.Black, Color.Blue, Color.BlueViolet, Color.Aqua)
                .SetFontGenericFamilyAndFontSize(HtmlFontFamilyEnum.Arial, HtmlFontFamilyEnum.Geneva, HtmlGenericFamilyEnum.SansSerif, 20)
                .EndConfiguration()
}
```
# Uso de MailMaker
Objeto para la construcción de la plantilla y envió del correo, el cual tiene diferentes métodos donde se pueden añadir líneas de diferentes maneras, nos permite recuperar la plantilla para usarla como cuerpo en el modelo de envío que tengamos implementado. También tiene un método que nos permite enviar el correo si hemos realizado las configuraciones previas.
Ejemplo:

```csharp

 private static async Task EmailTemplateTest(IHost host)
        {
            var leftLines = new[] {"Columna izquierda fila 1", "Columna izquierda fila 2", "Columna izquierda fila 3"};
            var rightLines = new[] {"Columna derecha fila 1", "Columna derecha fila 2", "Columna derecha fila 3"};

            var mailMaker = host.Services.GetRequiredService<IMailMaker>();

            mailMaker.Configure()
                // Configuraciones
                .EndConfiguration()

                .TitleHeader("Titulo", HtmlHeaderEnum.H1)
                .AddLineWithImage("https://raw.githubusercontent.com/promatcloud/Branding/master/AnimalFeeding/CirculoVerdeClaro_032.png",
                        "Texto de la imagen", maxWidth: 20, fontBoldRight: true, colorLinea: Color.Brown)
                .AddLineWithImage("https://raw.githubusercontent.com/promatcloud/Branding/master/AnimalFeeding/CirculoVerdeClaro_032.png"
                    , "Texto izquierda", "Texto derecha")
                .AddLine("Texto que es título, en negrita, es hipervínculo con dos tabulaciones y alineado a la izquierda"
                    , isTitle: true, fontBold: true
                    , isLinkTarget: true, isLinkText: false, colorLinea: Color.Brown
                    , htmlTabulationEnum: HtmlTabulationEnum.Two, htmlTextAlignEnum: HtmlTextAlignEnum.Left)
                .AddLine("Línea simple con dos columnas", "Texto columna derecha"
                    , false, true, isLinkTarget: false)
                .AddLine(leftLines, rightLines)
                .AddLine("Texto que es enlace del vínculo",isLinkText:true)
                .SetEmailSender(new SendGridSender("apiKey"))
                .SendMailAsync("to", "subject", new[] { "cc" }, "remitente@remi.com", "Nombre remitente")
                .Dispose()
                ;
            // Método para obtener la plantilla de Html por medio de un string 
            var htmlMailMaker = mailMaker.GetHtml();
            System.Console.WriteLine(htmlMailMaker);
        }
```