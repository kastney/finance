namespace Finance.Models;

/// <summary>
/// Representa os dados dinâmicos e específicos de uma alocação de ativo dentro de uma carteira,
/// como quantidade, preço, variação e performance.
/// Esses dados são persistidos individualmente por tipo de ativo.
/// </summary>
internal class AssetAllocationData {

    #region Properties

    /// <summary>
    /// Quantidade de ativos do tipo alocado.
    /// </summary>
    public int Count { get; set; }

    /// <summary>
    /// Preço atual total dos ativos alocados, considerando a quantidade.
    /// </summary>
    public float Price { get; set; }

    /// <summary>
    /// Variação percentual do ativo em um período recente (por exemplo, diário).
    /// </summary>
    public float Variation { get; set; }

    /// <summary>
    /// Performance acumulada do ativo em determinado intervalo (ex: mês, ano).
    /// </summary>
    public float Performance { get; set; }

    #endregion Properties
}