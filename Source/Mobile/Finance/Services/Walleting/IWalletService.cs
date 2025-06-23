using Finance.Enumerations;
using Finance.Models;

namespace Finance.Services.Walleting;

/// <summary>
/// Define os métodos e propriedades essenciais para o gerenciamento de carteiras no aplicativo.
/// </summary>
internal interface IWalletService {

    #region Properties

    /// <summary>
    /// Obtém a carteira atualmente selecionada e em uso no contexto do aplicativo.
    /// </summary>
    Wallet Wallet { get; }

    #endregion Properties

    #region Wallet Management Methods

    /// <summary>
    /// Verifica se existe alguma carteira registrada localmente.
    /// </summary>
    /// <returns><c>true</c> se houver pelo menos uma carteira; caso contrário, <c>false</c>.</returns>
    Task<bool> Exists();

    /// <summary>
    /// Verifica se existe uma carteira com o nome especificado.
    /// </summary>
    /// <param name="name">Nome da carteira a ser verificada.</param>
    /// <returns><c>true</c> se uma carteira com o nome especificado existir; caso contrário, <c>false</c>.</returns>
    Task<bool> Exists(string name);

    /// <summary>
    /// Cria e armazena uma nova carteira no repositório local.
    /// </summary>
    /// <param name="wallet">Objeto da carteira a ser criada.</param>
    /// <returns>O identificador único gerado para a nova carteira.</returns>
    Task<int> Create(Wallet wallet);

    /// <summary>
    /// Remove uma carteira do repositório local.
    /// </summary>
    /// <param name="wallet">Carteira que será excluída.</param>
    Task Delete(Wallet wallet);

    /// <summary>
    /// Define a carteira atual que será utilizada nas operações subsequentes.
    /// </summary>
    /// <param name="wallet">Carteira a ser definida como ativa.</param>
    void SetWallet(Wallet wallet);

    /// <summary>
    /// Retorna a lista de carteiras disponíveis localmente.
    /// </summary>
    /// <returns>Lista de objetos <see cref="Wallet"/> disponíveis.</returns>
    Task<List<Wallet>> AvailableWallets();

    #endregion Wallet Management Methods

    #region Strategy Methods

    /// <summary>
    /// Adiciona um novo grupo de ativos à estratégia da carteira, caso ainda não exista.
    /// </summary>
    /// <param name="assetGroupName">Nome do grupo de ativos a ser adicionado.</param>
    /// <returns>
    /// Uma tarefa que representa a operação assíncrona.
    /// O resultado será <c>true</c> se o grupo for adicionado com sucesso; <c>falso</c> se já existir um grupo com o mesmo nome.
    /// </returns>
    Task<bool> AddAssetGroup(string assetGroupName);

    /// <summary>
    /// Atualiza os grupos de ativos na estratégia.
    /// </summary>
    /// <param name="strategy">Lista de grupos de ativos.</param>
    /// <returns>Uma tarefa que representa a operação assíncrona de salvar a estratégia.</returns>
    Task<bool> UpdateStrategy(List<AssetGroup> strategy);

    #endregion Strategy Methods

    #region Notification Methods

    /// <summary>
    /// Adiciona uma notificação no sistema de notificações do aplicativo.
    /// </summary>
    /// <param name="notification">A notificação que será adicioinada.</param>
    /// <param name="key">A chave de identificação da notificação. O valor é <c>null</c> por padrão.</param>
    /// <returns>Uma tarefa que representa a operação assíncrona de adicionar uma notificação.</returns>
    Task<bool> AddNotification(NotificationCodes notification, string key = null);

    /// <summary>
    /// Remove uma notificação no sistema de notificações do aplicativo.
    /// </summary>
    /// <param name="notification">A notificação que será removida.</param>
    /// <param name="key">A chave de identificação da notificação. O valor é <c>null</c> por padrão.</param>
    /// <returns>Uma tarefa que representa a operação assíncrona de remover uma notificação.</returns>
    Task<bool> RemoveNotification(NotificationCodes notification, string key = null);

    #endregion Notification Methods
}