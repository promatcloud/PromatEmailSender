namespace Promat.EmailSender.Options
{
    public class SmtpOptions
    {
        internal const string Section = "PromatEmailSender:Smtp";
        internal const string HostKey = Section + ":" + nameof(Host);
        internal const string PortKey = Section + ":" + nameof(Port);
        internal const string TlsEnabledKey = Section + ":" + nameof(TlsEnabled);
        internal const string UserKey = Section + ":" + nameof(User);
        internal const string PasswordKey = Section + ":" + nameof(Password);

        public string Host { get; set; }
        public int Port { get; set; } = 587;
        public bool TlsEnabled { get; set; } = true;
        public string User { get; set; }
        public string Password { get; set; }
    }
}