using Finance.Models;

namespace Finance.Pages.Walleting;

/// <summary>
/// ViewModel responsável por controlar o fluxo de exclusão de uma carteira, incluindo estados visuais
/// e navegação após a operação.
/// </summary>
internal partial class DeleteWalletViewModel : ViewModel {

    #region Fields

    /// <summary>
    /// Indica se a interface deve estar habilitada (inverso de IsRunning). Útil para controle de UI.
    /// </summary>
    [ObservableProperty]
    private bool isRunningInverse;

    #endregion Fields

    #region Properties

    /// <summary>
    /// Representa a carteira atualmente selecionada que será deletada.
    /// </summary>
    public Wallet Wallet { get; set; }

    #endregion Properties

    #region Constructor

    /// <summary>
    /// Inicializa a instância do ViewModel definindo o estado inicial e carregando a carteira selecionada.
    /// </summary>
    public DeleteWalletViewModel() {
        // Habilita a interface por padrão.
        IsRunningInverse = true;
        // Define a carteira atual a ser deletada, obtida do serviço de carteiras.
        Wallet = walletService.Wallet;
    }

    #endregion Constructor

    #region Methods

    /// <summary>
    /// Comando responsável por executar a exclusão da carteira e realizar a navegação pós-exclusão.
    /// </summary>
    /// <returns>Task que representa a conclusão da operação assíncrona.</returns>
    [RelayCommand]
    private async Task Delete() {
        // Inicia o estado de carregamento (desabilita UI).
        IsRunning = true;
        // Desabilita a interação enquanto carrega.
        IsRunningInverse = false;

        // Executa a exclusão da carteira atual.
        await walletService.Delete(Wallet);

        // Aguarda brevemente para garantir transição suave.
        await Task.Delay(100);
        // Realiza navegação para a página de loading e inicia sua lógica.
        if(await navigationService.NavigateTo("///loading", false) is LoadingPage page) { page.Initialization(); }

        // Finaliza o estado de carregamento (habilita UI novamente).
        IsRunning = false;
        // Reabilita a interação do usuário.
        IsRunningInverse = true;
    }

    #endregion Methods
}