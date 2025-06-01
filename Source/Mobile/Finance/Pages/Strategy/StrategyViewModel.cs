using Finance.Models;
using Finance.Utilities;

namespace Finance.Pages.Strategy;

/// <summary>
/// ViewModel responsável pela lógica de exibição e interação da página de estratégia.
/// </summary>
internal partial class StrategyViewModel : ViewModel {

    #region Fields

    /// <summary>
    /// Lista de grupos de ativos que compõem a estratégia da carteira.
    /// </summary>
    public ObservableCollection<AssetGroup> Strategy { get; set; }

    /// <summary>
    /// Flag que indica se a estratégia da carteira está vazia.
    /// </summary>
    [ObservableProperty]
    private bool isEmpty;

    /// <summary>
    /// Flag que indica se é permitido a criação de um novo Grupo de Ativos.
    /// </summary>
    [ObservableProperty]
    private bool hasNewAssetGroup;

    /// <summary>
    /// Flag que indica se é permitido arrastar e soltar itens na interface.
    /// </summary>
    [ObservableProperty]
    private bool isAllowDragDropItems;

    #endregion Fields

    #region Constructor

    /// <summary>
    /// Inicializa uma nova instância do ViewModel de estratégia.
    /// </summary>
    public StrategyViewModel() {
        // Inicializa a coleção de grupos de ativos.
        Strategy = [];
    }

    #endregion Constructor

    #region Update Methods

    /// <summary>
    /// Atualiza o estado do ViewModel, refletindo as alterações na carteira e na estratégia.
    /// </summary>
    public override void Update() {
        // Obtém a instância do serviço de carteiras.
        var wallet = walletService.Wallet;
        // Verifica se é permitido a criação de um novo Grupo de Ativos.
        HasNewAssetGroup = wallet.Strategy.Count < AssetMetadata.Data.Count;

        // Limpa a coleção de grupos de ativos.
        Strategy.Clear();
        // Percorre cada grupo de ativos na carteira e adiciona à coleção.
        foreach(var strategy in wallet.Strategy) {
            // Adiciona cada grupo de ativos à coleção.
            Strategy.Add(strategy);
        }

        // Verifica se a estratégia da carteira possui mais de um grupo.
        IsAllowDragDropItems = wallet.Strategy.Count > 1;
        // Verifica se a estratégia da carteira está vazia.
        IsEmpty = wallet.Strategy.Count == 0;
    }

    #endregion Update Methods

    #region Navigation Methods

    /// <summary>
    /// Navega para a página de criação de um Grupo de Ativos.
    /// </summary>
    /// <returns>Uma tarefa assíncrona que representa a operação de navegação.</returns>
    [RelayCommand]
    private async Task NewAssetGroup() {
        // Impede execução simultânea do comando.
        if(!IsRunning) {
            // Sinaliza que a execução está em andamento.
            IsRunning = true;

            // Navega até a página de estratégia da carteira.
            await navigationService.NavigateTo("add");
            // Pequeno atraso para garantir estabilidade de navegação.
            await Task.Delay(100);

            // Finaliza a execução do comando.
            IsRunning = false;
        }
    }

    #endregion Navigation Methods

    #region Walleting Methods

    /// <summary>
    /// Salva a ordenação atual da estratégia da carteira no serviço de carteiras.
    /// </summary>
    /// <returns>Uma tarefa assíncrona que representa a operação de salvar a ordenação.</returns>
    public async Task SaveSorting() {
        // Salva a ordenação atual da estratégia no serviço de carteiras.
        await walletService.UpdateStrategy([.. Strategy]);
    }

    /// <summary>
    /// Atualiza o estado de habilitação de um grupo de ativos no serviço de carteiras.
    /// </summary>
    /// <param name="name">Nome do grupo de ativos que sofreu a mudança.</param>
    /// <returns>Uma tarefa assíncrona que representa a operação de salvar o status de um grupo de ativos.</returns>
    public async Task UpdateAssetGroupChecked(string name) {
        // Atualiza o estado de habilitação do grupo de ativos no serviço de carteiras.
        if(!await walletService.UpdateStrategy([.. Strategy])) {
            // Aguara um tempo para atualizar na tela.
            await Task.Delay(100);
            // Obtém a carteira atual do serviço de carteiras.
            var wallet = walletService.Wallet;
            // Se a atualização falhar, inverte o estado do grupo de ativos localmente.
            if(wallet.Strategy.FirstOrDefault(g => g.Name == name) is AssetGroup group) {
                // Inverte o estado de habilitação do grupo de ativos.
                group.Enabled = !group.Enabled;
                // Atualiza a interface.
                Update();
            }
        }
    }

    #endregion Walleting Methods
}