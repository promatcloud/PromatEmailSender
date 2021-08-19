namespace Promat.EmailSender.Options
{
    public class SendGridOptions
    {
        internal const string Section = "PromatEmailSender:SendGrid";
        internal const string ApiKeyKey = Section + ":" + nameof(ApiKey);

        /// <summary>
        /// API key de Sendgrid, Obligatorio si que quiere usar <see cref="SendGridSender"/>
        /// </summary>
        public string ApiKey { get; set; }
    }
}