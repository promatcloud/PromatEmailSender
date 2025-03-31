using Promat.EmailSender.MailMaker.Enums;
using Promat.EmailSender.MailMaker.Interfaces;
using System.Drawing;

namespace Promat.EmailSender.MailMaker;

public class MailConfigurator : IMailConfigurator
{
    private IMailMaker _mailMaker;

    public string PathPicture { get; private set; } = "https://raw.githubusercontent.com/promatcloud/Branding/master/icons/org/promat.512.png";
    public string BackgroundColorOodLine { get; private set; } = "#ffffff";
    public string BackgroundColorEvenLine { get; private set; } = "#E6E6E6";
    public string BackgroundColorTitle { get; private set; } = "#FCEEE2";
    public bool IsToggleColorInLines { get; private set; } = true;
    public int PercentageColumn { get; private set; } = 20;
    public int PercentageLeftColumn { get; private set; } = 20;
    public int PercentageCenterColumn { get; private set; } = 40;
    public int HeaderImageWidth { get; private set; } = 280;
    public int HeaderImageHeight { get; private set; } = 280;
    public int CorreoWidth { get; private set; } = 840;
    public string LinkColorStyle { get; private set; } = "#000000";
    public string VisitedColorStyle { get; private set; } = "#000000";
    public string HoverColorStyle { get; private set; } = "#000000";
    public string ActiveColorStyle { get; private set; } = "#000000";
    public HtmlDecorationLineEnum LinkTextDecorationLine { get; private set; } = HtmlDecorationLineEnum.None;
    public HtmlDecorationLineEnum VisitedTextDecorationLine { get; private set; } = HtmlDecorationLineEnum.None;
    public HtmlDecorationLineEnum HoverTextDecorationLine { get; private set; } = HtmlDecorationLineEnum.None;
    public HtmlDecorationLineEnum ActiveTextDecorationLine { get; private set; } = HtmlDecorationLineEnum.None;
    public HtmlDecorationStyleEnum LinkTextDecorationStyle { get; private set; } = HtmlDecorationStyleEnum.None;
    public HtmlDecorationStyleEnum VisitedTextDecorationStyle { get; private set; } = HtmlDecorationStyleEnum.None;
    public HtmlDecorationStyleEnum HoverTextDecorationStyle { get; private set; } = HtmlDecorationStyleEnum.None;
    public HtmlDecorationStyleEnum ActiveTextDecorationStyle { get; private set; } = HtmlDecorationStyleEnum.None;
    public string FontFamily { get; private set; } = ConcatFontFamily(HtmlFontFamilyEnum.Arial, HtmlFontFamilyEnum.Helvetica, HtmlGenericFamilyEnum.SansSerif);
    public int FontSize { get; private set; } = 14;

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
    public IMailConfigurator BackgroundTitle(Color color)
    {
        BackgroundColorTitle = ToHex(color);
        return this;
    }
    public IMailConfigurator BackgroundOddLine(string cssColor)
    {
        BackgroundColorOodLine = cssColor;
        return this;
    }
    public IMailConfigurator BackgroundOddLine(Color color)
    {
        BackgroundColorOodLine = ToHex(color);
        return this;
    }
    public IMailConfigurator BackgroundEvenLine(string cssColor)
    {
        BackgroundColorEvenLine = cssColor;
        return this;
    }
    public IMailConfigurator BackgroundEvenLine(Color color)
    {
        BackgroundColorEvenLine = ToHex(color);
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
    public IMailConfigurator SetPercentageTwoColumn(int percentageColumn)
    {
        PercentageColumn = percentageColumn switch
        {
            < 20 => 20,
            > 75 => 75,
            _ => percentageColumn
        };
        return this;
    }
    public IMailConfigurator SetPercentageThreeColumn(int percentageLeftColumn, int percentageRightColumn)
    {
        PercentageLeftColumn = percentageLeftColumn switch
        {
            < 20 => 20,
            > 60 => 60,
            _ => percentageLeftColumn
        };
        PercentageCenterColumn = percentageRightColumn switch
        {
            < 20 => 20,
            > 60 => 60,
            _ => percentageRightColumn
        };
        if (PercentageCenterColumn + PercentageLeftColumn > 80)
        {
            PercentageLeftColumn = 20;
            PercentageCenterColumn = 40;

        }
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
    public IMailConfigurator SetImageSize(int size) => SetImageSize(size, size);

    public IMailConfigurator SetLinksColorsAndTextDecoration(
        Color linkColorStyle, Color visitedColorStyle,
        Color hoverColorStyle, Color activeColorStyle,
        HtmlDecorationLineEnum linkTextDecorationLine = HtmlDecorationLineEnum.None,
        HtmlDecorationLineEnum visitedTextDecorationLine = HtmlDecorationLineEnum.None,
        HtmlDecorationLineEnum hoverTextDecorationLine = HtmlDecorationLineEnum.None,
        HtmlDecorationLineEnum activeTextDecorationLine = HtmlDecorationLineEnum.None,
        HtmlDecorationStyleEnum linkTextDecorationStyle = HtmlDecorationStyleEnum.None,
        HtmlDecorationStyleEnum visitedTextDecorationStyle = HtmlDecorationStyleEnum.None,
        HtmlDecorationStyleEnum hoverTextDecorationStyle = HtmlDecorationStyleEnum.None,
        HtmlDecorationStyleEnum activeTextDecorationStyle = HtmlDecorationStyleEnum.None)
    {
        LinkColorStyle = ToHex(linkColorStyle);
        VisitedColorStyle = ToHex(visitedColorStyle);
        HoverColorStyle = ToHex(hoverColorStyle);
        ActiveColorStyle = ToHex(activeColorStyle);
        LinkTextDecorationLine = linkTextDecorationLine;
        VisitedTextDecorationLine = visitedTextDecorationLine;
        HoverTextDecorationLine = hoverTextDecorationLine;
        ActiveTextDecorationLine = activeTextDecorationLine;
        LinkTextDecorationStyle = linkTextDecorationStyle;
        VisitedTextDecorationStyle = visitedTextDecorationStyle;
        HoverTextDecorationStyle = hoverTextDecorationStyle;
        ActiveTextDecorationStyle = activeTextDecorationStyle;

        return this;
    }
    public IMailConfigurator SetFontGenericFamilyAndFontSize(
        HtmlFontFamilyEnum fontFamilyOne = HtmlFontFamilyEnum.Arial,
        HtmlFontFamilyEnum fontFamilyTwo = HtmlFontFamilyEnum.Helvetica,
        HtmlGenericFamilyEnum genericFamily = HtmlGenericFamilyEnum.SansSerif, int fontSize = 14)
    {
        FontFamily = ConcatFontFamily(fontFamilyOne, fontFamilyTwo, genericFamily);
        FontSize = fontSize;

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
    private static string ConcatFontFamily(HtmlFontFamilyEnum fontFamilyOne, HtmlFontFamilyEnum fontFamilyTwo, HtmlGenericFamilyEnum genericFamily)
    {
        return $"{fontFamilyOne.Print()},{fontFamilyTwo.Print()},{genericFamily.Print()}";
    }
}