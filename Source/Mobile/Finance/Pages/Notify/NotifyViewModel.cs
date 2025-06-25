using Finance.Enumerations;
using Finance.Models;
using Finance.Utilities;

namespace Finance.Pages.Notify;

/// <summary>
/// ViewModel responsável pela lógica de exibição das notificações.
/// </summary>
internal partial class NotifyViewModel : ViewModel {

    #region Fields

    /// <summary>
    /// Flag que indica se a lista de notificações está vazia.
    /// </summary>
    [ObservableProperty]
    private bool isEmpty;

    #endregion Fields

    #region Properties

    /// <summary>
    /// Lista de notificações da carteira.
    /// </summary>
    public ObservableCollection<Notification> Notifications { get; set; }

    #endregion Properties

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

    #region Navigation Methods

    /// <summary>
    /// Vai para o sertor ou área do aplicativo reverente à notificação.
    /// </summary>
    /// <param name="id">O códeigo de identificação da notificação.</param>
    /// <param name="key">A chave da notificação.</param>
    /// <returns>Uma tarefa assíncrona que representa a navegação para a parte ou seção do aplicativo.</returns>
    public async Task NavigateTo(NotificationCodes id, string key) {
        // Impede execução simultânea do comando.
        if(!IsRunning) {
            // Sinaliza que a execução está em andamento.
            IsRunning = true;

            // Obtém a notificação correspondente.
            if(Notifications.FirstOrDefault(n => n.Id == id && n.Key == key) is Notification notification) {
                // Prepara a rota.
                notification.Route = notification.Route.Replace("{key}", key);
                // Volta até a raiz da pilha de navegação.
                await navigationService.NavigateToBack();
                // Navega para a rota.
                await navigationService.NavigateTo(notification.Route);
            }

            // Espera um tempo para a navegação fluida;
            await Task.Delay(100);

            // Finaliza a execução do comando.
            IsRunning = false;
        }
    }

    #endregion Navigation Methods
}