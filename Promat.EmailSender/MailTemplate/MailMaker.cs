using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Promat.EmailSender.MailTemplate.Enums;
using Promat.EmailSender.MailTemplate.Interfaces;

namespace Promat.EmailSender.MailTemplate
{
    public partial class MailMaker : IMailMaker
    {
        private readonly MailConfigurator _mailConfigurator;

        private string _titleText;
        private HtmlHeaderEnum _htmlHeaderEnum;
        private List<(int lineNumber, string text, bool isTitle, bool fontBold, HtmlTextAlignEnum htmlTextAlignEnum)> _lines = new();
        private List<(int lineNumber, string urlImage, string rightLine, int maxWidth, bool fontBoldRight)> _LineWithImagen = new();
        private List<(int lineNumber, string leftLine, string rightLine, bool fontBoldLeft, bool fontBoldRight)> _LinesWithColumns = new();
        private List<(int lineNumber, IEnumerable<string> leftLines, IEnumerable<string> rightLines, bool fontBoldLeft, bool fontBoldRight)> _multipleLinesWithColumns = new();
        private int _lineCount = 0;

        private MailMaker(MailConfigurator mailConfigurator)
        {
            _mailConfigurator = mailConfigurator;
        }
        
        public static MailMaker New()
        {
            var instance = new MailMaker(new MailConfigurator());
            instance._mailConfigurator.SetMailMaker(instance);
            return instance;
        }
        public IMailConfigurator ConfigureMail()
        {
            return _mailConfigurator;
        }
        public IMailMaker TitleHeader(string title, HtmlHeaderEnum headerEnum = HtmlHeaderEnum.H3)
        {
            _titleText = title;
            _htmlHeaderEnum = headerEnum;
            return this;
        }
        public IMailMaker AddLine(string lineText, bool isTitle = false, bool fontBold = false,
            HtmlTextAlignEnum htmlTextAlignEnum = HtmlTextAlignEnum.Left)
        {
            _lines.Add((_lineCount++, lineText, isTitle, fontBold, htmlTextAlignEnum));
            return this;
        }
        public IMailMaker AddLine(string leftLine, string rightLine,
            bool fontBoldLeft = false, bool fontBoldRight = false)
        {
            _LinesWithColumns.Add((_lineCount++, leftLine, rightLine, fontBoldLeft, fontBoldRight));
            return this;
        }
        public IMailMaker AddLineWithImage(string urlImage, string rightLine, int maxWidth = 20, bool fontBoldRight = false)
        {
            _LineWithImagen.Add((_lineCount++, urlImage, rightLine, maxWidth, fontBoldRight));
            return this;
        }
        public IMailMaker AddLine(IEnumerable<string> leftLines, IEnumerable<string> rightLines,
            bool fontBoldLeft = false, bool fontBoldRight = false)
        {
            _multipleLinesWithColumns.Add((_lineCount++, leftLines, rightLines, fontBoldLeft, fontBoldRight));
            return this;

        }
        public string GetHtml()
        {
            StringBuilder _template = new StringBuilder();

            _template.Append(TemplateHtmlHead);
            _template.Append(TemplateHtmlConfigureHeadEmail
                .Replace(TagImage, _mailConfigurator.PathPicture)
                .Replace(TagTitle, _titleText)
                .Replace(TagHeaderSize, _htmlHeaderEnum.ToString())
            );
            _template.Append(TemplateHtmlSeparatorLine);

            var numberLine = 0;

            for (int lineNumber = 0; lineNumber < _lineCount; lineNumber++)
            {
                if (_lines.SingleOrDefault(x => x.lineNumber == lineNumber) is { } line)
                {
                    if (!string.IsNullOrWhiteSpace(line.text))
                    {
                        if (!line.isTitle)
                        {
                            numberLine++;
                        }

                        _template.Append(TemplateHtmlAddLine
                            .Replace(TagLine, line.fontBold ? "<strong>" + line.text + "</strong>" : line.text)
                            .Replace(TagTextAlign, line.htmlTextAlignEnum.Print())
                            .Replace(TagColorLine,
                                line.isTitle
                                    ? _mailConfigurator.BackgroundColorTitulo
                                    : (numberLine % 2 == 0
                                        ? _mailConfigurator.BackgroundColorLinePair
                                        : _mailConfigurator.BackgroundColorLineOod))
                        );
                    }
                }
                if (_LineWithImagen.SingleOrDefault(x => x.lineNumber == lineNumber) is { } lineWithImagen)
                {
                    if (!string.IsNullOrWhiteSpace(lineWithImagen.urlImage )&& !string.IsNullOrWhiteSpace(lineWithImagen.rightLine))
                    {
                        numberLine++;
                        _template.Append(TemplateHtmlAddLineTwoColumns
                            .Replace(TagLeftColumn,GetTagImage(lineWithImagen.urlImage, lineWithImagen.maxWidth))
                            .Replace(TagRightColumn,GetTagLine(lineWithImagen.fontBoldRight, lineWithImagen.rightLine))
                            .Replace(TagColorLine,
                                _mailConfigurator.IsToggleColorInLines ?
                                    (numberLine % 2 == 0
                                        ? _mailConfigurator.BackgroundColorLinePair
                                        : _mailConfigurator.BackgroundColorLineOod)
                                    : _mailConfigurator.BackgroundColorLineOod)
                            );
                    }
                }
                if (_LinesWithColumns.SingleOrDefault(x => x.lineNumber == lineNumber) is { } lineWithColumns)
                {
                    if (!string.IsNullOrWhiteSpace(lineWithColumns.leftLine) && !string.IsNullOrWhiteSpace(lineWithColumns.rightLine))
                    {
                        numberLine++;
                        _template.Append(TemplateHtmlAddLineTwoColumns
                            .Replace(TagLeftColumn, GetTagLine(lineWithColumns.fontBoldLeft, lineWithColumns.leftLine))
                            .Replace(TagRightColumn, GetTagLine(lineWithColumns.fontBoldRight, lineWithColumns.rightLine))
                            .Replace(TagColorLine,
                                _mailConfigurator.IsToggleColorInLines ?
                                (numberLine % 2 == 0
                                    ? _mailConfigurator.BackgroundColorLinePair
                                    : _mailConfigurator.BackgroundColorLineOod)
                                : _mailConfigurator.BackgroundColorLineOod)
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
                        _template.Append(TemplateHtmlAddLineTwoColumns
                            .Replace(TagLeftColumn, tagLeftColumn)
                            .Replace(TagRightColumn, tagRightColumn)
                            .Replace(TagColorLine,
                                _mailConfigurator.IsToggleColorInLines ?
                                    (numberLine % 2 == 0
                                        ? _mailConfigurator.BackgroundColorLinePair
                                        : _mailConfigurator.BackgroundColorLineOod)
                                    : _mailConfigurator.BackgroundColorLineOod
                        ));
                    }
                }
            }
            _template.Append(TemplateHtmlEnd);
            return _template.ToString();
        }
        public void SendMailAsync(string to, string subject, IEnumerable<string> ccs = null)
        {
            //// No enviamos correos de expiración de puestos ni de problemas de comunicaciones si es un host levantado por los test.
            //if (DInjector.Contains<UnitTestKeysConfiguration>())
            //{
            //    return;
            //}

            SmtpSender smtpSender = null;

            try
            {
                var mailMessage = new MailMessage
                {
                    From = new MailAddress("Enrique Salinas", "Notificaciones ProMat"),
                    IsBodyHtml = true,
                    Body = GetHtml(),
                    Subject = subject
                };
                mailMessage.To.Add(to);
                if (ccs != null)
                {
                    // TODO
                }
                
                // KIKE revisar la configuración smtp
                //smtpSender = Log.NewSmtpSender();
                //await smtpSender.SendEmailAsync(mailMessage);
            }
            catch (Exception )
            {
                //await Log.DefaultErrorAsync(e, logActionMail: LogActionMailEnum.Nothing);
            }
            finally
            {
                smtpSender?.Dispose();
            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
