using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Promat.EmailSender.Interfaces;
using Promat.EmailSender.MailTemplate.Enums;
using Promat.EmailSender.MailTemplate.Interfaces;
// ReSharper disable ReplaceObjectPatternWithVarPattern

namespace Promat.EmailSender.MailTemplate
{
    public partial class MailMaker : IMailMaker
    {
        private readonly IMailConfigurator _mailConfigurator;
        private readonly List<(int lineNumber, string text, bool isTitle, bool fontBold, bool isLink, bool isLinkedIt, Color? colorLinea,
            HtmlTabulationEnum htmlTabulationEnum, HtmlTextAlignEnum htmlTextAlignEnum, int linkNumber)> _line = new();
        private readonly List<(int lineNumber, string leftLine, string rightLine, bool fontBoldLeft, bool fontBoldRight, Color? colorLine, bool isLinkLeft, int linkNumber)> _lineWithColumns = new();
        private readonly List<(int lineNumber, string urlImage, string rightLine, int maxWidth, bool fontBoldRight, Color? colorLine)> _lineWithImagen = new();
        private readonly List<(int lineNumber, string urlImage, string leftLine, string rightLine, int maxWidth, bool fontBoldLeft, bool fontBoldRight, Color? colorLine, HtmlTextAlignEnum htmlLeftTextAlignEnum, HtmlTextAlignEnum htmlCenterTextAlignEnum, HtmlTextAlignEnum htmlRightTextAlignEnum)> _lineWithImagenAndTwoColumns = new();
        private readonly List<(int lineNumber, IEnumerable<string> leftLines, IEnumerable<string> rightLines, bool fontBoldLeft, bool fontBoldRight, Color? colorLine)> _multipleLinesWithColumns = new();

        private readonly ILogger _logger;
        private int _lineCount;
        private int _linkCount;
        private int _linkedItCount;
        private string _titleText;
        private HtmlHeaderEnum _htmlHeaderEnum;
        private HtmlTextAlignEnum _htmlTextAlignEnum;
        private IEmailSender _emailSender;

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
        public IMailMaker TitleHeader(string title, HtmlHeaderEnum headerEnum = HtmlHeaderEnum.H3, HtmlTextAlignEnum htmlTextAlignEnum = HtmlTextAlignEnum.Left)
        {
            _logger?.LogTrace("TitleHeader variables: title -> {title}, headerEnum -> {headerEnum}", title, headerEnum);
            _titleText = title;
            _htmlHeaderEnum = headerEnum;
            _htmlTextAlignEnum = htmlTextAlignEnum;
            return this;
        }
        public IMailMaker AddLine(string lineText, bool isTitle = false, bool fontBold = false, bool isLinkTarget = false, bool isLinkText = false, Color? colorLinea = null,
            HtmlTabulationEnum htmlTabulationEnum = HtmlTabulationEnum.Zero, HtmlTextAlignEnum htmlTextAlignEnum = HtmlTextAlignEnum.Left)
        {
            _logger?.LogTrace(
                "AddLine variables: lineText -> {lineText}, isTitle -> {isTitle}, fontBold -> {fontBold}, isLinkTarget => {isLinkTarget}, isLinkText -> {isLinkText}, colorLinea -> {colorLinea}," +
                "htmlTabulationEnum => {htmlTabulationEnum}, htmlTextAlignEnum -> {htmlTextAlignEnum}",
                lineText, isTitle, fontBold, isLinkTarget, isLinkText, colorLinea, htmlTabulationEnum, htmlTextAlignEnum);

            if (isLinkTarget && isLinkText)
            {
                isLinkText = false;
            }

            _line.Add((_lineCount++, lineText, isTitle, fontBold, isLinkTarget, isLinkText, colorLinea, htmlTabulationEnum, htmlTextAlignEnum, isLinkTarget ? ++_linkCount : isLinkText ? ++_linkedItCount : 0));
            return this;
        }
        public IMailMaker AddLine(string leftLine, string rightLine, bool fontBoldLeft = false, bool fontBoldRight = false, bool isLinkTarget = false, Color? colorLinea = null)
        {
            _logger?.LogTrace("AddLine variables: leftLine -> {leftLine}, rightLine -> {rightLine}, fontBoldLeft -> {fontBoldLeft}, fontBoldRight -> {fontBoldRight}, isLinkTarget => {isLinkTarget},  colorLinea -> {colorLinea}",
                leftLine, rightLine, fontBoldLeft, fontBoldRight, isLinkTarget, colorLinea);
            _lineWithColumns.Add((_lineCount++, leftLine, rightLine, fontBoldLeft, fontBoldRight, colorLinea, isLinkTarget, isLinkTarget ? ++_linkCount : 0));
            return this;
        }
        public IMailMaker AddLineWithImage(string urlImage, string rightLine, int maxWidth = 20, bool fontBoldRight = false, Color? colorLinea = null)
        {
            _logger?.LogTrace("AddLineWithImage variables: urlImage -> {urlImage}, rightLine -> {rightLine}, maxWidth -> {maxWidth}, fontBoldRight -> {fontBoldRight}, colorLinea -> {colorLinea}", urlImage, rightLine, maxWidth, fontBoldRight, colorLinea);
            _lineWithImagen.Add((_lineCount++, urlImage, rightLine, maxWidth, fontBoldRight, colorLinea));
            return this;
        }

        public IMailMaker AddLineWithImage(string urlImage, string leftLine, string rightLine,
            int maxWidth = 20, bool fontBoldLeft = false, bool fontBoldRight = false, Color? colorLinea = null,
            HtmlTextAlignEnum htmlLeftTextAlignEnum = HtmlTextAlignEnum.Left, HtmlTextAlignEnum htmlCenterTextAlignEnum = HtmlTextAlignEnum.Left, HtmlTextAlignEnum htmlRightTextAlignEnum = HtmlTextAlignEnum.Left)
        {

            _logger?.LogTrace("AddLineWithImage variables: urlImage -> {urlImage},leftLine -> {leftLine}, rightLine -> {rightLine}, maxWidth -> {maxWidth}, fontBoldLeft -> {fontBoldLeft}, fontBoldRight -> {fontBoldRight}, colorLinea -> {colorLinea}," +
                              " htmlLeftTextAlignEnum -> {htmlLeftTextAlignEnum},htmlCenterTextAlignEnum -> {htmlCenterTextAlignEnum}, htmlRightTextAlignEnum -> {htmlRightTextAlignEnum}",
                urlImage, leftLine, rightLine, maxWidth, fontBoldLeft, fontBoldRight, colorLinea, htmlLeftTextAlignEnum, htmlCenterTextAlignEnum, htmlRightTextAlignEnum);
            _lineWithImagenAndTwoColumns.Add((_lineCount++, urlImage, leftLine, rightLine, maxWidth, fontBoldLeft, fontBoldRight, colorLinea, htmlLeftTextAlignEnum, htmlCenterTextAlignEnum, htmlRightTextAlignEnum));
            return this;
        }

        public IMailMaker AddLine(IEnumerable<string> leftLines, IEnumerable<string> rightLines, bool fontBoldLeft = false, bool fontBoldRight = false, Color? colorLinea = null)
        {
            var left = leftLines.ToList();
            var right = rightLines.ToList();
            _logger?.LogTrace("AddLine variables: leftLines -> {leftLines}, rightLines -> {rightLines}, fontBoldLeft -> {fontBoldLeft}, fontBoldRight -> {fontBoldRight}, colorLinea -> {colorLinea}",
                string.Join(", ", left),
                string.Join(", ", right),
                fontBoldLeft,
                fontBoldRight, colorLinea);
            _multipleLinesWithColumns.Add((_lineCount++, left, right, fontBoldLeft, fontBoldRight, colorLinea));
            return this;

        }
        public string GetHtml()
        {
            var template = new StringBuilder();

            template.Append(TemplateHtmlHead
                .Replace(TagCol1Percentage, _mailConfigurator.PercentageColumn.ToString())
                .Replace(TagCol2Percentage, (100 - _mailConfigurator.PercentageColumn).ToString())
                .Replace(TagColLeftPercentage, _mailConfigurator.PercentageLeftColumn.ToString())
                .Replace(TagColCenterPercentage, _mailConfigurator.PercentageCenterColumn.ToString())
                .Replace(TagColRightPercentage, (100 - (_mailConfigurator.PercentageLeftColumn + _mailConfigurator.PercentageCenterColumn)).ToString())
                .Replace(TagHeaderCol1Px, _mailConfigurator.HeaderImageWidth.ToString())
                .Replace(TagHeaderCol2Px, (_mailConfigurator.CorreoWidth - _mailConfigurator.HeaderImageWidth).ToString())
                .Replace(TagCorreoWidth, _mailConfigurator.CorreoWidth.ToString())

                .Replace(TagLinkColor, _mailConfigurator.LinkColorStyle)
                .Replace(TagVisitedColor, _mailConfigurator.VisitedColorStyle)
                .Replace(TagHoverColor, _mailConfigurator.HoverColorStyle)
                .Replace(TagActiveColor, _mailConfigurator.ActiveColorStyle)
                .Replace(TagLinkDecorationLine, _mailConfigurator.LinkTextDecorationLine.Print())
                .Replace(TagVisitedDecorationLine, _mailConfigurator.VisitedTextDecorationLine.Print())
                .Replace(TagHoverDecorationLine, _mailConfigurator.HoverTextDecorationLine.Print())
                .Replace(TagActiveDecorationLine, _mailConfigurator.ActiveTextDecorationLine.Print())
                .Replace(TagLinkDecorationStyle, _mailConfigurator.LinkTextDecorationStyle.Print())
                .Replace(TagVisitedDecorationStyle, _mailConfigurator.VisitedTextDecorationStyle.Print())
                .Replace(TagHoverDecorationStyle, _mailConfigurator.HoverTextDecorationStyle.Print())
                .Replace(TagActiveDecorationStyle, _mailConfigurator.ActiveTextDecorationStyle.Print())
            );
            template.Append(TemplateHtmlConfigureHeadEmail
                .Replace(TagImage, _mailConfigurator.PathPicture)
                .Replace(TagTitle, _titleText)
                .Replace(TagTextAlignTitle, _htmlTextAlignEnum.Print())
                .Replace(TagHeaderSize, _htmlHeaderEnum.Print())
                .Replace(TagHeaderLineHeight, _htmlHeaderEnum.PrintLineHeight())
                .Replace(TagPaddingTdTitle, _htmlHeaderEnum.PrintPaddingTdTitle())
                .Replace(TagPaddingTopTitulo, (_mailConfigurator.HeaderImageHeight - 65).ToString())
            );

            template.Append(TemplateHtmlSeparatorLine);

            var numberLine = 0;

            for (var currentLine = 0; currentLine < _lineCount; currentLine++)
            {
                var lineNumber = currentLine;
                if (_line.SingleOrDefault(x => x.lineNumber == lineNumber) is { } line
                    && !string.IsNullOrWhiteSpace(line.text))
                {
                    if (!line.isTitle)
                    {
                        numberLine++;
                    }

                    template.Append(TemplateHtmlAddLine
                        .Replace(TagLine, GetTemplateSimpleLine(line.text, line.fontBold, line.isLink, line.isLinkedIt, line.linkNumber))
                        .Replace(TagTabulation, line.htmlTabulationEnum.Print())
                        .Replace(TagTextAlign, line.htmlTextAlignEnum.Print())
                        .Replace(TagColorLine, GetColorLine(line.colorLinea, line.isTitle, numberLine))
                    );
                }
                if (_lineWithImagen.SingleOrDefault(x => x.lineNumber == lineNumber) is { } lineWithImagen
                    && !string.IsNullOrWhiteSpace(lineWithImagen.urlImage) && !string.IsNullOrWhiteSpace(lineWithImagen.rightLine))
                {
                    numberLine++;
                    template.Append(TemplateHtmlAddLineTwoColumns
                        .Replace(TagLeftColumn, GetTemplateImage(lineWithImagen.urlImage, lineWithImagen.maxWidth))
                        .Replace(TagRightColumn, GetTemplateLine(lineWithImagen.fontBoldRight, lineWithImagen.rightLine))
                        .Replace(TagTdPadding, GetPadding(1, 1))
                        .Replace(TagColorLine, GetColorLine(lineWithImagen.colorLine, false, numberLine))
                    );
                }
                if (_lineWithImagenAndTwoColumns.SingleOrDefault(x => x.lineNumber == lineNumber) is { } lineWithImagenAndTwoColumns
                    && !string.IsNullOrWhiteSpace(lineWithImagenAndTwoColumns.urlImage) && !string.IsNullOrWhiteSpace(lineWithImagenAndTwoColumns.leftLine)
                    && !string.IsNullOrWhiteSpace(lineWithImagenAndTwoColumns.rightLine))
                {
                    numberLine++;
                    template.Append(TemplateHtmlAddLineThreeColumns
                        .Replace(TagLeftColumn, GetTemplateImage(lineWithImagenAndTwoColumns.urlImage, lineWithImagenAndTwoColumns.maxWidth))
                        .Replace(TagCenterColumn, GetTemplateLine(lineWithImagenAndTwoColumns.fontBoldLeft, lineWithImagenAndTwoColumns.leftLine))
                        .Replace(TagRightColumn, GetTemplateLine(lineWithImagenAndTwoColumns.fontBoldRight, lineWithImagenAndTwoColumns.rightLine))
                        .Replace(TagTextAlignLeftColumn, lineWithImagenAndTwoColumns.htmlLeftTextAlignEnum.Print())
                        .Replace(TagTextAlignCenterColumn, lineWithImagenAndTwoColumns.htmlCenterTextAlignEnum.Print())
                        .Replace(TagTextAlignRightColumn, lineWithImagenAndTwoColumns.htmlRightTextAlignEnum.Print())
                        .Replace(TagTdPadding, GetPadding(1, 1))
                        .Replace(TagColorLine, GetColorLine(lineWithImagenAndTwoColumns.colorLine, false, numberLine))
                    );
                }
                if (_lineWithColumns.SingleOrDefault(x => x.lineNumber == lineNumber) is { } lineWithColumns
                    && !string.IsNullOrWhiteSpace(lineWithColumns.leftLine) && !string.IsNullOrWhiteSpace(lineWithColumns.rightLine))
                {
                    numberLine++;
                    template.Append(TemplateHtmlAddLineTwoColumns
                        .Replace(TagLeftColumn, GetTemplateLine(lineWithColumns.fontBoldLeft, lineWithColumns.leftLine, lineWithColumns.isLinkLeft, lineWithColumns.linkNumber))
                        .Replace(TagRightColumn, GetTemplateLine(lineWithColumns.fontBoldRight, lineWithColumns.rightLine))
                        .Replace(TagTdPadding, GetPadding(1, 1))
                        .Replace(TagColorLine, GetColorLine(lineWithColumns.colorLine, false, numberLine))
                    );
                }
                if (_multipleLinesWithColumns.SingleOrDefault(x => x.lineNumber == lineNumber)
                        is { leftLines: not null, rightLines: not null } multipleLinesWithColumns
                    && multipleLinesWithColumns.leftLines.Count() == multipleLinesWithColumns.rightLines.Count())
                {
                    numberLine++;
                    for (var i = 0; i < multipleLinesWithColumns.leftLines.Count(); i++)
                    {
                        var leftLine = GetTemplateLine(multipleLinesWithColumns.fontBoldLeft, multipleLinesWithColumns.leftLines.ElementAt(i));
                        var rightLine = GetTemplateLine(multipleLinesWithColumns.fontBoldRight, multipleLinesWithColumns.rightLines.ElementAt(i));

                        template.Append(TemplateHtmlAddLineTwoColumns
                            .Replace(TagLeftColumn, leftLine)
                            .Replace(TagRightColumn, rightLine)
                            .Replace(TagTdPadding, GetPadding(i + 1, multipleLinesWithColumns.leftLines.Count()))
                            .Replace(TagColorLine, GetColorLine(multipleLinesWithColumns.colorLine, false, numberLine))
                        );
                    }
                }
            }
            template.Append(TemplateHtmlEnd);
            template
                .Replace(TagFontFamily, _mailConfigurator.FontFamily)
                .Replace(TagFontSize, _mailConfigurator.FontSize.ToString())
                ;
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
            _logger?.LogTrace("Dispose()");
        }
    }
}
