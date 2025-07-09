namespace Finance.Enumerations;

/// <summary>
/// Códigos para o sistema de notificação do Finance.
/// </summary>
public enum NotificationCodes {

    #region Strategy

    /// <summary>
    /// Indica que a estratégia da carteira não existe.
    /// </summary>
    STRATEGY_EMPTY = 132,

    /// <summary>
    /// Indica que a porcentagem da alocação da estratégia não foi completamente definida.
    /// </summary>
    STRATEGY_PERCENTAGE_NOT_DEFINED = 754,

    /// <summary>
    /// Indica que o grupo de ativos da estratégia está vazio, ou seja, não há ativos alocados nesse grupo.
    /// </summary>
    STRATEGY_GROUP_EMPTY = 217,

    /// <summary>
    /// Indica que a porcentagem dentro de um Grupo de Ativos não foi completamente definida.
    /// </summary>
    STRATEGY_GROUP_PERCENTAGE_NOT_DEFINED = 234,

    #endregion Strategy
}