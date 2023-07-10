using Promat.EmailSender.Interfaces;
using Promat.EmailSender.MailTemplate.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;

namespace Promat.EmailSender.MailTemplate.Interfaces
{
    public interface IMailMaker : IDisposable
    {
        /// <summary>
        /// Nos devuelve una instancia de MailConfigurator para acceder a sus configuraciones
        /// </summary>
        /// <returns>la instancia de <see cref="IMailConfigurator"/> para encadenar métodos</returns>
        IMailConfigurator Configure();

        /// <summary>
        /// Añade el titulo de nuestra cabecera
        /// </summary>
        /// <param name="title">Texto del titulo de la cabecera</param>
        /// <param name="headerEnum">Tamaño del titulo de la cabecera. Por defecto 'h3'</param>
        /// <param name="htmlTextAlignEnum">Alineación del texto en la fila. Por defecto 'Left'  </param>
        /// <returns>la propia instancia de <see cref="IMailMaker"/> para encadenar métodos</returns>
        IMailMaker TitleHeader(string title, HtmlHeaderEnum headerEnum = HtmlHeaderEnum.H3, HtmlTextAlignEnum htmlTextAlignEnum = HtmlTextAlignEnum.Left);

        /// <summary>
        /// Añade una fila simple a la plantilla
        /// </summary>
        /// <param name="colorLinea">Color de la linea, si no lo pasamos se utilizan los de <see cref="IMailConfigurator"/></param>
        /// <param name="lineText">Texto fila</param>
        /// <param name="isTitle">Si la linea es un titulo<br/>
        /// True usa el color especifico de <see cref="IMailConfigurator.BackgroundColorTitle"/><br/>
        /// False se comporta como el resto de las líneas<br/>
        /// Por defecto false</param>
        /// <param name="fontBold">Si el texto esta en negrita<br/>
        /// Por defecto false</param>
        /// <param name="isLinkTarget">Si el texto es destino de un enlace<br/>
        /// True es destino<br/>
        /// False no es destino<br/>
        /// Si es enlace no puede ser vinculo, en caso de que ambas sean true se tomara como enlace, isLinkTarget = true</param>
        /// <param name="isLinkText">Si es texto de enlace<br/>
        /// True es enlace<br/>
        /// False no es enlace<br/>
        /// Si es vinculo no puede ser enlace, en caso de que ambas sean true se tomara como enlace, isLinkTarget = true</param>
        /// <param name="htmlTabulationEnum">Si el texto esta tabulado<br/>
        /// Por defecto <see cref=" HtmlTabulationEnum.Zero"/></param>
        /// <param name="htmlTextAlignEnum">Alineación del texto en la fila<br/>
        /// Por defecto <see cref="HtmlTextAlignEnum.Left"/></param>
        /// <returns>la propia instancia de <see cref="IMailMaker"/> para encadenar métodos</returns>
        IMailMaker AddLine(
            string lineText, bool isTitle = false, bool fontBold = false,
            bool isLinkTarget = false, bool isLinkText = false, Color? colorLinea = null,
            HtmlTabulationEnum htmlTabulationEnum = HtmlTabulationEnum.Zero,
            HtmlTextAlignEnum htmlTextAlignEnum = HtmlTextAlignEnum.Left);

        /// <summary>
        /// Añade una fila de dos columnas a la plantilla 
        /// </summary>
        /// <param name="colorLinea">Color de la linea, si no lo pasamos se utilizan los de <see cref="IMailConfigurator"/></param>
        /// <param name="leftLine">Texto columna de la izquierda</param>
        /// <param name="rightLine">Texto columna de la derecha</param>
        /// <param name="fontBoldLeft">Texto columna izquierda en negrita. Por defecto false</param>
        /// <param name="fontBoldRight">Texto columna derecha en negrita. Por defecto false</param>
        /// <param name="isLinkTarget">Si el texto de la linea es destino de enlace</param>
        /// <returns>la propia instancia de <see cref="IMailMaker"/> para encadenar métodos</returns>
        IMailMaker AddLine(string leftLine, string rightLine,
            bool fontBoldLeft = false, bool fontBoldRight = false,
            bool isLinkTarget = false, Color? colorLinea = null
            );

        /// <summary>
        /// Añade una fila de dos columnas a la plantilla, la columna de la izquierda sera una imagen. 
        /// </summary>
        /// <param name="colorLinea">Color de la linea, si no lo pasamos se utilizan los de <see cref="IMailConfigurator"/></param>
        /// <param name="urlImage">Url de la imagen a mostrar en la columna de la izquierda</param>
        /// <param name="rightLine">Texto columna derecha</param>
        /// <param name="maxWidth">Tamaño de la imagen, hay que pasarlo con el siguiente formato "32px". Por defecto "20px"</param>
        /// <param name="fontBoldRight">Texto columna derecha en negrita. Por defecto false</param>
        /// <returns>la propia instancia de <see cref="IMailMaker"/> para encadenar métodos</returns>
        IMailMaker AddLineWithImage(string urlImage, string rightLine,
            int maxWidth = 20, bool fontBoldRight = false, Color? colorLinea = null);

        /// <summary>
        /// Añade una fila de tres columnas a la plantilla, la columna de la izquierda sera una imagen y las otras dos texto. 
        /// </summary>
        /// <param name="colorLinea">Color de la linea, si no lo pasamos se utilizan los de <see cref="IMailConfigurator"/></param>
        /// <param name="urlImage">Url de la imagen a mostrar en la columna de la izquierda</param>
        /// <param name="leftLine">Texto columna izquierda</param>
        /// <param name="rightLine">Texto columna derecha</param>
        /// <param name="maxWidth">Tamaño de la imagen, hay que pasarlo con el siguiente formato "32px". Por defecto "20px"</param>
        /// <param name="fontBoldLeft">Texto columna izquierda en negrita. Por defecto false</param>
        /// <param name="fontBoldRight">Texto columna derecha en negrita. Por defecto false</param>
        /// <param name="htmlLeftTextAlignEnum">Alineación del texto en columna izquierda. Por defecto 'Right'  </param>
        /// <param name="htmlCenterTextAlignEnum">Alineación del texto en columna izquierda. Por defecto 'Left'  </param>
        /// <param name="htmlRightTextAlignEnum">Alineación del texto en columna derecha. Por defecto 'Left'  </param>
        /// <returns>la propia instancia de <see cref="IMailMaker"/> para encadenar métodos</returns>
        IMailMaker AddLineWithImage(string urlImage, string leftLine, string rightLine,
            int maxWidth = 20, bool fontBoldLeft = false, bool fontBoldRight = false, Color? colorLinea = null,
            HtmlTextAlignEnum htmlLeftTextAlignEnum = HtmlTextAlignEnum.Right, HtmlTextAlignEnum htmlCenterTextAlignEnum = HtmlTextAlignEnum.Left, HtmlTextAlignEnum htmlRightTextAlignEnum = HtmlTextAlignEnum.Left);

        /// <summary>
        /// Añade una o varias filas de dos columnas a la plantilla 
        /// </summary>
        /// <param name="leftLines">Texto columna de la izquierda</param>
        /// <param name="rightLines">Texto columna de la derecha</param>
        /// <param name="fontBoldLeft">Texto columna izquierda en negrita. Por defecto es false</param>
        /// <param name="fontBoldRight">Texto columna derecha en negrita. Por defecto es false</param>
        /// <returns>la propia instancia de <see cref="IMailMaker"/> para encadenar métodos</returns>
        IMailMaker AddLine(IEnumerable<string> leftLines, IEnumerable<string> rightLines,
            bool fontBoldLeft = false, bool fontBoldRight = false, Color? colorLinea = null);

        /// <summary>
        /// Devuelve la plantilla completa con la información y configuración utilizada.
        /// </summary>
        /// <returns><see cref="string"/> con toda la información de la plantilla</returns>
        string GetHtml();

        /// <summary>
        /// Permite establecer una instancia de <see cref="IEmailSender"/> para poder utilizar el método <see cref="SendMailAsync"/>
        /// </summary>
        /// <param name="emailSender">Instancia de <see cref="IEmailSender"/></param>
        /// <returns>la propia instancia de <see cref="IMailMaker"/> para encadenar métodos</returns>
        IMailMaker SetEmailSender(IEmailSender emailSender);

        /// <summary>
        /// Envía el correo utilizando en el cuerpo la plantilla html configurada.
        /// </summary>
        /// <param name="to">Para quien va dirigido el correo</param>
        /// <param name="subject">Asunto del correo</param>
        /// <param name="cc">Copias para quien va dirigido el correo</param>
        /// <param name="fromEmail">Email remitente</param>
        /// <param name="fromName">Nombre remitente</param>
        /// <returns></returns>
        Task SendMailAsync(string to, string subject, IEnumerable<string> cc = null, string fromEmail = null,
            string fromName = null);
    }
}