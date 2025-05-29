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

    /// <summary>
    /// Representa a estratégia de agrupamento de ativos definida para esta carteira.
    /// </summary>
    public ObservableCollection<AssetGroup> Strategy { get; set; }

    /// <summary>
    /// Indica se a estratégia da carteira está vazia ou não.
    /// </summary>
    [ObservableProperty]
    private bool isStrategy;

    /// <summary>
    /// Indica se existe algo que necessita de atenção no botão de estratégia.
    /// </summary>
    [ObservableProperty]
    private bool isWarningStrategy;

    #endregion Fields

    #region Constructor

    /// <summary>
    /// Inicializa a instância do ViewModel definindo o estado inicial da estratégia da carteira.
    /// </summary>
    public MainViewModel() {
        // Inicializa a coleção de estratégia como uma nova ObservableCollection.
        Strategy = [];
    }

    #endregion Constructor

    #region Update Methods

    /// <summary>
    /// Atualiza a propriedade <see cref="Wallet"/> com os dados mais recentes do serviço.
    /// </summary>
    public override void Update() {
        // Obtém a carteira atual do serviço de carteiras.
        Wallet = walletService.Wallet;

        // Limpa a coleção de estratégia para evitar duplicação de dados.
        Strategy.Clear();
        // Percorre cada estratégia na carteira e adiciona à coleção de estratégia.
        foreach(var strategy in Wallet.Strategy) {
            // Verifica se existe algo dentro do grupo de ativos.
            if(strategy.Assets.Count != 0) {
                // Adiciona a estratégia atual à coleção de estratégia.
                Strategy.Add(strategy);
            }
        }

        // Verifica se a estratégia da carteira está vazia.
        IsStrategy = Strategy.Count != 0;
        // Verifica se existe algo que precisa de atenção no botão de estratégia.
        IsWarningStrategy = Strategy.Count == 0 || Strategy.Any(a => a.Assets.Count == 0);

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
    private async Task NavigateToSelectWallet() {
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
    private async Task NavigateToDangerZone() {
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

    /// <summary>
    /// Comando responsável por navegar até a página de estratégia da carteira.
    /// </summary>
    /// <returns>Uma tarefa assíncrona que representa a operação de navegação.</returns>
    [RelayCommand]
    private async Task NavigateToStrategy() {
        // Impede execução simultânea do comando.
        if(!IsRunning) {
            // Sinaliza que a execução está em andamento.
            IsRunning = true;

            // Navega até a página de estratégia da carteira.
            await navigationService.NavigateTo("strategy");
            // Pequeno atraso para garantir estabilidade de navegação.
            await Task.Delay(100);

            // Finaliza a execução do comando.
            IsRunning = false;
        }
    }

    #endregion Navigation Methods
}