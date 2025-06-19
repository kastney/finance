namespace Finance.Models;

/// <summary>
/// Representa os metadados estáticos associados a um tipo de ativo,
/// como título, cultura regional e bandeira.
/// Essas informações não são persistidas, pois são fixas para cada tipo.
/// </summary>
internal class AssetAllocationMeta {

    #region Properties

    /// <summary>
    /// Nome curto descritivo do tipo do ativo.
    /// </summary>
    public string ShortName { get; set; }

    /// <summary>
    /// Nome longo descritivo do tipo do ativo.
    /// </summary>
    public string LongName { get; set; }

    /// <summary>
    /// Código de cultura utilizado para formatação regional (ex: "pt-br", "en-us").
    /// </summary>
    public string Culture { get; set; }

    /// <summary>
    /// Código da bandeira (flag) representando o país de origem do ativo (ex: "pt-br", "en-us").
    /// </summary>
    public string Flag { get; set; }

    #endregion Properties
}