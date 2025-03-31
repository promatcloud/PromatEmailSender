using System.Drawing;

namespace Promat.EmailSender.MailMaker;

public partial class MailMaker
{
    // Plantilla hmtl donde están las etiquetas de apertura necesarias para la tabla asi como sus estilos
    private const string TemplateHtmlHead =
        @"<!DOCTYPE html PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"" ""http://www.w3.org/TR/REC-html40/loose.dtd"">
<html xmlns=""http://www.w3.org/1999/xhtml"" xmlns:v=""urn:schemas-microsoft-com:vml""
  xmlns:o=""urn:schemas-microsoft-com:office:office"">

<head>
  <!--[if gte mso 9]>
  <xml>
    <o:OfficeDocumentSettings>
      <o:AllowPNG/>
      <o:PixelsPerInch>96</o:PixelsPerInch>
    </o:OfficeDocumentSettings>
  </xml>
  <![endif]-->
  <meta http-equiv=""Content-Type"" content=""text/html; charset=UTF-8"">
  <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
  <meta name=""x-apple-disable-message-reformatting"">
  <!--[if !mso]><!-->
  <meta http-equiv=""X-UA-Compatible"" content=""IE=edge""><!--<![endif]-->
  <title></title>

  <style type=""text/css"">

    html.* {
      color: #000000;
      font-family: [FontFamily];
      font-size: [FontSize]px;
      line-height: inherit;
    }
    .h1 {
    
    }
    .u-row {
      width: [CorreoWidth]px !important;
    }

    .u-row .u-col {
      vertical-align: top;
      width: 100%;
    }

    .u-row .u-col.header-col1{
      width: [HeaderCol1px]px;
    }

    .u-row .u-col.header-col2{
      width: [HeaderCol2px]px;
    }

    .u-row .u-col.col1{
      width: [Col1Percentage]%;
    }

    .u-row .u-col.col2{
      width: [Col2Percentage]%;
    } 

    .u-row .u-col.colleft{
      width: [ColLeftPercentage]%;
    }

    .u-row .u-col.colcenter{
      width: [ColCenterPercentage]%;
    }   

    .u-row .u-col.colright{
      width: [ColRightPercentage]%;
    }  
    .tabulation1{
	    margin-left: 15px;
    }
    .tabulation2{
	    margin-left: 30px;
    }
    .tabulation3{
	    margin-left: 45px;
    }
    .tabulation4{
	    margin-left: 60px;
    }
    .tabulation5{
	    margin-left: 75px;
    }
    a:link {
      color: [LinkColor];
      text-decoration-line: [LinkDecorationLine];
      text-decoration-style: [LinkDecorationStyle];
      text-decoration-thickness: 2px;  
      background-color: transparent;
    }
    a:visited {
      color: [VisitedColor];
      text-decoration-line: [VisitedDecorationLine];
      text-decoration-style: [VisitedDecorationStyle];
      text-decoration-thickness: 2px;  
      background-color: transparent;
    }
    a:hover {
      color: [HoverColor];
      text-decoration-line: [HoverDecorationLine];
      text-decoration-style: [HoverDecorationStyle];
      text-decoration-thickness: 2px;  
      background-color: transparent; 
    }
    a:active {
      color: [ActiveColor];
      text-decoration-line: [ActiveDecorationLine];
      text-decoration-style: [ActiveDecorationStyle];
      text-decoration-thickness: 2px;  
      background-color: transparent;
    }

    body {
      margin: 0;
      padding: 0;
    }

    table,
    tr,
    td {
      vertical-align: top;
      border-collapse: collapse;
    }

    p {
      margin: 0;
    }

    .ie-container table,
    .mso-container table {
      table-layout: fixed;
    }

    a[x-apple-data-detectors='true'] {
      color: inherit !important;
      text-decoration: none !important;
    }

    table,
    td {
      color: #000000;
    }
  </style>

</head>

<body class=""clean-body u_body""
  style=""margin: 0;padding: 0;-webkit-text-size-adjust: 100%;background-color: #FFFFFF;color: #000000"">
  <table
    style=""border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;min-width: 320px;Margin: 0 auto;background-color: #FFFFFF;width:100%""
    cellpadding=""0"" cellspacing=""0"">
    <tbody>
      <tr style=""vertical-align: top"">
        <td style=""word-break: break-word;border-collapse: collapse !important;vertical-align: top"">    
        ";

    // Plantilla hmtl donde introducimos la cabecera y sustituimos imagen y título
    private const string TemplateHtmlConfigureHeadEmail = @"	

  <div class=""u-row-container"" style=""padding: 0px;background-color: transparent"">
            <div class=""u-row"" style=""overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: transparent;"">
              <div style=""border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;"">
                <div class=""u-col header-col1"" style=""display: table-cell;vertical-align: top;"">
                  <div style=""height: 100%;width: 100% !important;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;"">                    
                    <div style=""box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;"">

                      <table style=""font-family:[FontFamily];"" role=""presentation"" cellpadding=""0""
                        cellspacing=""0"" width=""100%"" border=""0"">
                        <tbody>
                          <tr>
                            <td
                              style=""overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:[FontFamily];""
                              align=""left"">

                              <table width=""100%"" cellpadding=""0"" cellspacing=""0"" border=""0"">
                                <tbody>
                                  <tr>
                                    <td style=""padding-right: 0px;padding-left: 0px;"" align=""center"">

                                      <img align=""center"" border=""0"" 
										src=""[Image]"" 
										alt="""" title=""""
                                        style=""outline: none;text-decoration: none;-ms-interpolation-mode: bicubic;clear: both;display: inline-block !important;border: none;height: auto;float: none;width: 100%;max-width: 260px;""
                                        width=""260"">

                                    </td>
                                  </tr>
                                </tbody>
                              </table>

                            </td>
                          </tr>
                        </tbody>
                      </table>

                    </div>
                  </div>
                </div>
                <div class=""u-col header-col2"" style=""display: table-cell;vertical-align: top;"">
                  <div style=""height: 100%;width: 100% !important;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;"">
                    <div style=""box-sizing: border-box; height: 100%; padding: [PaddingTopTitle]px 0px 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;"">

                      <table style=""font-family:[FontFamily];"" role=""presentation"" cellpadding=""0"" cellspacing=""0"" width=""100%"" border=""0"">
                        <tbody>
                          <tr>
                            <td
                              style=""overflow-wrap:break-word;word-break:break-word;[PaddingTdTitle]font-family:[FontFamily];""
                              align=""left"">

                              <div style=""font-size: [FontSize]px; line-height: 130%; text-align: [TextAlignTitle]; word-wrap: break-word;"">
                                <[HeaderSize] style=""line-height: [HeaderLineHeight]%; margin: 0"">[Title]</[HeaderSize]>
                              </div>

                            </td>
                          </tr>
                        </tbody>
                      </table>

                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        
        ";

    // Plantilla html para poner una línea separadora
    private const string TemplateHtmlSeparatorLine = @"
          <div class=""u-row-container"" style=""padding: 0px;background-color: transparent"">
            <div class=""u-row"" style="" overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: transparent;"">
              <div style=""border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;"">
                <div class=""u-col u-col-100""
                  style=""max-width: 320px;min-width: 840px;display: table-cell;vertical-align: top;"">
                  <div style=""height: 100%;width: 100% !important;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;"">
                    <div style=""box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;"">
                     
                      <table style=""font-family:[FontFamily];"" role=""presentation"" cellpadding=""0"" cellspacing=""0"" width=""100%"" border=""0"">
                        <tbody>
                          <tr>
                            <td
                              style=""overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:[FontFamily];""
                              align=""left"">

                              <table height=""0px"" align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%""
                                style=""border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 1px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%"">
                                <tbody>
                                  <tr style=""vertical-align: top"">
                                    <td
                                      style=""word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: [FontSize]px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%"">
                                      <span>&nbsp;</span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>

                            </td>
                          </tr>
                        </tbody>
                      </table>

                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>

        ";

    // Plantilla para cada linea de la tabla que queramos componer en el correo
    private const string TemplateHtmlAddLine = @"
         <div class=""u-row-container"" style=""padding: 0px;background-color: transparent"">
            <div class=""u-row"" style="" overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: transparent;"">
              <div style=""border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;"">
                <div class=""u-col"" style=""display: table-cell;vertical-align: top;"">
                  <div
                    style=""background-color:[Color];height: 100%;width: 100% !important;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;"">
                    <div
                      style=""box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;"">                     

                      <table style=""font-family:[FontFamily];"" role=""presentation"" cellpadding=""0"" cellspacing=""0"" width=""100%"" border=""0"">
                        <tbody>
                          <tr>
                            <td style=""overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:[FontFamily];"" align=""left"">
                              <div style=""font-size: [FontSize]px; line-height: 140%; text-align: [TagTextAlign]; word-wrap: break-word;"">

                                [Line]

                              </div>
                            </td>
                          </tr>
                        </tbody>
                      </table>

                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        ";

    // Plantilla para cada linea con dos columnas de la tabla que queramos componer en el correo 
    private const string TemplateHtmlAddLineTwoColumns = @"
          <div class=""u-row-container"" style=""padding: 0px;background-color: transparent"">
            <div class=""u-row"" style="" overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: transparent;"">
              <div style=""border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;"">
                <div class=""u-col col1"" style=""background-color:[Color];display: table-cell;vertical-align: top;"">
                  <div style=""height: 100%;width: 100% !important;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;"">
                    <div style=""box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;"">

                      <table style=""font-family:[FontFamily];"" role=""presentation"" cellpadding=""0""
                        cellspacing=""0"" width=""100%"" border=""0"">
                        <tbody>
                          <tr>
                            <td
                              style=""overflow-wrap:break-word;word-break:break-word;[TdPadding]font-family:[FontFamily];""
                              align=""left"">
                              <div style=""font-size: [FontSize]px; line-height: 140%; text-align: right; word-wrap: break-word;"">
								
								[LeftColumn]
								
                              </div>
                            </td>
                          </tr>
                        </tbody>
                      </table>

                    </div>
                  </div>
                </div>
                <div class=""u-col col2"" style=""background-color:[Color];display: table-cell;vertical-align: top;"">
                  <div style=""height: 100%;width: 100% !important;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;"">                   
                    <div style=""box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;"">
                    
                      <table style=""font-family:[FontFamily];"" role=""presentation"" cellpadding=""0""
                        cellspacing=""0"" width=""100%"" border=""0"">
                        <tbody>
                          <tr>
                            <td
                              style=""overflow-wrap:break-word;word-break:break-word;[TdPadding]font-family:[FontFamily];""
                              align=""left"">
                              <div style=""font-size: [FontSize]px; line-height: 140%; text-align: left; word-wrap: break-word;"">
							  
								[RightColumn]
								
                              </div>
                            </td>
                          </tr>
                        </tbody>
                      </table>

                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        ";

    // Plantilla para cada línea que de tres columnas que queramos componer en el correo
    private const string TemplateHtmlAddLineThreeColumns = @"
		<div class=""u-row-container"" style=""padding: 0px;background-color: transparent"">
            <div class=""u-row"" style="" overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: transparent;"">
              <div style=""border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;"">
                <div class=""u-col colleft"" style=""background-color: #F5F5DC;display: table-cell;vertical-align: top;"">
                  <div style=""height: 100%;width: 100% !important;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;"">
                    <div style=""box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;"">

                      <table style=""font-family:[FontFamily];"" role=""presentation"" cellpadding=""0"" cellspacing=""0"" width=""100%"" border=""0"">
                        <tbody>
                          <tr>
                            <td style=""overflow-wrap:break-word;word-break:break-word;[TdPadding]font-family:[FontFamily];"" align=""left"">
                              <div style=""font-size: [FontSize]px; line-height: 140%; text-align: [TagTextAlignLeftColumn]; word-wrap: break-word;"">
								
								[LeftColumn]
								
                              </div>
                            </td>
                          </tr>
                        </tbody>
                      </table>

                    </div>
                  </div>
                </div>
                
                <div class=""u-col colcenter"" style=""background-color: #F5F5DC;display: table-cell;vertical-align: top;"">
                  <div style=""height: 100%;width: 100% !important;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;"">
                    <div style=""box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;"">

                      <table style=""font-family:[FontFamily];"" role=""presentation"" cellpadding=""0"" cellspacing=""0"" width=""100%"" border=""0"">
                        <tbody>
                          <tr>
                            <td style=""overflow-wrap:break-word;word-break:break-word;[TdPadding]font-family:[FontFamily];"" align=""left"">
                              <div style=""font-size: [FontSize]px; line-height: 140%; text-align: [TagTextAlignCenterColumn]; word-wrap: break-word;"">
							  
								[CenterColumn]
								
                              </div>
                            </td>
                          </tr>
                        </tbody>
                      </table>

                    </div>
                  </div>
                </div><div class=""u-col colright"" style=""background-color: #F5F5DC;display: table-cell;vertical-align: top;"">
                  <div style=""height: 100%;width: 100% !important;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;"">
                    <div style=""box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;"">

                      <table style=""font-family:[FontFamily];"" role=""presentation"" cellpadding=""0"" cellspacing=""0"" width=""100%"" border=""0"">
                        <tbody>
                          <tr>
                            <td style=""overflow-wrap:break-word;word-break:break-word;[TdPadding]font-family:[FontFamily];"" align=""left"">
                              <div style=""font-size: [FontSize]px; line-height: 140%; text-align: [TagTextAlignRightColumn]; word-wrap: break-word;"">
							  
								[RightColumn]
								
                              </div>
                            </td>
                          </tr>
                        </tbody>
                      </table>

                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>

";

    // En el BodyEnd cerramos toda las etiquetas que tenemos abiertas
    private const string TemplateHtmlEnd = @"
          <!--[if (mso)|(IE)]></td></tr></table><![endif]-->
        </td>
      </tr>
    </tbody>
  </table>
  <!--[if mso]></div><![endif]-->
  <!--[if IE]></div><![endif]-->



</body>

</html>
        ";


    // Tag en plantilla TemplateHtmlHead:
    private const string TagFontFamily = "[FontFamily]";
    private const string TagFontSize = "[FontSize]";
    private const string TagCorreoWidth = "[CorreoWidth]";
    private const string TagHeaderCol1Px = "[HeaderCol1px]";
    private const string TagHeaderCol2Px = "[HeaderCol2px]";
    private const string TagCol1Percentage = "[Col1Percentage]";
    private const string TagCol2Percentage = "[Col2Percentage]";
    private const string TagColLeftPercentage = "[ColLeftPercentage]";
    private const string TagColCenterPercentage = "[ColCenterPercentage]";
    private const string TagColRightPercentage = "[ColRightPercentage]";
    private const string TagLinkColor = "[LinkColor]";
    private const string TagVisitedColor = "[VisitedColor]";
    private const string TagHoverColor = "[HoverColor]";
    private const string TagActiveColor = "[ActiveColor]";
    private const string TagLinkDecorationLine = "[LinkDecorationLine]";
    private const string TagVisitedDecorationLine = "[VisitedDecorationLine]";
    private const string TagHoverDecorationLine = "[HoverDecorationLine]";
    private const string TagActiveDecorationLine = "[ActiveDecorationLine]";
    private const string TagLinkDecorationStyle = "[LinkDecorationStyle]";
    private const string TagVisitedDecorationStyle = "[VisitedDecorationStyle]";
    private const string TagHoverDecorationStyle = "[HoverDecorationStyle]";
    private const string TagActiveDecorationStyle = "[ActiveDecorationStyle]";
    // Tag en plantilla TemplateHtmlConfigureHeadEmail:
    private const string TagImage = "[Image]";
    private const string TagPaddingTopTitulo = "[PaddingTopTitle]";
    private const string TagTextAlignTitle = "[TextAlignTitle]";
    private const string TagPaddingTdTitle = "[PaddingTdTitle]";
    private const string TagHeaderSize = "[HeaderSize]";
    private const string TagTitle = "[Title]";
    private const string TagHeaderLineHeight = "[HeaderLineHeight]";
    // Tag en plantilla TemplateHtmlAddLine:
    private const string TagColorLine = "[Color]";
    private const string TagTextAlign = "[TagTextAlign]";
    private const string TagLine = "[Line]";
    // Tag en plantilla TemplateHtmlAddLineTwoColumns:
    // Se reutiliza la tag TagColorLine = "[Color]"
    private const string TagLeftColumn = "[LeftColumn]";
    private const string TagRightColumn = "[RightColumn]";
    private const string TagTdPadding = "[TdPadding]";
    // Tag en plantilla TemplateHtmlAddLineThreeColumns:
    private const string TagTextAlignLeftColumn = "[TagTextAlignLeftColumn]";
    private const string TagTextAlignCenterColumn = "[TagTextAlignCenterColumn]";
    private const string TagTextAlignRightColumn = "[TagTextAlignRightColumn]";
    // Se reutiliza TagLeftColumn = "[LeftColumn]" y TagRightColumn = "[RightColumn]"
    private const string TagCenterColumn = "[CenterColumn]";

    private const string TagTabulation = "[Tabulation]";

    private string GetTemplateLine(bool isBoldText, string textLine, bool isLink = false, int linkNumber = 0)
    {
        var idLink = isLink ? $"section{linkNumber}" : string.Empty;

        return isBoldText ?
            $"<p id=\"{idLink}\"><strong>" + textLine + "</strong></p>" :
            $"<p id=\"{idLink}\">" + textLine + "</p>";
    }
    private string GetTemplateSimpleLine(string textLine, bool isBoldText, bool isLink, bool isLinkedIt, int linkNumber)
    {
        var line = isLinkedIt ? $"<a href=\"#section{linkNumber}\">{textLine}</a>" : textLine;
        var idLink = isLink ? $"section{linkNumber}" : string.Empty;

        return isBoldText
            ? $"<p id=\"{idLink}\" class=\"[Tabulation]\"><strong>{line}</strong></p>"
            : $"<p id=\"{idLink}\" class=\"[Tabulation]\">" + line + "</p>";
    }
    private string GetTemplateImage(string urlPicture, int maxWidth)
    {
        return "<img align=\"center\" border=\"0\" " +
               $"src=\"{urlPicture}\" " +
               "alt=\"\" " +
               "title=\"\" " +
               "style=\"outline: none;text-decoration: none;-ms-interpolation-mode: bicubic;clear: both;display: inline-block !important;" +
               "border: none;height: auto;" +
               "float: none;width: 100%;" +
               $"max-width: {maxWidth.ToString()}px;\" " +
               "width=\"30\">"
            ;
    }
    private string GetPadding(int lineaActual, int lineasTotales)
    {
        if (lineasTotales == 1)
        {
            return "padding: 10px;";
        }
        if (lineaActual == 1)
        {
            return "padding-top: 10px; padding-right: 10px; padding-left: 10px;";
        }
        return lineaActual == lineasTotales ?
            "padding-right: 10px; padding-left: 10px; padding-bottom: 10px;" :
            "padding-top: 2px;padding-right: 10px; padding-left: 10px; padding-bottom: 2px;";
    }

    private string GetColorLine(Color? colorLinea, bool isTitle, int numberLine)
    {
        if (colorLinea != null)
        {
            var color = colorLinea.Value;
            var col = ToHex(color);
            return col;
        }

        if (isTitle)
        {
            return _mailConfigurator.BackgroundColorTitle;
        }

        return _mailConfigurator.IsToggleColorInLines ?
            (numberLine % 2 == 0
                ? _mailConfigurator.BackgroundColorEvenLine
                : _mailConfigurator.BackgroundColorOodLine)
            : _mailConfigurator.BackgroundColorOodLine;
    }
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