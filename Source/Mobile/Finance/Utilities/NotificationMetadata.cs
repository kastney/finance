using Finance.Enumerations;
using Finance.Models;

namespace Finance.Utilities;

/// <summary>
/// Fornece métodos utilitários relacionados a notificações, incluindo a serialização e
/// desserialização de listas de notificações.
/// </summary>
internal static class NotificationMetadata {

    #region Properties

    /// <summary>
    /// Configurações do serializador JSON, incluindo a opção de ignorar diferenças entre
    /// maiúsculas e minúsculas nos nomes das propriedades durante a serialização e desserialização.
    /// </summary>
    private static JsonSerializerOptions JsonOptions { get; } = new() {
        // Configuração para tratar os nomes das propriedades de forma case-insensitive.
        PropertyNameCaseInsensitive = true
    };

    /// <summary>
    /// Retorna um dicionário com os metadados de cada tipo de notificações, incluindo título, descrição e ícone.
    /// Esses dados são utilizados para exibição amigável na interface do usuário.
    /// </summary>
    public static Dictionary<NotificationCodes, Notification> Data => new() {
        {
            // Notificação para estratégia vazia.
            NotificationCodes.STRATEGY_EMPTY,
            new Notification {
                Icon = "\uf200",
                Title = "Estratégia não definida",
                Description = "Ainda não há nenhum Grupo de Ativos configurado nesta carteira!",
                Tag = "Estratégia",
                Level = NotificationLevel.Warning,
                Route = "strategy"
            }
        },
        {
            NotificationCodes.STRATEGY_PERCENTAGE_NOT_DEFINED,
            new Notification {
                Icon = "\uf200",
                Title = "Estratégia sem alocação definida",
                Description = "Parte da carteira permanece não alocada entre os Grupos de Ativos!",
                Tag = "Estratégia",
                Level = NotificationLevel.Warning,
                Route = "strategy"
            }
        }
    };

    #endregion Properties

    #region Notification Methods

    /// <summary>
    /// Retorna uma lista padrão de notificações.
    /// Atualmente, retorna uma lista vazia.
    /// Pode ser ajustado futuramente para incluir notificações padrão da aplicação.
    /// </summary>
    /// <returns>Uma lista de <see cref="Notification"/> com os itens padrão.</returns>
    public static List<Notification> GetDefaultNotifications() => [];

    /// <summary>
    /// Converte uma string JSON em uma lista de objetos <see cref="Notification"/>.
    /// Caso o JSON seja nulo, vazio, inválido ou ocorra erro durante a desserialização,
    /// retorna a lista padrão de notificações.
    /// </summary>
    /// <param name="json">String JSON contendo a lista de notificações.</param>
    /// <returns>Uma lista de <see cref="Notification"/> desserializada ou a lista padrão em caso de erro.</returns>
    public static List<Notification> DeserializeNotification(string json) {
        try {
            // Verifica se o JSON está vazio ou nulo, retornando a lista padrão nesses casos. Caso contrário, tenta desserializar o JSON. Se o resultado for nulo, também retorna a lista padrão.
            return string.IsNullOrWhiteSpace(json) ? GetDefaultNotifications() : JsonSerializer.Deserialize<List<Notification>>(json, JsonOptions) ?? GetDefaultNotifications();
        } catch {
            // Em caso de exceção na desserialização, retorna a lista padrão.
            return GetDefaultNotifications();
        }
    }

    /// <summary>
    /// Serializa uma lista de objetos <see cref="Notification"/> para uma string JSON,
    /// utilizando as configurações definidas em <see cref="JsonOptions"/>.
    /// </summary>
    /// <param name="notifications">Lista de notificações a ser serializada.</param>
    /// <returns>String JSON representando a lista de notificações.</returns>
    public static string SerializeStrategy(List<Notification> notifications) {
        // Serializa a lista de notificações para JSON.
        return JsonSerializer.Serialize(notifications, JsonOptions);
    }

    /// <summary>
    /// Verifica se existe alguma notificação com rota igual ou que esteja em sub-rota da rota informada.
    /// </summary>
    /// <param name="notifications">Lista de notificações a ser verificada.</param>
    /// <param name="route">Rota base para comparação, como "strategy" ou "strategy/group".</param>
    /// <returns>True se existir uma notificação que tenha a mesma rota ou sub-rota da rota informada.</returns>
    public static bool HasNotificationForRoute(this IEnumerable<Notification> notifications, string route) {
        // Verifica se existe rota para ser verificada.
        if(string.IsNullOrWhiteSpace(route)) { return false; }
        // Procura se existe alguma notificação que contém a rota enviada.
        return notifications.Any(n => !string.IsNullOrEmpty(n.Route) && (n.Route.Equals(route, StringComparison.OrdinalIgnoreCase) || n.Route.StartsWith(route + "/", StringComparison.OrdinalIgnoreCase)));
    }

    #endregion Notification Methods
}