using Finance.Models;
using Finance.Utilities;

namespace Finance.Pages.Notify;

/// <summary>
/// ViewModel responsável pela lógica de exibição das notificações.
/// </summary>
internal partial class NotifyViewModel : ViewModel {

    #region Fields

    /// <summary>
    /// Lista de notificações da carteira.
    /// </summary>
    public ObservableCollection<Notification> Notifications { get; set; }

    /// <summary>
    /// Flag que indica se a lista de notificações está vazia.
    /// </summary>
    [ObservableProperty]
    private bool isEmpty;

    #endregion Fields

    #region Constructor

    /// <summary>
    /// Inicializa uma nova instância do ViewModel de estratégia.
    /// </summary>
    public NotifyViewModel() {
        // Inicializa a coleção de notificações.
        Notifications = [];
    }

    #endregion Constructor

    #region Update Methods

    /// <summary>
    /// Atualiza o estado do ViewModel, refletindo as alterações na carteira e na estratégia.
    /// </summary>
    public override void Update() {
        // Obtém a instância do serviço de carteiras.
        var wallet = walletService.Wallet;

        // Limpa a lista de notificações da carteira.
        Notifications.Clear();
        // Percorre cada notificação da carteira.
        foreach(var notification in wallet.Notifications) {
            // Adiciona a notificação à coleção.
            Notifications.Add(notification);
        }

        // Verifica se a lista de notificações está vazia.
        IsEmpty = wallet.Notifications.Count == 0;
    }

    #endregion Update Methods
}