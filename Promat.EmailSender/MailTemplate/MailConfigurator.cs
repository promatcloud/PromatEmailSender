using Promat.EmailSender.MailTemplate.Interfaces;

namespace Promat.EmailSender.MailTemplate
{
    public class MailConfigurator : IMailConfigurator
    {
        private IMailMaker _mailMaker;
        private string _backgroundColorLineOod = "#ffffff";
        private string _backgroundColorLinePair = "#E6E6E6";
        private string _backgroundColorTitle = "#FCEEE2";

        public string PathPicture { get; private set; } = "https://raw.githubusercontent.com/promatcloud/Branding/master/icons/org/promat.512.png";
        public string BackgroundColorLineOod
        {
            get => $"background-color: {_backgroundColorLineOod};";
            private set => _backgroundColorLineOod = value;
        }
        public string BackgroundColorLinePair
        {
            get => $"background-color: {_backgroundColorLinePair};";
            private set => _backgroundColorLinePair = value;
        }
        public string BackgroundColorTitulo
        {
            get => $"background-color: {_backgroundColorTitle};";
            private set => _backgroundColorTitle = value;
        }
        public bool IsToggleColorInLines { get; private set; } = true;


        public IMailConfigurator SetMailMaker(IMailMaker mailMaker)
        {
            _mailMaker = mailMaker;
            return this;
        }
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
        public IMailMaker EndConfiguration() => _mailMaker;
    }
}