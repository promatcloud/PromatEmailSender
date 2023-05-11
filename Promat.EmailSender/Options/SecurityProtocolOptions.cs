namespace Promat.EmailSender.Options
{
    public class SecurityProtocolOptions
    {
        internal const string Section = "PromatEmailSender:Smtp:SecurityProtocol";
        internal const string Ssl3Key = Section + ":" + nameof(Ssl3);
        internal const string TlsKey = Section + ":" + nameof(Tls);
        internal const string Tls11Key = Section + ":" + nameof(Tls11);
        internal const string Tls12Key = Section + ":" + nameof(Tls12);

        /// <summary>
        /// Specifies the Secure Socket Layer (SSL) 3.0 security protocol. SSL 3.0 has been superseded by the Transport Layer Security (TLS) protocol and is provided for backward compatibility only.
        /// </summary>
        public bool Ssl3 { get; set; }
        /// <summary>
        /// Specifies the Transport Layer Security (TLS) 1.0 security protocol. The TLS 1.0 protocol is defined in IETF RFC 2246.
        /// </summary>
        public bool Tls { get; set; }
        /// <summary>
        /// Specifies the Transport Layer Security (TLS) 1.1 security protocol. The TLS 1.1 protocol is defined in IETF RFC 4346. On Windows systems, this value is supported starting with Windows 7.
        /// </summary>
        public bool Tls11 { get; set; }
        /// <summary>
        /// Specifies the Transport Layer Security (TLS) 1.2 security protocol. The TLS 1.2 protocol is defined in IETF RFC 5246. On Windows systems, this value is supported starting with Windows 7.
        /// </summary>
        public bool Tls12 { get; set; }
    }
}