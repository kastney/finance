namespace Finance.Models;

/// <summary>
/// Representa um item de banner exibido na tela de apresentação, contendo título, subtítulo e imagem.
/// </summary>
internal class BannerItem {

    /// <summary>
    /// Título principal do banner, usado para destacar o conteúdo da apresentação.
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Texto complementar ao título, explicando brevemente o propósito do banner.
    /// </summary>
    public string Subtitle { get; set; }

    /// <summary>
    /// Caminho da imagem exibida no banner, geralmente armazenada nos recursos do aplicativo.
    /// </summary>
    public string Image { get; set; }
}