namespace Promat.EmailSender.Options
{
    public class PromatEmailSenderOptions
    {
        internal const string Section = "PromatEmailSender";
        internal const string DefaultFromEmailKey = Section + ":" + nameof(DefaultFromEmail);
        internal const string DefaultFromNameKey = Section + ":" + nameof(DefaultFromName);
        internal const string ProxyKey = Section + ":" + nameof(Proxy);

        /// <summary>
        /// (opcional) Email desde el que se enviarán los correos por defecto
        /// </summary>
        public string DefaultFromEmail { get; set; }
        /// <summary>
        /// (opcional) Nombre por defecto para el <see cref="DefaultFromEmail"/>
        /// </summary>
        public string DefaultFromName { get; set; }
        /// <summary>
        /// (opcional) Url del proxy
        /// </summary>
        public string Proxy { get; set; }
        /// <summary>
        /// Opciones de configuración para <see cref="SendGridSender"/>
        /// </summary>
        public SendGridOptions SendGrid { get; set; }
        /// <summary>
        /// Opciones de configuración para <see cref="SmtpSender"/>
        /// </summary>
        public SmtpOptions Smtp { get; set; }
    }
}
