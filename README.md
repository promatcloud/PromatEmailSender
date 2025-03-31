<h1 align="center">
<img src="https://github.com/promatcloud/Branding/blob/master/icons/PromatEmailSender/promatemailsenderarroba.512.png?raw=true" alt="promat" width="256"/>
 <br/>
 PromatEmailSender
</h1>

<div align="center">

[![Build status](https://ci.appveyor.com/api/projects/status/e6m2m84bn51mq7t8?svg=true)](https://ci.appveyor.com/project/promatcloud/promatemailsender)
 
[![NuGet version](https://badge.fury.io/nu/PromatEmailSender.svg)](https://badge.fury.io/nu/PromatEmailSender) **Promat.EmailSender.Smtp**

</div>
Simple library in net standard to send emails via SMTP

Está diponible por **NuGet [Promat.EmailSender.Smtp](https://www.nuget.org/packages/PromatEmailSender/)**

Plantilla simple para gestionar la información en un envio por mail 
**[MailTemplate](https://github.com/promatcloud/PromatEmailSender/blob/master/Promat.EmailSender/MailTemplate/).**

# Generalidades
La librería está implementada en netstandard 2, por lo que puede ser usada en todo tipo de proyectos:
 - El uso en core se puede realizar mediante el inyector de dependencias y configurar las generalidades en appsettings (o cualquier método de configuración establecido en la app).
 - En full framework tendremos que instanciar la clase que queramos usar facilitándole los datos de configuración en el constructor.

# Net Core
Configuración:
```json
{
  "Promat": {
    "EmailSender": {
      "Smtp": {
        "Host": "smtp.myEmailServer.com",
        "Port": 587,
        "TlsEnabled": true,
        "User": "user@myEmailServer.com",
        "Password": "******************",
        "DefaultFromEmail": "Optional: MyApplicationEmail@MyDomain.com",
        "DefaultFromName": "Optional: My application name",
        "Proxy": "Optional:https://myproxy:port",
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
}
```
Los campos de configuración obligatorios son:
	- Promat
      - EmailSender 
        - Smtp 
		  - Host
		  - Port
		  - User
		  - Password

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

Para utilizar el envío **STMP** en nuestra app debemos agregar los servicios en la clase **startup.cs** o en el sitio correspondiente donde registremos los servicios en el contenedor de inyección de dependencias
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
    // Contiene la configuración referente a PromatEmailSender SMTP
    private readonly SmtpOptions _smtpOptions;
    // Contiene la configuración referente a los protocolos de seguridad 
    // de la capa de transporte
    private readonly SecurityProtocolOptions _securityProtocolOptions;
    
    public MyService(IOptions<SmtpOptions > smtpOptions,
					 IOptions<SecurityProtocolOptions > securityProtocolOptions)
    {
        _smtpOptions= smtpOptions.Value;
        _securityProtocolOptions= securityProtocolOptions.Value;
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
