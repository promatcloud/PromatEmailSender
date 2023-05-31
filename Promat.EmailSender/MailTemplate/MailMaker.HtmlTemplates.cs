namespace Promat.EmailSender.MailTemplate
{
    public partial class MailMaker
    {
        // Plantilla hmtl donde están las etiquetas de apertura necesarias para la tabla asi como sus estilos
        private const string TemplateHtmlHead = @"<!DOCTYPE html PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"" ""http://www.w3.org/TR/REC-html40/loose.dtd"">
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
    @media only screen and (min-width: 860px) {
      .u-row {
        width: 840px !important;
      }

      .u-row .u-col {
        vertical-align: top;
      }

      .u-row .u-col-18p81 {
        width: 158.004px !important;
      }

      .u-row .u-col-33p33 {
        width: 279.972px !important;
      }

      .u-row .u-col-66p67 {
        width: 560.028px !important;
      }

      .u-row .u-col-81p19 {
        width: 681.9959999999999px !important;
      }

      .u-row .u-col-100 {
        width: 840px !important;
      }

    }

    @media (max-width: 860px) {
      .u-row-container {
        max-width: 100% !important;
        padding-left: 0px !important;
        padding-right: 0px !important;
      }

      .u-row .u-col {
        min-width: 320px !important;
        max-width: 100% !important;
        display: block !important;
      }

      .u-row {
        width: 100% !important;
      }

      .u-col {
        width: 100% !important;
      }

      .u-col>div {
        margin: 0 auto;
      }
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

    * {
      line-height: inherit;
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
  <!--[if IE]><div class=""ie-container""><![endif]-->
  <!--[if mso]><div class=""mso-container""><![endif]-->
  <table
    style=""border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;min-width: 320px;Margin: 0 auto;background-color: #FFFFFF;width:100%""
    cellpadding=""0"" cellspacing=""0"">
    <tbody>
      <tr style=""vertical-align: top"">
        <td style=""word-break: break-word;border-collapse: collapse !important;vertical-align: top"">
          <!--[if (mso)|(IE)]><table width=""100%"" cellpadding=""0"" cellspacing=""0"" border=""0""><tr><td align=""center"" style=""background-color: #FFFFFF;""><![endif]-->
    
        ";

        // Plantilla hmtl donde introducimos la cabecera y sustituimos imagen y título
        private const string TemplateHtmlConfigureHeadEmail = @"	

  <div class=""u-row-container"" style=""padding: 0px;background-color: transparent"">
            <div class=""u-row""
              style=""min-width: 320px;max-width: 840px;overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: transparent;"">
              <div
                style=""border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;"">
                <!--[if (mso)|(IE)]><table width=""100%"" cellpadding=""0"" cellspacing=""0"" border=""0""><tr><td style=""padding: 0px;background-color: transparent;"" align=""center""><table cellpadding=""0"" cellspacing=""0"" border=""0"" style=""width:840px;""><tr style=""background-color: transparent;""><![endif]-->

                <!--[if (mso)|(IE)]><td align=""center"" width=""280"" style=""width: 280px;padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;"" valign=""top""><![endif]-->
                <div class=""u-col u-col-33p33""
                  style=""max-width: 320px;min-width: 280px;display: table-cell;vertical-align: top;"">
                  <div
                    style=""height: 100%;width: 100% !important;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;"">
                    <!--[if (!mso)&(!IE)]><!-->
                    <div
                      style=""box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;"">
                      <!--<![endif]-->

                      <table style=""font-family:arial,helvetica,sans-serif;"" role=""presentation"" cellpadding=""0""
                        cellspacing=""0"" width=""100%"" border=""0"">
                        <tbody>
                          <tr>
                            <td
                              style=""overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:arial,helvetica,sans-serif;""
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

                      <!--[if (!mso)&(!IE)]><!-->
                    </div><!--<![endif]-->
                  </div>
                </div>
                <!--[if (mso)|(IE)]></td><![endif]-->
                <!--[if (mso)|(IE)]><td align=""center"" width=""560"" style=""width: 560px;padding: 220px 0px 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;"" valign=""top""><![endif]-->
                <div class=""u-col u-col-66p67""
                  style=""max-width: 320px;min-width: 560px;display: table-cell;vertical-align: top;"">
                  <div
                    style=""height: 100%;width: 100% !important;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;"">
                    <!--[if (!mso)&(!IE)]><!-->
                    <div
                      style=""box-sizing: border-box; height: 100%; padding: 190px 0px 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;"">
                      <!--<![endif]-->

                      <table style=""font-family:arial,helvetica,sans-serif;"" role=""presentation"" cellpadding=""0""
                        cellspacing=""0"" width=""100%"" border=""0"">
                        <tbody>
                          <tr>
                            <td
                              style=""overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:arial,helvetica,sans-serif;""
                              align=""left"">

                              <div style=""font-size: 17px; line-height: 130%; text-align: left; word-wrap: break-word;"">
                                <[HeaderSize] style=""line-height: 130%;"">[Title]</[HeaderSize]>
                              </div>

                            </td>
                          </tr>
                        </tbody>
                      </table>

                      <!--[if (!mso)&(!IE)]><!-->
                    </div><!--<![endif]-->
                  </div>
                </div>
                <!--[if (mso)|(IE)]></td><![endif]-->
                <!--[if (mso)|(IE)]></tr></table></td></tr></table><![endif]-->
              </div>
            </div>
          </div>

        
        ";

        // Plantilla html para poner una línea separadora
        private const string TemplateHtmlSeparatorLine = @"
          <div class=""u-row-container"" style=""padding: 0px;background-color: transparent"">
            <div class=""u-row""
              style=""min-width: 320px;max-width: 840px;overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: transparent;"">
              <div
                style=""border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;"">
                <!--[if (mso)|(IE)]><table width=""100%"" cellpadding=""0"" cellspacing=""0"" border=""0""><tr><td style=""padding: 0px;background-color: transparent;"" align=""center""><table cellpadding=""0"" cellspacing=""0"" border=""0"" style=""width:840px;""><tr style=""background-color: transparent;""><![endif]-->

                <!--[if (mso)|(IE)]><td align=""center"" width=""840"" style=""width: 840px;padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;"" valign=""top""><![endif]-->
                <div class=""u-col u-col-100""
                  style=""max-width: 320px;min-width: 840px;display: table-cell;vertical-align: top;"">
                  <div
                    style=""height: 100%;width: 100% !important;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;"">
                    <!--[if (!mso)&(!IE)]><!-->
                    <div
                      style=""box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;"">
                      <!--<![endif]-->

                      <table style=""font-family:arial,helvetica,sans-serif;"" role=""presentation"" cellpadding=""0""
                        cellspacing=""0"" width=""100%"" border=""0"">
                        <tbody>
                          <tr>
                            <td
                              style=""overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:arial,helvetica,sans-serif;""
                              align=""left"">

                              <table height=""0px"" align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%""
                                style=""border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 1px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%"">
                                <tbody>
                                  <tr style=""vertical-align: top"">
                                    <td
                                      style=""word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%"">
                                      <span>&nbsp;</span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>

                            </td>
                          </tr>
                        </tbody>
                      </table>

                      <!--[if (!mso)&(!IE)]><!-->
                    </div><!--<![endif]-->
                  </div>
                </div>
                <!--[if (mso)|(IE)]></td><![endif]-->
                <!--[if (mso)|(IE)]></tr></table></td></tr></table><![endif]-->
              </div>
            </div>
          </div>

        ";

        // Cada linea de la tabla que queramos componer en el correo
        private const string TemplateHtmlAddLine = @"
         <div class=""u-row-container"" style=""padding: 0px;background-color: transparent"">
            <div class=""u-row""
              style=""min-width: 320px;max-width: 840px;overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: transparent;"">
              <div
                style=""border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;"">
                <!--[if (mso)|(IE)]><table width=""100%"" cellpadding=""0"" cellspacing=""0"" border=""0""><tr><td style=""padding: 0px;background-color: transparent;"" align=""center""><table cellpadding=""0"" cellspacing=""0"" border=""0"" style=""width:840px;""><tr style=""background-color: transparent;""><![endif]-->

                <!--[if (mso)|(IE)]><td align=""center"" width=""840"" style=""background-color: #f8cba3;width: 840px;padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;"" valign=""top""><![endif]-->
                <div class=""u-col u-col-100""
                  style=""max-width: 320px;min-width: 840px;display: table-cell;vertical-align: top;"">
                  <div
                    style=""[Color]height: 100%;width: 100% !important;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;"">
                    <!--[if (!mso)&(!IE)]><!-->
                    <div
                      style=""box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;"">
                      <!--<![endif]-->

                      <table style=""font-family:arial,helvetica,sans-serif;"" role=""presentation"" cellpadding=""0""
                        cellspacing=""0"" width=""100%"" border=""0"">
                        <tbody>
                          <tr>
                            <td
                              style=""overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:arial,helvetica,sans-serif;""
                              align=""left"">

                              <div style=""font-size: 14px; line-height: 140%; text-align: [TagTextAlign]; word-wrap: break-word;"">
                                <p style=""line-height: 140%;"">[Line]</p>
                              </div>

                            </td>
                          </tr>
                        </tbody>
                      </table>

                      <!--[if (!mso)&(!IE)]><!-->
                    </div><!--<![endif]-->
                  </div>
                </div>
                <!--[if (mso)|(IE)]></td><![endif]-->
                <!--[if (mso)|(IE)]></tr></table></td></tr></table><![endif]-->
              </div>
            </div>
          </div>
        ";

        // Cada linea con dos columnas de la tabla que queramos componer en el correo 
        private const string TemplateHtmlAddLineTwoColumns = @"
          <div class=""u-row-container"" style=""padding: 0px;background-color: transparent"">
            <div class=""u-row""
              style=""min-width: 320px;max-width: 840px;overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: transparent;"">
              <div
                style=""border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;"">
                <!--[if (mso)|(IE)]><table width=""100%"" cellpadding=""0"" cellspacing=""0"" border=""0""><tr><td style=""padding: 0px;background-color: transparent;"" align=""center""><table cellpadding=""0"" cellspacing=""0"" border=""0"" style=""width:840px;""><tr style=""background-color: transparent;""><![endif]-->
                <!--[if (mso)|(IE)]><td align=""center"" width=""158"" style=""width: 158px;padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;"" valign=""top""><![endif]-->
                <div class=""u-col u-col-18p81""
                  style=""[Color]max-width: 320px;min-width: 158px;display: table-cell;vertical-align: top;"">
                  <div
                    style=""height: 100%;width: 100% !important;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;"">
                    <!--[if (!mso)&(!IE)]><!-->
                    <div
                      style=""box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;"">
                      <!--<![endif]-->

                      <table style=""font-family:arial,helvetica,sans-serif;"" role=""presentation"" cellpadding=""0""
                        cellspacing=""0"" width=""100%"" border=""0"">
                        <tbody>
                          <tr>
                            <td
                              style=""overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:arial,helvetica,sans-serif;""
                              align=""left"">
                              <div style=""font-size: 14px; line-height: 140%; text-align: right; word-wrap: break-word;"">
								
								[LeftColumn]
								
                              </div>
                            </td>
                          </tr>
                        </tbody>
                      </table>

                      <!--[if (!mso)&(!IE)]><!-->
                    </div><!--<![endif]-->
                  </div>
                </div>
                <!--[if (mso)|(IE)]></td><![endif]-->
                <!--[if (mso)|(IE)]><td align=""center"" width=""681"" style=""width: 681px;padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;"" valign=""top""><![endif]-->
                <div class=""u-col u-col-81p19""
                  style=""[Color]max-width: 320px;min-width: 682px;display: table-cell;vertical-align: top;"">
                  <div
                    style=""height: 100%;width: 100% !important;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;"">
                    <!--[if (!mso)&(!IE)]><!-->
                    <div
                      style=""box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;"">
                      <!--<![endif]-->

                      <table style=""font-family:arial,helvetica,sans-serif;"" role=""presentation"" cellpadding=""0""
                        cellspacing=""0"" width=""100%"" border=""0"">
                        <tbody>
                          <tr>
                            <td
                              style=""overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:arial,helvetica,sans-serif;""
                              align=""left"">
                              <div style=""font-size: 14px; line-height: 140%; text-align: left; word-wrap: break-word;"">
							  
								[RightColumn]
								
                              </div>
                            </td>
                          </tr>
                        </tbody>
                      </table>

                      <!--[if (!mso)&(!IE)]><!-->
                    </div><!--<![endif]-->
                  </div>
                </div>
                <!--[if (mso)|(IE)]></td><![endif]-->
                <!--[if (mso)|(IE)]></tr></table></td></tr></table><![endif]-->
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

        private const string TagImage = "[Image]";
        private const string TagTitle = "[Title]";
        private const string TagLine = "[Line]";
        private const string TagColorLine = "[Color]";
        private const string TagLeftColumn = "[LeftColumn]";
        private const string TagRightColumn = "[RightColumn]";
        private const string TagHeaderSize = "[HeaderSize]";
        private const string TagTextAlign = "[TagTextAlign]";
        
        private string GetTagLine(bool isBoldText, string textLine)
        {
            return isBoldText ? "<p><strong>" + textLine + "</strong></p>" : "<p>" + textLine + "</p>";
        }
        private string GetTagImage(string urlPicture, int maxWidth)
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
    }
}