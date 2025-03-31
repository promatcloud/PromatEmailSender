using Promat.EmailSender.MailMaker.Enums;
using System;

namespace Promat.EmailSender.MailMaker;

internal static class ExtensionMethods
{
    internal static string Print(this HtmlHeaderEnum enumValue) => enumValue switch
    {
        HtmlHeaderEnum.H1 => "h1",
        HtmlHeaderEnum.H2 => "h2",
        HtmlHeaderEnum.H3 => "h3",
        HtmlHeaderEnum.H4 => "h4",
        HtmlHeaderEnum.H5 => "h5",
        HtmlHeaderEnum.H6 => "h6",
        _ => throw new ArgumentOutOfRangeException(nameof(enumValue), enumValue, null)
    };
    internal static string PrintLineHeight(this HtmlHeaderEnum enumValue) => enumValue switch
    {
        HtmlHeaderEnum.H1 => "161",
        HtmlHeaderEnum.H2 => "229",
        HtmlHeaderEnum.H3 => "300",
        HtmlHeaderEnum.H4 => "378",
        HtmlHeaderEnum.H5 => "560",
        HtmlHeaderEnum.H6 => "695",
        _ => throw new ArgumentOutOfRangeException(nameof(enumValue), enumValue, null)
    };
    internal static string PrintPaddingTdTitle(this HtmlHeaderEnum enumValue) => enumValue switch
    {
        HtmlHeaderEnum.H1 => "padding:10px;",
        HtmlHeaderEnum.H2 => "padding:9px;",
        HtmlHeaderEnum.H3 => "padding:8px;",
        HtmlHeaderEnum.H4 => "padding:6px;",
        HtmlHeaderEnum.H5 => "padding:0px;",
        HtmlHeaderEnum.H6 => "padding:0px;",
        _ => throw new ArgumentOutOfRangeException(nameof(enumValue), enumValue, null)
    };
    internal static string Print(this HtmlTextAlignEnum enumValue) => enumValue switch
    {
        HtmlTextAlignEnum.Left => "left",
        HtmlTextAlignEnum.Center => "center",
        HtmlTextAlignEnum.Right => "right",
        _ => throw new ArgumentOutOfRangeException(nameof(enumValue), enumValue, null)
    };
    internal static string Print(this HtmlDecorationLineEnum decoracionLineValue) => decoracionLineValue switch
    {
        HtmlDecorationLineEnum.None => "none",
        HtmlDecorationLineEnum.Underline => "uderline",
        HtmlDecorationLineEnum.Overline => "overline",
        HtmlDecorationLineEnum.LineThrough => "line-through",
        _ => throw new ArgumentOutOfRangeException(nameof(decoracionLineValue), decoracionLineValue, null)
    };
    internal static string Print(this HtmlDecorationStyleEnum decoracionStyleValue) => decoracionStyleValue switch
    {
        HtmlDecorationStyleEnum.None => "none",
        HtmlDecorationStyleEnum.Solid => "solid",
        HtmlDecorationStyleEnum.LineDouble => "double",
        HtmlDecorationStyleEnum.Dotted => "dotted",
        HtmlDecorationStyleEnum.Dashed => "dashed",
        HtmlDecorationStyleEnum.Wavy => "wavy",
        _ => throw new ArgumentOutOfRangeException(nameof(decoracionStyleValue), decoracionStyleValue, null)
    };
    internal static string Print(this HtmlTabulationEnum tabulationEnumValue) => tabulationEnumValue switch
    {
        HtmlTabulationEnum.Zero => "",
        HtmlTabulationEnum.One => "tabulation1",
        HtmlTabulationEnum.Two => "tabulation2",
        HtmlTabulationEnum.Three => "tabulation3",
        HtmlTabulationEnum.Four => "tabulation4",
        HtmlTabulationEnum.Five => "tabulation5",
        _ => throw new ArgumentOutOfRangeException(nameof(tabulationEnumValue), tabulationEnumValue, null)
    };
    internal static string Print(this HtmlGenericFamilyEnum enumValue) => enumValue switch
    {
        HtmlGenericFamilyEnum.SansSerif => "sans-serif",
        HtmlGenericFamilyEnum.Serif => "serif",
        HtmlGenericFamilyEnum.Monospace => "monospace",
        HtmlGenericFamilyEnum.Cursive => "cursive",
        HtmlGenericFamilyEnum.Fantasy => "fantasy",
        _ => throw new ArgumentOutOfRangeException(nameof(enumValue), enumValue, null)
    };
    internal static string Print(this HtmlFontFamilyEnum enumValue) => enumValue switch
    {
        // Fuentes Sans-Serif
        HtmlFontFamilyEnum.Arial => "arial",
        HtmlFontFamilyEnum.Verdana => "verdana",
        HtmlFontFamilyEnum.Helvetica => "helvetica",
        HtmlFontFamilyEnum.Geneva => "geneva",
        HtmlFontFamilyEnum.Tahoma => "tahoma",
        HtmlFontFamilyEnum.TrebuchetMs => "\"Trebuchet MS\"",
        HtmlFontFamilyEnum.OpenSans => "\"Open Sans\"",
        HtmlFontFamilyEnum.LiberationSans => "\"Liberation Sans\"",
        HtmlFontFamilyEnum.Impact => "impact",
        // Fuentes Serif
        HtmlFontFamilyEnum.Georgia => "georgia",
        HtmlFontFamilyEnum.Times => "times",
        HtmlFontFamilyEnum.TimesNewRoman => "\"times new roman\"",
        HtmlFontFamilyEnum.Bodoni => "\"bodoni mt\"",
        HtmlFontFamilyEnum.Garamond => "garamond",
        HtmlFontFamilyEnum.Palatino => "palatino",
        HtmlFontFamilyEnum.ITCClearface => "\"ITC Clearface\"",
        HtmlFontFamilyEnum.Plantin => "plantin",
        HtmlFontFamilyEnum.FreightText => "\"Freight Text\"",
        HtmlFontFamilyEnum.Didot => "didot",
        HtmlFontFamilyEnum.AmericanTypewriter => "\"american typewriter\"",
        // Fuentes Monospace
        HtmlFontFamilyEnum.Courier => "courier",
        HtmlFontFamilyEnum.MSCourierNew => "\"MS Courier New\"",
        HtmlFontFamilyEnum.Monaco => "monaco",
        HtmlFontFamilyEnum.LucidaConsole => "\"Lucida Console\"",
        HtmlFontFamilyEnum.AndaleMono => "\"Andalé Mono\"",
        HtmlFontFamilyEnum.Menlo => "Menlo",
        HtmlFontFamilyEnum.Consolas => "Consolas",
        // Fuentes Cursive
        HtmlFontFamilyEnum.ComicSans => "\"Comic Sans\"",
        HtmlFontFamilyEnum.ComicSansMS => "\"Comic Sans MS\"",
        HtmlFontFamilyEnum.AppleChancery => "\"Apple Chancery\"",
        HtmlFontFamilyEnum.ZapfChancery => "\"Zapf Chancery\"",
        HtmlFontFamilyEnum.BradleyHand => "\"Bradley Hand\"",
        HtmlFontFamilyEnum.BrushScriptMT => "\"Brush Script MT\"",
        HtmlFontFamilyEnum.BrushScriptStd => "\"Brush Script Std\"",
        HtmlFontFamilyEnum.SnellRoundhan => "\"Snell Roundhan\"",
        HtmlFontFamilyEnum.URWChancery => "\"URW Chancery\"",
        HtmlFontFamilyEnum.Coronetscript => "\"Coronet script\"",
        HtmlFontFamilyEnum.Florence => "Florenc",
        HtmlFontFamilyEnum.Parkavenue => "Parkavenue",
        // Fuentes Fantasy
        HtmlFontFamilyEnum.ImpactFANTASY => "Impact",
        HtmlFontFamilyEnum.Brushstroke => "Brushstroke",
        HtmlFontFamilyEnum.Luminari => "Luminari",
        HtmlFontFamilyEnum.Chalkduster => "Chalkduster",
        HtmlFontFamilyEnum.JazzLET => "\"Jazz LET\"",
        HtmlFontFamilyEnum.Blippo => "Blippo",
        HtmlFontFamilyEnum.StencilStd => "\"Stencil Std\"",
        HtmlFontFamilyEnum.MarkerFelt => "\"Marker Felt\"",
        HtmlFontFamilyEnum.Trattatello => "Trattatello",
        HtmlFontFamilyEnum.Arnoldboecklin => "Arnoldboecklin",
        HtmlFontFamilyEnum.Oldtown => "Oldtown",
        HtmlFontFamilyEnum.Copperplate => "Copperplate,",
        HtmlFontFamilyEnum.papyrus => "papyrus",

        _ => throw new ArgumentOutOfRangeException(nameof(enumValue), enumValue, null)
    };
}