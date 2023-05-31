using Promat.EmailSender.MailTemplate.Interfaces;

namespace Promat.EmailSender.MailTemplate
{
    public class MailConfigurator : IMailConfigurator
    {
        private MailMaker _mailMaker;

        private string _backgroundColorLineOod = "#ffffff";
        private string _backgroundColorLinePair = "#E6E6E6";
        private string _backgroundColorTitle = "#FCEEE2";
        private string _pathPicture = "https://raw.githubusercontent.com/promatcloud/Branding/master/icons/org/promat.512.png";
        private bool _isToggleColorInLines = true;
        
        internal string PathPicture
        {
            get => _pathPicture;
            set => _pathPicture = value;
        }
        internal string BackgroundColorLineOod
        {
            get => $"background-color: {_backgroundColorLineOod};";
            private set => _backgroundColorLineOod = value;
        }
        internal string BackgroundColorLinePair
        {
            get => $"background-color: {_backgroundColorLinePair};";
            private set => _backgroundColorLinePair = value;
        }
        internal string BackgroundColorTitulo
        {
            get => $"background-color: {_backgroundColorTitle};";
            private set => _backgroundColorTitle = value;
        }
        internal bool IsToggleColorInLines
        {
            get => _isToggleColorInLines;
            private set => _isToggleColorInLines = value;
        }

        public MailConfigurator(){}

        internal void SetMailMaker(MailMaker mailMaker) => _mailMaker = mailMaker;
      
        public IMailConfigurator BackgroundTitle(string cssColor)
        {
            BackgroundColorTitulo = cssColor;
            return this;
        }
        public IMailConfigurator BackgroundOddLine(string cssColor)
        {
            BackgroundColorLineOod = cssColor;
            return this;
        }
        public IMailConfigurator BackgroundEvenLine(string cssColor)
        {
            BackgroundColorLinePair = cssColor;
            return this;
        }
        public IMailConfigurator SetPathPicture(string pathPicture)
        {
            PathPicture = pathPicture;
            return this;
        }
        public IMailConfigurator IsToggleColor(bool isDifferent)
        {
            IsToggleColorInLines = isDifferent;
            return this;
        }
        public IMailMaker EndMailConfigurator() => _mailMaker;

    }
}