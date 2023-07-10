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
        var mailMaker = MailMaker.New(new MailConfigurator(),new SmtpSender(SmtpHost,SmtpPort,SmtpUser,SmtpPassword,SmtpTlsEnabled),null)
    }
}
```
