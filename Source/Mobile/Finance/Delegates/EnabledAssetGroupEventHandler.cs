namespace Finance.Delegates;

/// <summary>
/// Delegate para manipulação de eventos relacionados a mudanças do status de ativado de um grupo de ativos.
/// </summary>
/// <param name="name">Nome do grupo de ativos que sofreu alteração.</param>
public delegate void EnabledAssetGroupEventHandler(string name);