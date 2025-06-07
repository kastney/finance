using Finance.Enumerations;

namespace Finance.Models;

/// <summary>
/// Representa um tipo de ativo disponível para seleção na página de seleção de ativos.
/// </summary>
internal class AvailableAsset {

    #region Properties

    /// <summary>
    /// Representa o nome do ativo disponível.
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Um nome alternativo ou secundário para o ativo, usado para fornecer mais contexto ou detalhes.
    /// </summary>
    public string Subtitle { get; set; }

    /// <summary>
    /// Representa a bandeira do país ou região associada ao ativo.
    /// </summary>
    public string Flag { get; set; }

    /// <summary>
    /// Representa o país ou região associada ao ativo.
    /// </summary>
    public string Locale { get; set; }

    /// <summary>
    /// Cadeia de textos chaves para auxílio na busca e filtragem do ativo.
    /// </summary>
    public string Token { get; set; }

    /// <summary>
    /// Representa o tipo de ativo, que pode ser uma ação, título, moeda, etc.
    /// </summary>
    public AssetType Type { get; set; }

    #endregion Properties
}