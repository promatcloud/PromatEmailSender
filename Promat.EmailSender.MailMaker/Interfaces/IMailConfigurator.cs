using Promat.EmailSender.MailMaker.Enums;
using System.Drawing;

namespace Promat.EmailSender.MailMaker.Interfaces;

public interface IMailConfigurator
{
    /// <summary>
    /// Ruta de la imagen que queremos mostrar
    /// </summary>
    string PathPicture { get; }
    /// <summary>
    /// Color de la línea impar de la plantilla html
    /// </summary>
    string BackgroundColorOodLine { get; }
    /// <summary>
    /// Color de la línea par de la plantilla html
    /// </summary>
    string BackgroundColorEvenLine { get; }
    /// <summary>
    /// Color de la línea titulo de la plantilla html
    /// </summary>
    string BackgroundColorTitle { get; }
    /// <summary>
    /// Indica si se utilizan colores de las líneas de forma alterna.
    /// True utiliza los colores configurados en "BackgroundColorOodLine" y "BackgroundColorEventLine".
    /// False utiliza el color configurado en "BackgroundColorOodLine".
    /// </summary>
    bool IsToggleColorInLines { get; }
    /// <summary>
    /// Porcentaje que ocupa la columna de la izquierda
    /// </summary>
    int PercentageColumn { get; }
    /// <summary>
    /// Porcentaje que ocupa la columna de la izquierda cuando hay tres columnas
    /// </summary>
    int PercentageLeftColumn { get; }
    /// <summary>
    /// Porcentaje que ocupa la columna de la derecha cuando hay tres columnas
    /// </summary>
    int PercentageCenterColumn { get; }
    /// <summary>
    /// Ancho de la imagen de cabecera, en px
    /// </summary>
    int HeaderImageWidth { get; }
    /// <summary>
    /// Ancho de la imagen de cabecera, en px
    /// </summary>
    int HeaderImageHeight { get; }
    /// <summary>
    /// Ancho total del correo, en px
    /// </summary>
    int CorreoWidth { get; }
    /// <summary>
    /// Color del Link
    /// </summary>
    string LinkColorStyle { get; } 
    /// <summary>
    /// Color del Link una vez visitado
    /// </summary>
    string VisitedColorStyle { get; } 
    /// <summary>
    /// Color del Link cuando pasas el ratón por encima
    /// </summary>
    string HoverColorStyle { get; } 
    /// <summary>
    /// Color del Link cuando se mantiene el Link pulsado
    /// </summary>
    string ActiveColorStyle { get; } 
    /// <summary>
    /// Decoración de la línea del Link
    /// </summary>
    HtmlDecorationLineEnum LinkTextDecorationLine { get; }
    /// <summary>
    /// Decoración de la línea del Link una vez visitado
    /// </summary>
    HtmlDecorationLineEnum VisitedTextDecorationLine { get; }
    /// <summary>
    /// Decoración de la línea del Link cuando pasas el ratón por encima
    /// </summary>
    HtmlDecorationLineEnum HoverTextDecorationLine { get; }
    /// <summary>
    /// Decoración de la línea del Link cuando se mantiene el Link pulsado
    /// </summary>
    HtmlDecorationLineEnum ActiveTextDecorationLine { get; }
    /// <summary>
    /// Estilo de la línea del Link
    /// </summary>
    HtmlDecorationStyleEnum LinkTextDecorationStyle { get; }
    /// <summary>
    /// Estilo de la línea del Link una vez visitado
    /// </summary>
    HtmlDecorationStyleEnum VisitedTextDecorationStyle { get; }
    /// <summary>
    /// Estilo de la línea del Link cuando pasas el ratón por encima
    /// </summary>
    HtmlDecorationStyleEnum HoverTextDecorationStyle { get; }
    /// <summary>
    /// Estilo de la línea del Link cuando se mantiene el Link pulsado
    /// </summary>
    HtmlDecorationStyleEnum ActiveTextDecorationStyle { get; }
    /// <summary>
    /// Tipo de fuente
    /// </summary>
    string FontFamily { get;  }
    /// <summary>
    /// Tamaño de fuente
    /// </summary>
    int FontSize { get;  }

    /// <summary>
    /// Establece sobre que 'MailMaker' vamos a hacer la configuracion 'MailConfiguration'
    /// </summary>
    /// <param name="mailMaker">MailMaker sobre el que se hará la configuración</param>
    /// <returns>la propia instancia de <see cref="IMailConfigurator"/> para encadenar métodos</returns>
    IMailConfigurator SetMailMaker(IMailMaker mailMaker);

    /// <summary>
    /// Establece el color de fondo de la línea de title.
    /// <para>
    /// Podemos establecerlo en diferentes formatos Ejemplo "#FF0", "#808080",  "rgb(255, 255, 0)" o <see cref="Color"/>
    /// </para>
    /// </summary>
    /// <param name="cssColor">string del color en formato "#FF0", "#808080",  "rgb(255, 255, 0)"</param>
    /// <returns>la propia instancia de <see cref="IMailConfigurator"/> para encadenar métodos</returns>
    IMailConfigurator BackgroundTitle(string cssColor);

    /// <summary>
    /// Establece el color de fondo de la línea de title.
    /// <para>
    /// Podemos establecerlo en diferentes formatos Ejemplo "#FF0", "#808080",  "rgb(255, 255, 0)" o <see cref="Color"/>
    /// </para>
    /// </summary>
    /// <param name="color"><see cref="Color"/></param>
    /// <returns>la propia instancia de <see cref="IMailConfigurator"/> para encadenar métodos</returns>
    IMailConfigurator BackgroundTitle(Color color);

    /// <summary>
    /// Configura el color de fondo de la línea impar, color principal en caso de que <see cref="IsToggleColor"/> sea false.
    /// <para>
    /// Podemos establecerlo en diferentes formatos Ejemplo "#FF0", "#808080",  "rgb(255, 255, 0)" o <see cref="Color"/>
    /// </para>
    /// </summary>
    /// <param name="cssColor">string del color en formato "#FF0", "#808080",  "rgb(255, 255, 0)"</param>
    /// <returns>la propia instancia de <see cref="IMailConfigurator"/> para encadenar métodos</returns>
    IMailConfigurator BackgroundOddLine(string cssColor);

    /// <summary>
    /// Configura el color de fondo de la línea impar, color principal en caso de que <see cref="IsToggleColor"/> sea false.
    /// <para>
    /// Podemos establecerlo en diferentes formatos Ejemplo "#FF0", "#808080",  "rgb(255, 255, 0)" o <see cref="Color"/>
    /// </para>
    /// </summary>
    /// <param name="color"><see cref="Color"/></param>
    /// <returns>la propia instancia de <see cref="IMailConfigurator"/> para encadenar métodos</returns>
    IMailConfigurator BackgroundOddLine(Color color);

    /// <summary>
    /// Configura el color de fondo de la línea par. Este color solo se verá si <see cref="IsToggleColor"/> es true
    /// <para>
    /// Podemos establecerlo en diferentes formatos Ejemplo "#FF0", "#808080",  "rgb(255, 255, 0)" o <see cref="Color"/>
    /// </para>
    /// </summary>
    /// <param name="cssColor">string del color en formato "#FF0", "#808080",  "rgb(255, 255, 0)"</param>
    /// <returns>la propia instancia de <see cref="IMailConfigurator"/> para encadenar métodos</returns>
    IMailConfigurator BackgroundEvenLine(string cssColor);

    /// <summary>
    /// Configura el color de fondo de la línea par. Este color solo se verá si <see cref="IsToggleColor"/> es true
    /// <para>
    /// Podemos establecerlo en diferentes formatos Ejemplo "#FF0", "#808080",  "rgb(255, 255, 0)" o <see cref="Color"/>
    /// </para>
    /// </summary>
    /// <param name="color"><see cref="Color"/></param>
    /// <returns>la propia instancia de <see cref="IMailConfigurator"/> para encadenar métodos</returns>
    IMailConfigurator BackgroundEvenLine(Color color);

    /// <summary>
    /// Configura la ruta de la imagen que queremos utilizar.
    /// </summary>
    /// <param name="pathPicture">Ruta de la imagen a establecer</param>
    /// <returns>la propia instancia de <see cref="IMailConfigurator"/> para encadenar métodos</returns>
    IMailConfigurator SetPathPicture(string pathPicture);

    /// <summary>
    /// Indica si se utilizan colores de las líneas de forma alterna
    /// <para>
    /// True utiliza los colores configurados en <see cref="BackgroundColorOodLine"/>  y <see cref="BackgroundColorEvenLine"/><br/>
    /// False utiliza el color configurado en "BackgroundColorOodLine"
    /// </para>
    /// </summary>
    /// <param name="isDifferent">Por defecto es true</param>
    /// <returns>la propia instancia de <see cref="IMailConfigurator"/> para encadenar métodos</returns>
    IMailConfigurator IsToggleColor(bool isDifferent);

    /// <summary>
    /// Establece el porcentaje que ocupa la columna de la izquierda, cuando la fila tiene dos columnas. La columna de la derecha ocupará el espacio restante
    /// <para>
    /// Se admiten valores: 20 &lt;= valor &lt;= 75<br/>
    /// Si es menor que 20 se establecerá a 20<br/>
    /// Si es mayor que 75 se establecerá a 75
    /// </para>
    /// </summary>
    /// <param name="percentageColumn">Porcentaje columna</param>
    /// <returns>la propia instancia de <see cref="IMailConfigurator"/> para encadenar métodos</returns>
    IMailConfigurator SetPercentageTwoColumn(int percentageColumn);
    /// <summary>
    /// Establece el porcentaje que ocupa la columna de la izquierda y la del centro, cuando la fila tiene tres columnas. La columna de la derecha ocupará el espacio restante
    /// <para>
    /// Se admiten valores: 20 &lt;= valor &lt;= 60<br/>
    /// Si es menor que 20 se establecerá a 20<br/>
    /// Si es mayor que 60 se establecerá a 60<br/>
    /// Si la suma de <see cref="percentageLeftColumn"/> y <see cref="percentageRightColumn"/> &gt; 80. Se establece la columna izquierda a 20 y la del centro a 40.
    /// </para>
    /// </summary>
    /// <param name="percentageLeftColumn">Porcentaje columna izquierda por defecto a 20</param>
    /// <param name="percentageRightColumn">Porcentaje columna derecha por defecto a 40</param>
    /// <returns>la propia instancia de <see cref="IMailConfigurator"/> para encadenar métodos</returns>
    IMailConfigurator SetPercentageThreeColumn(int percentageLeftColumn, int percentageRightColumn);
    /// <summary>
    /// Establece el tamaño de ancho de la plantilla del correo
    /// <para>
    /// Se admiten valores: 840 &lt;= valor &lt;= 1500<br/>
    /// Si es menor que 840 px se establecerá a 840 px<br/>
    /// Si es mayor que 1450 px se establecerá a 1450 px
    /// </para>
    /// </summary>
    /// <param name="correoWidth"></param>
    /// <returns>la propia instancia de <see cref="IMailConfigurator"/> para encadenar métodos</returns>
    IMailConfigurator SetCorreoWidth(int correoWidth);
    /// <summary>
    /// Establece el ancho de la imagen y utiliza el alto para alínear el título
    /// <para>
    /// Se admiten valores: 50 &lt;= valor &lt;= 1000<br/>
    /// Si es menor que 50 px se establecerá a 50 px<br/>
    /// Si es mayor que 1000 px se establecerá a 1000 px
    /// </para>
    /// </summary>
    /// <param name="headerImageWidth">Ancho de la imagen en px</param>
    /// <param name="headerImageHeight">Altura de la imagen en px (en proporción a la anchura)</param>
    /// <returns>la propia instancia de <see cref="IMailConfigurator"/> para encadenar métodos</returns>
    IMailConfigurator SetImageSize(int headerImageWidth, int headerImageHeight);
    /// <summary>
    /// Establece los tamaños de la imagen en la cabecera de la plantilla del correo en px, usar si la imagen es cuadrada.
    /// <para>
    /// Se admiten valores: 50 &lt;= valor &lt;= 1000<br/>
    /// Si es menor que 50 px se establecerá a 50 px<br/>
    /// Si es mayor que 1000 px se establecerá a 1000 px
    /// </para>
    /// </summary>
    /// <param name="size">Ancho y alto de la imagen en px</param>
    /// <returns>la propia instancia de <see cref="IMailConfigurator"/> para encadenar métodos</returns>
    IMailConfigurator SetImageSize(int size);
    /// <summary>
    /// Establece los estilos y la decoración de los link 
    /// </summary>
    /// <param name="linkColorStyle">Color del link</param>
    /// <param name="visitedColorStyle">Color del link una vez visitado</param>
    /// <param name="hoverColorStyle">Color del link al pasar el ratón por encima</param>
    /// <param name="activeColorStyle">Color del link mientras esta pulsado</param>
    /// <param name="linkTextDecorationLine">Decoración de la línea del link</param>
    /// <param name="visitedTextDecorationLine">Decoración de la línea del link una vez visitado</param>
    /// <param name="hoverTextDecorationLine">Decoración de la línea del link al pasar el ratón por encima</param>
    /// <param name="activeTextDecorationLine">Decoración de la línea del link mientras esta pulsado</param>
    /// <param name="linkTextDecorationStyle">Estilo de la línea del link</param>
    /// <param name="visitedTextDecorationStyle">Estilo de la línea del link una vez visitado</param>
    /// <param name="hoverTextDecorationStyle">Estilo de la línea del link al pasar el ratón por encima</param>
    /// <param name="activeTextDecorationStyle">Estilo de la línea del link mientras esta pulsado</param>
    /// <returns>la propia instancia de <see cref="IMailConfigurator"/> para encadenar métodos</returns>
    IMailConfigurator SetLinksColorsAndTextDecoration(
        Color linkColorStyle , Color visitedColorStyle, Color hoverColorStyle, Color activeColorStyle,
        HtmlDecorationLineEnum linkTextDecorationLine = HtmlDecorationLineEnum.None, 
        HtmlDecorationLineEnum visitedTextDecorationLine = HtmlDecorationLineEnum.None, 
        HtmlDecorationLineEnum hoverTextDecorationLine = HtmlDecorationLineEnum.None, 
        HtmlDecorationLineEnum activeTextDecorationLine = HtmlDecorationLineEnum.None,
        HtmlDecorationStyleEnum linkTextDecorationStyle = HtmlDecorationStyleEnum.None, 
        HtmlDecorationStyleEnum visitedTextDecorationStyle = HtmlDecorationStyleEnum.None, 
        HtmlDecorationStyleEnum hoverTextDecorationStyle = HtmlDecorationStyleEnum.None, 
        HtmlDecorationStyleEnum activeTextDecorationStyle = HtmlDecorationStyleEnum.None
    );
    /// <summary>
    /// Estable el tipo y tamaño de la fuente
    /// </summary>
    /// <param name="fontFamilyOne">Primera opción de fuente<br/>
    /// Defecto <see cref="HtmlFontFamilyEnum.Arial"/></param>
    /// <param name="fontFamilyTwo">Segunda opción de fuente por si falla la primera<br/>
    /// Defecto <see cref="HtmlFontFamilyEnum.Helvetica"/></param>
    /// <param name="genericFamily">Familia genérica por si no estuvieran disponibles las fuentes<br/>
    /// Defecto <see cref="HtmlGenericFamilyEnum.SansSerif"/></param>
    /// <param name="fontSize">Tamaño para la fuente en px<br/>
    /// Defecto 14</param>
    /// <returns>la propia instancia de <see cref="IMailConfigurator"/> para encadenar métodos</returns>
    IMailConfigurator SetFontGenericFamilyAndFontSize(
        HtmlFontFamilyEnum fontFamilyOne = HtmlFontFamilyEnum.Arial, HtmlFontFamilyEnum fontFamilyTwo = HtmlFontFamilyEnum.Helvetica, 
        HtmlGenericFamilyEnum genericFamily = HtmlGenericFamilyEnum.SansSerif, int fontSize = 14);
    /// <summary>
    /// Finaliza la configuración del objeto MailConfigurator
    /// </summary>
    /// <returns>la instancia de <see cref="IMailMaker"/> que estamos configurando</returns>
    IMailMaker EndConfiguration();
}