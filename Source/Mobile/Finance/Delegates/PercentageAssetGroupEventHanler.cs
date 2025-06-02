namespace Finance.Delegates;

/// <summary>
/// Delegate para manipulação de eventos relacionados a mudanças da porcentage de um grupo de ativos.
/// </summary>
/// <param name="name">Nome do grupo de ativos que sofreu alteração.</param>
/// <param name="oldPercentage">O valor da porcentagem antes da mudança.</param>
public delegate void PercentageAssetGroupEventHanler(string name, int oldPercentage);