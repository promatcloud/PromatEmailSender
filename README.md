<h1 align="center">
<img src="https://github.com/promatcloud/Branding/blob/master/icons/PromatEmailSender/promatemailsenderarroba.512.png" alt="promat" width="256"/>
 <br/>
 PromatEmailSender
</h1>

<div align="center">

[![Build status](https://ci.appveyor.com/api/projects/status/e6m2m84bn51mq7t8?svg=true)](https://ci.appveyor.com/project/promatcloud/promatemailsender)
[![NuGet Badge](https://buildstats.info/nuget/PromatEmailSender?includePreReleases=true)](https://www.nuget.org/packages/PromatEmailSender/)

</div>
Simple library in net standard to send emails via SMTP or SendGrid service

PromatEmailSender está diponible por **NuGet [PromatEmailSender](https://www.nuget.org/packages/PromatEmailSender/)**

# Generalidades
La librería está implementada en netstandard 2, por lo que puede ser usada en todo tipo de proyectos:
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
    // Define SendGrid or Smtp configuration, al least one. If both are defined, smtp will be used
    "SendGrid": {
      "ApiKey": "MySendGridApiKey"
    },
    "Smtp": {
      "Host": "smtp.myEmailServer.com",
      "Port": 587,
      "TlsEnabled": true,
      "User": "user@myEmailServer.com",
      "Password": "******************"
      // Optionals
      "IgnoreRemoteCertificateChainErrors": false,
      "IgnoreRemoteCertificateNameMismatch": false,
      "IgnoreRemoteCertificateNotAvailable": false
    }
  }
}
```
Los campos de configuración obligatorios según si queremos usar SendGrid o SMTP son:
* SendGrid: 
-- PromatEmailSender -> SendGrid -> ApiKey
* Stmp: PromatEmailSender -> Smtp -> 
-- Host
-- Port
-- User
-- Password

Para utilizar el envío por **SendGrid** en nuestra app debemos agregar los servicios en la clase **startup.cs**
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
	    services.AddPromatEmailSenderSendGrid(Configuration);
	    // some code
    }
}
```

De igual modo si queremos utilizar el envío por **STMP**
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
	    services.AddPromatEmailSenderSmtp(Configuration);
	    // some code
    }
}
```

Si quisiésemos **recuperar configuración** mediante el ID
```csharp
public class MyService
{
	// Contiene toda la configuración del paquete
    private readonly PromatEmailSenderOptions _promatEmailSenderOptions;
    // Contiene la configuración referente a SendGrid
    private readonly SendGridOptions _sendGridOptions;
    // Contiene la configuración referente a SMTP
    private readonly SmtpOptions _smtpOptions;
    
    public MyService(IOptions<PromatEmailSenderOptions> promatEmailSenderOptions, IOptions<SendGridOptions> sendGridOptions, IOptions<SmtpOptions > smtpOptions)
    {
        _promatEmailSenderOptions = promatEmailSenderOptions.Value;
        _sendGridOptions= sendGridOptions.Value;
        _smtpOptions= smtpOptions.Value;
    }
}
```
**Para enviar emails**, solicitamos al DI el sender configurado y llamamos al método de envío
```csharp
public class MyService
{
	private readonly IEmailSender _emailSender;

	public MyService(IEmailSender emailSender)
	{
	    _emailSender = emailSender;
	}

	public async Task SendAsync()
	{
	    await _emailSender.SendEmailAsync("toEmail@mail.com", "subject", "<p>My HTML message<p>");
	}
}
```

# Uso en Full Framework
En full framework nos construimos la clase del sender que nos interese de forma manual pasandole en el constructor la información necesaria, Por ejemplo:
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
```

# Logging
Podemos cambiar el nivel de log para PromatEmailSender en el archivo de configuración, estableciendo el nivel de log para el espacio de nombres "Promat.EmailSender"

Con el logger por defecto de Core
```csharp
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information",
      "Promat.EmailSender": "Debug"
    }
  }
}
```

Con Serilog
```csharp
{
  "Serilog": {
    "MinimumLevel": {
      "Default": "Verbose",
      "Override": {
        "Microsoft": "Information",
        "System": "Warning",
        "Promat.EmailSender": "Debug"
      }
    }
  }
}
```