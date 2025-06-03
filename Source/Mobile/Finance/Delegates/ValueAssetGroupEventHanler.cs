namespace Finance.Delegates;

/// <summary>
/// Delegate para manipulação de eventos relacionados a mudanças de um grupo de ativos com um valor mudado.
/// </summary>
/// <param name="name">Nome do grupo de ativos que sofreu alteração.</param>
/// <param name="oldValue">Um valor genérico antes da mudança.</param>
public delegate void ValueAssetGroupEventHanler(string name, int oldValue);