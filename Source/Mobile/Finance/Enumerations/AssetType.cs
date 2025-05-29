namespace Finance.Enumerations;

/// <summary>
/// Representa os diferentes tipos de ativos que podem compor uma carteira de investimentos.
/// Cada tipo é identificado por um valor numérico único, utilizado para integração com dados externos.
/// </summary>
internal enum AssetType {

    /// <summary>
    /// Ações brasileiras (ações listadas na B3).
    /// </summary>
    BRA_STOCK = 561,

    /// <summary>
    /// Fundos Imobiliários (FIIs) brasileiros.
    /// </summary>
    BRA_FII = 962,

    /// <summary>
    /// Brazilian Depositary Receipts (BDRs), que representam ativos estrangeiros negociados na B3.
    /// </summary>
    BRA_BDR = 341
}