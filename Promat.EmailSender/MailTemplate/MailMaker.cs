using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Promat.EmailSender.Interfaces;
using Promat.EmailSender.MailTemplate.Enums;
using Promat.EmailSender.MailTemplate.Interfaces;

namespace Promat.EmailSender.MailTemplate
{
    // TODO: loggear lo que se estime
    public partial class MailMaker : IMailMaker
    {
        private readonly IMailConfigurator _mailConfigurator;
        private readonly List<(int lineNumber, string text, bool isTitle, bool fontBold, HtmlTextAlignEnum htmlTextAlignEnum)> _lines = new();
        private readonly List<(int lineNumber, string urlImage, string rightLine, int maxWidth, bool fontBoldRight)> _lineWithImagen = new();
        private readonly List<(int lineNumber, string leftLine, string rightLine, bool fontBoldLeft, bool fontBoldRight)> _linesWithColumns = new();
        private readonly List<(int lineNumber, IEnumerable<string> leftLines, IEnumerable<string> rightLines, bool fontBoldLeft, bool fontBoldRight)> _multipleLinesWithColumns = new();
        
        private int _lineCount;
        private string _titleText;
        private HtmlHeaderEnum _htmlHeaderEnum;
        private IEmailSender _emailSender;
        private ILogger _logger;

        private MailMaker(IMailConfigurator mailConfigurator)
        {
            mailConfigurator.SetMailMaker(this);
            _mailConfigurator = mailConfigurator;
        }
        private MailMaker(IMailConfigurator mailConfigurator, IEmailSender emailSender, ILogger logger)
        {
            mailConfigurator.SetMailMaker(this);
            _mailConfigurator = mailConfigurator;
            _emailSender = emailSender;
            _logger = logger;
        }

        public static MailMaker New() => new(new MailConfigurator());
        public static MailMaker New(IMailConfigurator configurator, IEmailSender emailSender, ILogger logger) => 
            new(configurator, emailSender, logger);

        public IMailConfigurator Configure()
        {
            return _mailConfigurator;
        }
        public IMailMaker TitleHeader(string title, HtmlHeaderEnum headerEnum = HtmlHeaderEnum.H3)
        {
            _logger.LogInformation($"TitleHeader variables: title -> {title} , headerEnum -> {headerEnum}");
            _titleText = title;
            _htmlHeaderEnum = headerEnum;
            return this;
        }
        public IMailMaker AddLine(string lineText, bool isTitle = false, bool fontBold = false, HtmlTextAlignEnum htmlTextAlignEnum = HtmlTextAlignEnum.Left)
        {
            _logger.LogInformation($"AddLine variables: lineText -> {lineText}, isTitle -> {isTitle}, fontBold -> {fontBold}, htmlTextAlignEnum -> {htmlTextAlignEnum}");
            _lines.Add((_lineCount++, lineText, isTitle, fontBold, htmlTextAlignEnum));
            return this;
        }
        public IMailMaker AddLine(string leftLine, string rightLine, bool fontBoldLeft = false, bool fontBoldRight = false)
        {
            _logger.LogInformation($"AddLine variables: leftLine -> {leftLine}, rightLine -> {rightLine}, fontBoldLeft -> {fontBoldLeft}, fontBoldRight -> {fontBoldRight}");
            _linesWithColumns.Add((_lineCount++, leftLine, rightLine, fontBoldLeft, fontBoldRight));
            return this;
        }
        public IMailMaker AddLineWithImage(string urlImage, string rightLine, int maxWidth = 20, bool fontBoldRight = false)
        {
            _logger.LogInformation($"AddLineWithImage variables: urlImage -> {urlImage}, rightLine -> {rightLine}, maxWidth -> {maxWidth}, fontBoldRight -> {fontBoldRight}");
            _lineWithImagen.Add((_lineCount++, urlImage, rightLine, maxWidth, fontBoldRight));
            return this;
        }
        public IMailMaker AddLine(IEnumerable<string> leftLines, IEnumerable<string> rightLines, bool fontBoldLeft = false, bool fontBoldRight = false)
        {
            _logger.LogInformation($"AddLine variables: leftLines -> {leftLines}, rightLines -> {rightLines}, fontBoldLeft -> {fontBoldLeft}, fontBoldRight -> {fontBoldRight}");
            _multipleLinesWithColumns.Add((_lineCount++, leftLines, rightLines, fontBoldLeft, fontBoldRight));
            return this;

        }
        public string GetHtml()
        {
            var template = new StringBuilder();

            template.Append(TemplateHtmlHead
                .Replace(TagCol1Percentage, _mailConfigurator.PercentageColumn.ToString())
                .Replace(TagCol2Percentage,  (100 -_mailConfigurator.PercentageColumn).ToString())
                .Replace(TagHeaderCol1Px, _mailConfigurator.HeaderImageWidth.ToString())
                .Replace(TagHeaderCol2Px,  (_mailConfigurator.CorreoWidth -_mailConfigurator.HeaderImageWidth).ToString())
                .Replace(TagCorreoWidth,  _mailConfigurator.CorreoWidth.ToString())
            );
            template.Append(TemplateHtmlConfigureHeadEmail
                .Replace(TagImage, _mailConfigurator.PathPicture)
                .Replace(TagTitle, _titleText)
                .Replace(TagHeaderSize, _htmlHeaderEnum.ToString())
                .Replace(TagPaddingTopTitulo, (_mailConfigurator.HeaderImageHeight - 65).ToString())
            );
            template.Append(TemplateHtmlSeparatorLine);

            var numberLine = 0;

            for (var lineNumber = 0; lineNumber < _lineCount; lineNumber++)
            {
                if (_lines.SingleOrDefault(x => x.lineNumber == lineNumber) is { } line)
                {
                    if (!string.IsNullOrWhiteSpace(line.text))
                    {
                        if (!line.isTitle)
                        {
                            numberLine++;
                        }

                        template.Append(TemplateHtmlAddLine
                            .Replace(TagLine, line.fontBold ? "<strong>" + line.text + "</strong>" : line.text)
                            .Replace(TagTextAlign, line.htmlTextAlignEnum.Print())
                            .Replace(TagColorLine,
                                line.isTitle
                                    ? _mailConfigurator.BackgroundColorTitle
                                    : (numberLine % 2 == 0
                                        ? _mailConfigurator.BackgroundColorEventLine
                                        : _mailConfigurator.BackgroundColorOodLine))
                        );
                    }
                }
                if (_lineWithImagen.SingleOrDefault(x => x.lineNumber == lineNumber) is { } lineWithImagen)
                {
                    if (!string.IsNullOrWhiteSpace(lineWithImagen.urlImage) && !string.IsNullOrWhiteSpace(lineWithImagen.rightLine))
                    {
                        numberLine++;
                        template.Append(TemplateHtmlAddLineTwoColumns
                            .Replace(TagLeftColumn, GetTagImage(lineWithImagen.urlImage, lineWithImagen.maxWidth))
                            .Replace(TagRightColumn, GetTagLine(lineWithImagen.fontBoldRight, lineWithImagen.rightLine))
                            .Replace(TagColorLine,
                                _mailConfigurator.IsToggleColorInLines ?
                                    (numberLine % 2 == 0
                                        ? _mailConfigurator.BackgroundColorEventLine
                                        : _mailConfigurator.BackgroundColorOodLine)
                                    : _mailConfigurator.BackgroundColorOodLine)
                            );
                    }
                }
                if (_linesWithColumns.SingleOrDefault(x => x.lineNumber == lineNumber) is { } lineWithColumns)
                {
                    if (!string.IsNullOrWhiteSpace(lineWithColumns.leftLine) && !string.IsNullOrWhiteSpace(lineWithColumns.rightLine))
                    {
                        numberLine++;
                        template.Append(TemplateHtmlAddLineTwoColumns
                            .Replace(TagLeftColumn, GetTagLine(lineWithColumns.fontBoldLeft, lineWithColumns.leftLine))
                            .Replace(TagRightColumn, GetTagLine(lineWithColumns.fontBoldRight, lineWithColumns.rightLine))
                            .Replace(TagColorLine,
                                _mailConfigurator.IsToggleColorInLines ?
                                (numberLine % 2 == 0
                                    ? _mailConfigurator.BackgroundColorEventLine
                                    : _mailConfigurator.BackgroundColorOodLine)
                                : _mailConfigurator.BackgroundColorOodLine)
                        );
                    }
                }
                if (_multipleLinesWithColumns.SingleOrDefault(x => x.lineNumber == lineNumber) is { } multipleLinesWithColumns)
                {
                    if (multipleLinesWithColumns.leftLines != null & multipleLinesWithColumns.rightLines != null)
                    {
                        var tagLeftColumn = string.Empty;
                        var tagRightColumn = string.Empty;

                        tagLeftColumn = multipleLinesWithColumns.leftLines.Select(line => GetTagLine(multipleLinesWithColumns.fontBoldLeft, line))
                            .Aggregate(tagLeftColumn, (current, tagLine) => current + tagLine);
                        tagRightColumn = multipleLinesWithColumns.rightLines.Select(line => GetTagLine(multipleLinesWithColumns.fontBoldRight, line))
                            .Aggregate(tagRightColumn, (current, tagLine) => current + tagLine);

                        numberLine++;
                        template.Append(TemplateHtmlAddLineTwoColumns
                            .Replace(TagLeftColumn, tagLeftColumn)
                            .Replace(TagRightColumn, tagRightColumn)
                            .Replace(TagColorLine,
                                _mailConfigurator.IsToggleColorInLines ?
                                    (numberLine % 2 == 0
                                        ? _mailConfigurator.BackgroundColorEventLine
                                        : _mailConfigurator.BackgroundColorOodLine)
                                    : _mailConfigurator.BackgroundColorOodLine
                        ));
                    }
                }
            }
            template.Append(TemplateHtmlEnd);
            Console.WriteLine(_logger.ToString());
            return template.ToString();
        }
        public IMailMaker SetEmailSender(IEmailSender emailSender)
        {
            _emailSender = emailSender;
            return this;
        }
        public Task SendMailAsync(string to, string subject, IEnumerable<string> cc = null, string fromEmail = null, string fromName = null)
        {
            if (_emailSender == null)
            {
                throw new InvalidOperationException("No se ha establecido ninguna instancia de Promat.EmailSender.Interfaces.IEmailSender");
            }

            return _emailSender.SendEmailAsync(to, cc, subject, GetHtml(), string.Empty, fromEmail, fromName);
        }

        public void Dispose()
        {
            _emailSender?.Dispose();
            _logger.LogInformation("_emailSender?.Dispose()");
        }
    }
}
