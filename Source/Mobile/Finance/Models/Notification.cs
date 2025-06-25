using Finance.Enumerations;
using Finance.Utilities;

namespace Finance.Models;

/// <summary>
/// Representa uma notificação interna da aplicação, contendo um código identificador
/// e, opcionalmente, uma chave de referência para informações adicionais.
/// </summary>
internal class Notification {

    #region Fields

    /// <summary>
    /// Variávei interna para guardar o código da notificação.
    /// </summary>
    private NotificationCodes id;

    #endregion Fields

    #region Properties

    /// <summary>
    /// Código identificador da notificação, definido a partir da enumeração <see cref="NotificationCodes"/>.
    /// Esse código determina o tipo ou motivo da notificação.
    /// </summary>
    public NotificationCodes Id {
        get => id;
        set {
            // Define o identificador da notificação.
            id = value;

            // Obtém as meta-informações da notificação.
            var notification = NotificationMetadata.Data[id];
            // Define o ícone da notificação.
            Icon = notification.Icon;
            // Define o título da notificação.
            Title = notification.Title;
            // Define a descrição da notificação.
            Description = notification.Description;
            // Define a tag de categoria da notificação.
            Tag = notification.Tag;
            // Define o nível de severidade da notificação.
            Level = notification.Level;
            // Define a rota para a parte ou seção do aplicativo.
            Route = notification.Route;
        }
    }

    /// <summary>
    /// Chave opcional que pode ser utilizada para identificar um contexto específico da notificação,
    /// como um identificador de recurso, usuário ou outra informação relacionada.
    /// Essa propriedade será ignorada na serialização JSON se estiver nula.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string Key { get; set; }

    /// <summary>
    /// O ícone da notificação.
    /// </summary>
    [JsonIgnore]
    public string Icon { get; set; }

    /// <summary>
    /// O título da notificação.
    /// </summary>
    [JsonIgnore]
    public string Title { get; set; }

    /// <summary>
    /// A descrição da notificação.
    /// </summary>
    [JsonIgnore]
    public string Description { get; set; }

    /// <summary>
    /// A tag de categoria da notificação.
    /// </summary>
    [JsonIgnore]
    public string Tag { get; set; }

    /// <summary>
    /// O nível de severidade da notificação.
    /// </summary>
    [JsonIgnore]
    public NotificationLevel Level { get; set; }

    /// <summary>
    /// A rota para a seção ou parte do aplicatio.
    /// </summary>
    [JsonIgnore]
    public string Route { get; set; }

    #endregion Properties
}