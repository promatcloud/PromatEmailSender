<h1 align="center">
 MailTemplate
</h1>

Plantilla simple para gestionar la información en un envio por mail

MailTemplate está diponible por **NuGet [PromatEmailSender](https://www.nuget.org/packages/PromatEmailSender/)**

# Generalidades
La Plantilla está implementad en nerstandard 2, por lo que puede ser usada en todo tipo de prroyectos:
 - El uso en core se puede realizar mediante el inyector de dependencias y configurar las generalidades en appsettings (o cualquier método de configuración establecido en la app).
 - En full framework tendremos que instanciar la clase que queramos usar facilitándole los datos de configuración en el constructor.

# Net Core
Configuración:
```json
{
  "PromatEmailSender": {
    "DefaultFromEmail": "Optional: MyApplicationEmail@MyDomain.com",
    "DefaultFromName": "Optional: My application name",
    "Proxy": "Optional:https://myproxy:port",
    "SendGrid": {
      "ApiKey": "MySendGridApiKey"
    },
    "Smtp": {
      "Host": "smtp.myEmailServer.com",
      "Port": 587,
      "TlsEnabled": true,
      "User": "user@myEmailServer.com",
      "Password": "******************",
      "IgnoreRemoteCertificateChainErrors": false,
      "IgnoreRemoteCertificateNameMismatch": false,
      "IgnoreRemoteCertificateNotAvailable": false,
      "SecurityProtocol": {
        "Ssl3": false,
        "Tls": false,
        "Tls11": true,
        "Tls12": true
      }
    }
  }
}
```
Según el uso, sólo tendremos que definir el bloque "SendGrid" o el bloque "Smtp".
Los campos de configuración obligatorios según si queremos usar SendGrid o SMTP son:
- SendGrid: 
	- PromatEmailSender -> SendGrid
		- ApiKey
- Stmp: 
	- PromatEmailSender -> Smtp -> 
		- Host
		- Port
		- User
		- Password
Para utilizar el envío por **MailMaker** en nuestra app debemos agregar los servicios en la clase **startup.cs**
```csharp
public class Startup
{
    public IConfiguration Configuration { get; }
    
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
	    // some code
        services.AddMailMaker();
	    // some code
    }
}
```

De igual modo si queremos utilizar el envío por **MailMaker** recuperamos el servicio 
```csharp

class Program
{    
    static async Task Main(string[] args)
    {
        var host = CreateHost(builder);
        await EmailTemplateTest(host);
    }
    
    static async Task EmailTemplateTest(IHost host)
    {      
        var mailMaker = host.Services.GetRequiredService<IMailMaker>()
    }
}
```
# Uso en Full Framework
En full framework nos construimos la clase del MailMaker que nos interese de forma manual pasandole en el constructor la información necesaria, Por ejemplo:
```csharp

class Program
{
    private const string SmtpHost = "mail.server.com";
    private const int SmtpPort = 587;
    private const bool SmtpTlsEnabled = true;
    private const string SmtpUser = "user@server.com";
    private const string SmtpPassword = "**********************";
    
    static async Task Main(string[] args)
    {
        await EmailTemplateTest();
    }

    private static async Task EmailTemplateTest()
    {      
        var mailMaker = MailMaker.New(new MailConfigurator(),new SmtpSender(SmtpHost,SmtpPort,SmtpUser,SmtpPassword,SmtpTlsEnabled),null); 
    }
}
```
# Uso de MailConfigurator
Tenemos diferentes métodos para hacer configuraciones generales desde el objeto MailConfigurator, estas configuraciones son usadas a la hora de solicitar la plantilla al objeto MailMaker. Inicializado el objeto MailMaker tenemos que llamar al método Configure() para empezar las configuraciones generales. Una vez configurado todo lo deseado invocaremos al método EndConfiguration() para salir de las configuraciones. Todo ello se realiza por medio de métodos encadenados.
Ejemplo:

```csharp

private static async Task EmailTemplateTest()
{      
	var mailMaker = MailMaker.New(new MailConfigurator(),new SmtpSender(SmtpHost,SmtpPort,SmtpUser,SmtpPassword,SmtpTlsEnabled),null)

	mailMaker.Configure()
                    .BackgroundEvenLine("#CCC")
                    .BackgroundOddLine(Color.Beige)
                    .BackgroundTitle(Color.Yellow)
                    .SetPathPicture("https://raw.githubusercontent.com/promatcloud/Branding/master/icons/org/promat.512.png")
                    .SetPathPicture("https://raw.githubusercontent.com/promatcloud/Branding/master/AnimalFeeding/AnimalFeeding_512.png")
                    .IsToggleColor(true)
                    .SetPercentageTwoColumn(30)
                    .SetPercentageThreeColumn(30,35)
                    .SetCorreoWidth(500)
                    .SetImageSize(150, 150)
                    .SetLinksColorsAndTextDecoration(Color.Black,Color.Blue, Color.BlueViolet,Color.Aqua)
                    .SetFontGenericFamilyAndFontSize(HtmlFontFamilyEnum.Arial,HtmlFontFamilyEnum.Geneva,HtmlGenericFamilyEnum.SansSerif,20)
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
