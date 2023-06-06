using System.Drawing;

namespace Promat.EmailSender.MailTemplate.Interfaces
{
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
        string BackgroundColorEventLine { get; }
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
        /// Establece sobre que 'MailMaker' vamos a hacer la configuracion 'MailConfiguration'
        /// </summary>
        /// <param name="mailMaker">MailMaker sobre el que se hará la configuración</param>
        /// <returns>la propia instancia de <see cref="IMailConfigurator"/> para encadenar métodos</returns>
        IMailConfigurator SetMailMaker(IMailMaker mailMaker);

        /// <summary>
        /// Establece el color de fondo de la linea de title.
        /// <para>
        /// Podemos establecerlo en diferentes formatos Ejemplo "#FF0", "#808080",  "rgb(255, 255, 0)" o <see cref="Color"/>
        /// </para>
        /// </summary>
        /// <param name="cssColor">string del color en formato "#FF0", "#808080",  "rgb(255, 255, 0)"</param>
        /// <returns>la propia instancia de <see cref="IMailConfigurator"/> para encadenar métodos</returns>
        IMailConfigurator BackgroundTitle(string cssColor);
        
        /// <summary>
        /// Establece el color de fondo de la linea de title.
        /// <para>
        /// Podemos establecerlo en diferentes formatos Ejemplo "#FF0", "#808080",  "rgb(255, 255, 0)" o <see cref="Color"/>
        /// </para>
        /// </summary>
        /// <param name="cssColor"><see cref="Color"/></param>
        /// <returns>la propia instancia de <see cref="IMailConfigurator"/> para encadenar métodos</returns>
        IMailConfigurator BackgroundTitle(Color cssColor);

        /// <summary>
        /// Configura el color de fondo de la linea impar, color principal en caso de que <see cref="IsToggleColor"/> sea false.
        /// <para>
        /// Podemos establecerlo en diferentes formatos Ejemplo "#FF0", "#808080",  "rgb(255, 255, 0)" o <see cref="Color"/>
        /// </para>
        /// </summary>
        /// <param name="cssColor">string del color en formato "#FF0", "#808080",  "rgb(255, 255, 0)"</param>
        /// <returns>la propia instancia de <see cref="IMailConfigurator"/> para encadenar métodos</returns>
        IMailConfigurator BackgroundOddLine(string cssColor);
        
        /// <summary>
        /// Configura el color de fondo de la linea impar, color principal en caso de que <see cref="IsToggleColor"/> sea false.
        /// <para>
        /// Podemos establecerlo en diferentes formatos Ejemplo "#FF0", "#808080",  "rgb(255, 255, 0)" o <see cref="Color"/>
        /// </para>
        /// </summary>
        /// <param name="cssColor"><see cref="Color"/></param>
        /// <returns>la propia instancia de <see cref="IMailConfigurator"/> para encadenar métodos</returns>
        IMailConfigurator BackgroundOddLine(Color cssColor);

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
        /// <param name="cssColor"><see cref="Color"/></param>
        /// <returns>la propia instancia de <see cref="IMailConfigurator"/> para encadenar métodos</returns>
        IMailConfigurator BackgroundEvenLine(Color cssColor);

        /// <summary>
        /// Configura la ruta de la imagen que queremos utilizar.
        /// </summary>
        /// <param name="pathPicture">Ruta de la imagen a establecer</param>
        /// <returns>la propia instancia de <see cref="IMailConfigurator"/> para encadenar métodos</returns>
        IMailConfigurator SetPathPicture(string pathPicture);

        /// <summary>
        /// Indica si se utilizan colores de las líneas de forma alterna
        /// <para>
        /// True utiliza los colores configurados en <see cref="BackgroundColorOodLine"/>  y <see cref="BackgroundColorEventLine"/><br/>
        /// False utiliza el color configurado en "BackgroundColorOodLine"
        /// </para>
        /// </summary>
        /// <param name="isDifferent">Por defecto es true</param>
        /// <returns>la propia instancia de <see cref="IMailConfigurator"/> para encadenar métodos</returns>
        IMailConfigurator IsToggleColor(bool isDifferent);

        /// <summary>
        /// Establece el porcentaje que ocupa la columna de la izquierda, cuando la fila tiene dos columnas.
        /// <para>
        /// Se admiten valores: 20 &lt;= valor &lt;= 75<br/>
        /// Si es menor que 20 se establecerá a 20<br/>
        /// Si es mayor que 75 se establecerá a 75
        /// </para>
        /// </summary>
        /// <param name="percentageColumn">Porcentaje columna</param>
        /// <returns>la propia instancia de <see cref="IMailConfigurator"/> para encadenar métodos</returns>
        IMailConfigurator SetPercentageColumn(int percentageColumn);
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
        /// Establece los tamaños de la imagen en la cabecera de la plantilla del correo en px
        /// <para>
        /// Se admiten valores: 50 &lt;= valor &lt;= 1000<br/>
        /// Si es menor que 50 px se establecerá a 50 px<br/>
        /// Si es mayor que 1000 px se establecerá a 1000 px
        /// </para>
        /// </summary>
        /// <param name="headerImageWidth">Ancho de la imagen en px</param>
        /// <param name="headerImageHeight">Altura de la imagen en px</param>
        /// <returns>la propia instancia de <see cref="IMailConfigurator"/> para encadenar métodos</returns>
        IMailConfigurator SetImageSize(int headerImageWidth, int headerImageHeight);
        /// <summary>
        /// Finaliza la configuración del objeto MailConfigurator
        /// </summary>
        /// <returns>la instancia de <see cref="IMailMaker"/> que estamos configurando</returns>
        IMailMaker EndConfiguration();
    }
}