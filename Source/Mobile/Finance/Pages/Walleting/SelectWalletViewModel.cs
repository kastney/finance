using Finance.Models;
using Finance.Pages.Initialize;
using Finance.Utilities;

namespace Finance.Pages.Walleting;

/// <summary>
/// ViewModel responsável por gerenciar a lógica de seleção e criação de carteiras,
/// incluindo carregamento inicial, verificação de estado e navegação.
/// </summary>
internal partial class SelectWalletViewModel : ViewModel {

    #region Fields

    /// <summary>
    /// Representa a carteira atualmente selecionada pelo usuário na interface.
    /// </summary>
    [ObservableProperty]
    private Wallet selectedWallet;

    /// <summary>
    /// Indica se a lista de carteiras disponíveis está vazia, afetando a exibição de mensagens de aviso.
    /// </summary>
    [ObservableProperty]
    private bool isEmpty;

    #endregion Fields

    #region Properties

    /// <summary>
    /// Coleção de carteiras disponíveis para seleção. Alimentada no carregamento inicial da tela.
    /// </summary>
    public ObservableCollection<Wallet> Wallets { get; set; }

    #endregion Properties

    #region Constructor

    /// <summary>
    /// Inicializa uma nova instância do ViewModel de seleção de carteiras,
    /// carregando os dados disponíveis imediatamente.
    /// </summary>
    public SelectWalletViewModel() {
        // Inicializa a coleção de carteiras como vazia.
        Wallets = [];
        // Executa o carregamento das carteiras disponíveis.
        Loading();
    }

    #endregion Constructor

    #region Start Methods

    /// <summary>
    /// Carrega a lista de carteiras disponíveis para seleção,
    /// atualizando a interface conforme o conteúdo.
    /// </summary>
    private async void Loading() {
        // Recupera a lista de carteiras disponíveis via serviço.
        foreach(var wallet in await walletService.AvailableWallets()) {
            // Adiciona cada carteira à coleção observável.
            Wallets.Add(wallet);
        }

        // Define se a lista está vazia, para exibir ou ocultar mensagem apropriada.
        IsEmpty = Wallets.Count == 0;
    }

    #endregion Start Methods

    #region Navigation Methods

    /// <summary>
    /// Comando responsável por iniciar a navegação para a tela de criação de uma nova carteira.
    /// </summary>
    /// <returns>Tarefa assíncrona da operação de navegação.</returns>
    [RelayCommand]
    private async Task NewWallet() {
        // Impede múltiplas execuções simultâneas.
        if(!IsRunning) {
            // Ativa o estado de carregamento.
            IsRunning = true;

            // Navega para a página de criação de carteira.
            await navigationService.NavigateTo("create");
            // Aguarda brevemente para suavizar transição visual.
            await Task.Delay(100);

            // Desativa o estado de carregamento.
            IsRunning = false;
        }
    }

    /// <summary>
    /// Comando responsável por selecionar a carteira atual e navegar para a próxima etapa.
    /// </summary>
    /// <returns>Tarefa assíncrona da operação de seleção e navegação.</returns>
    [RelayCommand]
    private async Task SelectWallet() {
        // Impede múltiplas execuções simultâneas.
        if(!IsRunning) {
            // Ativa o estado de carregamento.
            IsRunning = true;

            // Define a carteira selecionada como a carteira atual no contexto da aplicação.
            walletService.SetWallet(SelectedWallet);
            // Limpa a seleção após o uso.
            SelectedWallet = null;

            // Aguarda brevemente antes da navegação.
            await Task.Delay(100);
            // Navega para a página de loading e aciona sua lógica de inicialização.
            if(await navigationService.NavigateTo("///loading", false) is LoadingPage page) { page.Initialization(); }

            // Desativa o estado de carregamento.
            IsRunning = false;
        }
    }

    #endregion Navigation Methods
}