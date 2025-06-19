namespace Finance.Delegates;

/// <summary>
/// Delegate para manipulação de eventos relacionados ao click de um grupo de ativos.
/// </summary>
/// <param name="name">Nome do grupo de ativos que sofreu alteração.</param>
public delegate void AssetGroupEventHanler(string name);