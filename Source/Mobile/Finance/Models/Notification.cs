using Finance.Enumerations;

namespace Finance.Models;

/// <summary>
/// Representa uma notificação interna da aplicação, contendo um código identificador
/// e, opcionalmente, uma chave de referência para informações adicionais.
/// </summary>
/// <param name="id">Código da notificação, representado pela enumeração <see cref="NotificationCodes"/>.</param>
internal class Notification(NotificationCodes id) {

    #region Properties

    /// <summary>
    /// Código identificador da notificação, definido a partir da enumeração <see cref="NotificationCodes"/>.
    /// Esse código determina o tipo ou motivo da notificação.
    /// </summary>
    public NotificationCodes Id { get; set; } = id;

    /// <summary>
    /// Chave opcional que pode ser utilizada para identificar um contexto específico da notificação,
    /// como um identificador de recurso, usuário ou outra informação relacionada.
    /// Essa propriedade será ignorada na serialização JSON se estiver nula.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string Key { get; set; }

    #endregion Properties
}