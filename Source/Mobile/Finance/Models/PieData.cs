namespace Finance.Models;

/// <summary>
/// Representa um dado de um segmento de gráfico de pizza (PieChart), contendo um rótulo, um valor e uma cor associada.
/// </summary>
/// <param name="label">O rótulo descritivo do segmento.</param>
/// <param name="value">O valor numérico representando a proporção do segmento.</param>
/// <param name="color">O identificador da cor associada ao segmento.</param>
public class PieData(string label, double value, int color) {

    #region Properties

    /// <summary>
    /// Obtém o rótulo descritivo do segmento.
    /// </summary>
    public string Label { get; } = label;

    /// <summary>
    /// Obtém o valor numérico representando a proporção do segmento no gráfico.
    /// </summary>
    public double Value { get; } = value;

    /// <summary>
    /// Obtém o identificador da cor associada ao segmento.
    /// </summary>
    public int Color { get; } = color;

    #endregion Properties
}