namespace Promat.EmailSender.Options
{
    public class SendGridOptions
    {
        internal const string Section = "PromatEmailSender:SendGrid";
        internal const string ApiKeyKey = Section + ":" + nameof(ApiKey);

        public string ApiKey { get; set; }
    }
}