using System;
using Promat.EmailSender.MailTemplate.Enums;

namespace Promat.EmailSender.MailTemplate
{
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
        internal static string Print(this HtmlTextAlignEnum enumValue) => enumValue switch
        {
            HtmlTextAlignEnum.Left => "left",
            HtmlTextAlignEnum.Center => "center",
            HtmlTextAlignEnum.Right => "right",
            _ => throw new ArgumentOutOfRangeException(nameof(enumValue), enumValue, null)
        };
    }
}