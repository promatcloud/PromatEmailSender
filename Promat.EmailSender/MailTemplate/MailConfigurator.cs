using System.Drawing;
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
        public string BackgroundColorOodLine
        {
            get => $"background-color: {_backgroundColorLineOod};";
            private set => _backgroundColorLineOod = value;
        }
        public string BackgroundColorEventLine
        {
            get => $"background-color: {_backgroundColorLinePair};";
            private set => _backgroundColorLinePair = value;
        }
        public string BackgroundColorTitle
        {
            get => $"background-color: {_backgroundColorTitle};";
            private set => _backgroundColorTitle = value;
        }
        public bool IsToggleColorInLines { get; private set; } = true;
        public int PercentageColumn { get; private set; } = 20;
        public int HeaderImageWidth { get; private set; } = 280;
        public int HeaderImageHeight { get; private set; } = 280;
        public int CorreoWidth { get; private set; } = 840;

        public IMailConfigurator SetMailMaker(IMailMaker mailMaker)
        {
            _mailMaker = mailMaker;
            return this;
        }
        public IMailConfigurator BackgroundTitle(string cssColor)
        {
            BackgroundColorTitle = cssColor;
            return this;
        }
        public IMailConfigurator BackgroundTitle(Color cssColor)
        {
            BackgroundColorTitle = ToHex(cssColor);
            return this;
        }
        public IMailConfigurator BackgroundOddLine(string cssColor)
        {
            BackgroundColorOodLine = cssColor;
            return this;
        }
        public IMailConfigurator BackgroundOddLine(Color cssColor)
        {
            BackgroundColorOodLine = ToHex(cssColor);
            return this;
        }
        public IMailConfigurator BackgroundEvenLine(string cssColor)
        {
            BackgroundColorEventLine = cssColor;
            return this;
        }
        public IMailConfigurator BackgroundEvenLine(Color cssColor)
        {
            BackgroundColorEventLine = ToHex(cssColor);
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
        public IMailConfigurator SetPercentageColumn(int percentageColumn)
        {
            PercentageColumn = percentageColumn switch
            {
                < 20 => 20,
                > 75 => 75,
                _ => percentageColumn
            };
            return this;
        }
        public IMailConfigurator SetCorreoWidth(int correoWidth)
        {
            CorreoWidth = correoWidth switch
            {
                < 840 => 840,
                > 1450 => 1450,
                _ => correoWidth
            };
            return this;
        }
        public IMailConfigurator SetImageSize(int headerImageWidth, int headerImageHeight)
        {
            HeaderImageHeight = headerImageHeight switch
            {
                < 50 => 50,
                > 1000 => 1000,
                _ => headerImageHeight
            };
            HeaderImageWidth = headerImageWidth switch
            {
                < 50 => 50,
                > 1000 => 1000,
                _ => headerImageWidth
            };
            return this;
        }
        public IMailMaker EndConfiguration() => _mailMaker;

        private string ToHex(Color color)
        {
            var hasAlpha = color.A != byte.MaxValue;
            return hasAlpha
                ? $"#{color.A:X2}{color.R:X2}{color.G:X2}{color.B:X2}"
                : $"#{color.R:X2}{color.G:X2}{color.B:X2}";
        }
        private string ToRgb(Color color)
        {
            var hasAlpha = color.A != byte.MaxValue;
            var alphaValue = "";
            if (hasAlpha)
            {
                alphaValue = (color.A / (double)byte.MaxValue).ToString("#.##");
            }

            return hasAlpha
                ? $"RGBA({color.R}, {color.G}, {color.B}, {alphaValue}"
                : $"RGB({color.R}, {color.G}{color.B})";
        }
    }
}