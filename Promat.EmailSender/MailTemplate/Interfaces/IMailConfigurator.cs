namespace Promat.EmailSender.MailTemplate.Interfaces
{
    public interface IMailConfigurator
    {
        /// <summary>
        /// Establece el color de fondo de la linea de title.
        /// Le podemos pasar el color en diferentes formatos Ejemplo "#FF0", "#808080", "rgb(255, 255, 0)"
        /// </summary>
        /// <param name="cssColor"></param>
        /// <returns>MailConfigurator</returns>
        IMailConfigurator BackgroundTitle(string cssColor);

        /// <summary>
        /// Configura el color de fondo de la linea impar, color principal en caso de que 'IsToggleColor' no sea true.
        /// Le podemos pasar el color en diferentes formatos Ejemplo "#FF0", "#808080", "rgb(255, 255, 0)"
        /// </summary>
        /// <param name="cssColor"></param>
        /// <returns>MailConfigurator</returns>
        IMailConfigurator BackgroundOddLine(string cssColor);

        /// <summary>
        /// Configura el color de fondo de la línea par.
        /// Le podemos pasar el color en diferentes formatos Ejemplo "#FF0", "#808080", "rgb(255, 255, 0)"
        /// </summary>
        /// <param name="cssColor"></param>
        /// <returns>MailConfigurator</returns>
        IMailConfigurator BackgroundEvenLine(string cssColor);

        /// <summary>
        /// Configura la ruta de la imagen que queremos utilizar.
        /// </summary>
        /// <param name="pathPicture">Ruta de la imagen a establecer</param>
        /// <returns>MailConfigurator</returns>
        IMailConfigurator SetPathPicture(string pathPicture);

        /// <summary>
        /// Indica si se utilizan colores de las líneas alternos.
        /// True utiliza los colores configurados en "BackgroundLineOod" y "BackgroundLinePair".
        /// False utiliza el color configurado en "BackgroundLineOod".
        /// </summary>
        /// <param name="isDifferent">Por defecto es true</param>
        /// <returns>MailConfigurator</returns>
        IMailConfigurator IsToggleColor(bool isDifferent);
        /// <summary>
        /// Finaliza la configuración del objeto MailConfigurator
        /// </summary>
        /// <returns>MailMaker</returns>
        IMailMaker EndMailConfigurator();
    }
}