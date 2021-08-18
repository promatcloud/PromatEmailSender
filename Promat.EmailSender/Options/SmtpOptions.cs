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
        internal const string IgnoreRemoteCertificateChainErrorsKey = Section + ":" + nameof(IgnoreRemoteCertificateChainErrors);
        internal const string IgnoreRemoteCertificateNameMismatchKey = Section + ":" + nameof(IgnoreRemoteCertificateNameMismatch);
        internal const string IgnoreRemoteCertificateNotAvailableKey = Section + ":" + nameof(IgnoreRemoteCertificateNotAvailable);

        public string Host { get; set; }
        public int Port { get; set; } = 587;
        public bool TlsEnabled { get; set; } = true;
        public string User { get; set; }
        public string Password { get; set; }
        public bool IgnoreRemoteCertificateChainErrors { get; set; }
        public bool IgnoreRemoteCertificateNameMismatch { get; set; }
        public bool IgnoreRemoteCertificateNotAvailable { get; set; }
    }
}