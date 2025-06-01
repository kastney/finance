namespace Finance.Models;

/// <summary>
/// Representa um agrupamento de ativos dentro de uma carteira, como "Renda Variável" ou "Caixa".
/// Cada grupo possui um nome e uma lista de alocações de ativos.
/// </summary>
internal class AssetGroup {

    #region Properties

    /// <summary>
    /// Nome do grupo de ativos, definido pelo usuário para organização (ex: "Ações", "Caixa").
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Lista de tipos de ativos que pertencem a este grupo.
    /// </summary>
    public List<AssetAllocation> Assets { get; set; }

    /// <summary>
    /// Indicador do estado de ativado de um grupo de ativos.
    /// </summary>
    public bool Enabled { get; set; }

    #endregion Properties

    #region Constructor

    /// <summary>
    /// Inicializa uma nova instância do grupo de ativos com a lista de alocações vazia.
    /// </summary>
    public AssetGroup() {
        // Inicializa a lista de ativos do grupo.
        Assets = [];
    }

    #endregion Constructor
}