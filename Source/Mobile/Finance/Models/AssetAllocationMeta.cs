namespace Finance.Models;

/// <summary>
/// Representa os metadados estáticos associados a um tipo de ativo,
/// como título, cultura regional e bandeira.
/// Essas informações não são persistidas, pois são fixas para cada tipo.
/// </summary>
internal class AssetAllocationMeta {

    #region Properties

    /// <summary>
    /// Título descritivo do tipo de ativo (ex: "Ações", "FIIs").
    /// </summary>
    public string Title { get; set; }

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