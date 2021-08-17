using System;
using System.Collections.Generic;
using System.Text;

namespace Promat.EmailSender.Options
{
    public class PromatEmailSenderOptions
    {
        internal const string Section = "PromatEmailSender";
        internal const string DefaultFromEmailKey = Section + ":" + nameof(DefaultFromEmail);
        internal const string DefaultFromNameKey = Section + ":" + nameof(DefaultFromName);
        internal const string ProxyKey = Section + ":" + nameof(Proxy);

        public string DefaultFromEmail { get; set; }
        public string DefaultFromName { get; set; }
        public string Proxy { get; set; }
        public SendGridOptions SendGrid { get; set; }
        public SmtpOptions Smtp { get; set; }
    }
}
