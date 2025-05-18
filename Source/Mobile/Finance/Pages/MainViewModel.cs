using Finance.Models;
using Finance.Utilities;

namespace Finance.Pages;

/// <summary>
/// ViewModel da página principal, responsável por fornecer os dados da carteira e comandos de navegação.
/// </summary>
internal partial class MainViewModel : ViewModel {

    #region Fields

    /// <summary>
    /// Representa a carteira atualmente selecionada, contendo dados como estratégia e alocações.
    /// </summary>
    [ObservableProperty]
    private Wallet wallet;

    #endregion Fields

    #region Start Methods

    /// <summary>
    /// Inicia o carregamento dos dados da ViewModel, chamando o método de atualização.
    /// </summary>
    internal void Initialization() {
        // Atualiza os dados da carteira a partir do serviço associado.
        Update();
    }

    #endregion Start Methods

    #region Update Methods

    /// <summary>
    /// Atualiza a propriedade <see cref="Wallet"/> com os dados mais recentes do serviço.
    /// </summary>
    public override void Update() {
        // Obtém a carteira atual do serviço de carteiras.
        Wallet = walletService.Wallet;
        // Notifica a interface de que a propriedade Wallet foi alterada.
        OnPropertyChanged(nameof(Wallet));
    }

    #endregion Update Methods

    #region Navigation Methods

    /// <summary>
    /// Comando responsável por navegar até a tela de seleção de carteira.
    /// </summary>
    /// <returns>Uma tarefa assíncrona que representa a operação de navegação.</returns>
    [RelayCommand]
    private async Task SelectWallet() {
        // Impede execução simultânea do comando.
        if(!IsRunning) {
            // Sinaliza que a execução está em andamento.
            IsRunning = true;

            // Navega até a página de seleção de carteira.
            await navigationService.NavigateTo("select");
            // Pequeno atraso para evitar sobreposição de ações.
            await Task.Delay(100);

            // Finaliza a execução do comando.
            IsRunning = false;
        }
    }

    /// <summary>
    /// Comando responsável por navegar até a "zona de perigo", onde ações sensíveis são realizadas.
    /// </summary>
    /// <returns>Uma tarefa assíncrona que representa a operação de navegação.</returns>
    [RelayCommand]
    private async Task DangerZone() {
        // Impede execução simultânea do comando.
        if(!IsRunning) {
            // Sinaliza que a execução está em andamento.
            IsRunning = true;

            // Navega até a página "zona de perigo".
            await navigationService.NavigateTo("dangerZone");
            // Pequeno atraso para garantir estabilidade de navegação.
            await Task.Delay(100);

            // Finaliza a execução do comando.
            IsRunning = false;
        }
    }

    #endregion Navigation Methods
}