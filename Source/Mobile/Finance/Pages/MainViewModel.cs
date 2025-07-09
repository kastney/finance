using Finance.Enumerations;
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
    /// Indica se existe alguma notificação.
    /// </summary>
    [ObservableProperty]
    private bool isNotification;

    /// <summary>
    /// Indica se existe algo que necessita de atenção no botão de estratégia.
    /// </summary>
    [ObservableProperty]
    private bool isNotificationStrategy;

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

        // Atualiza a visualização dos ativos da estratégia da carteira.
        UpdateStrategy();

        // Verifica se a estratégia da carteira está vazia.
        IsStrategy = Strategy.Count != 0;
        // Verifica se existe algo que precisa de atenção no botão de estratégia.
        IsNotificationStrategy = Wallet.Notifications.HasNotificationForRoute("strategy");
        // Verifica se existe alguma notificação.
        IsNotification = Wallet.Notifications.Count != 0;

        // Notifica a interface de que a propriedade Wallet foi alterada.
        OnPropertyChanged(nameof(Wallet));
    }

    /// <summary>
    /// Atualiza a visualização dos ativos da estratégia da carteira.
    /// </summary>
    private void UpdateStrategy() {
        // Limpa a coleção de estratégias visíveis na interface para evitar duplicação de dados.
        Strategy.Clear();

        // Mantém controle dos tipos de ativos que já foram incluídos, para depois saber quais ainda faltam.
        var usedAssetTypes = new HashSet<AssetType>();

        // Percorre cada grupo de ativos (AssetGroup) presente na carteira.
        foreach(var group in Wallet.Strategy) {
            // Cria uma lista temporária para armazenar apenas os tipos de ativos (AssetAllocation) que devem ser exibidos.
            var filteredAssets = new List<AssetAllocation>();
            // Percorre cada tipo de ativo dentro do grupo atual.
            foreach(var asset in group.Assets) {
                // Verifica as regras de exibição:
                // - Se o tipo de ativo possui pelo menos um item na sua lista de dados (Data.Count > 0), ele deve ser exibido.
                // - Ou, se o grupo estiver habilitado e o tipo de ativo também estiver habilitado, ele deve ser exibido.
                if(asset.Data.Count > 0 || (group.Enabled && asset.Enabled)) {
                    // Define a cor do grupo de ativos.
                    asset.GroupColor = group.Color;
                    // Adiciona o tipo de ativo à lista de exibição do grupo.
                    filteredAssets.Add(asset);
                    // Marca esse tipo de ativo como utilizado.
                    usedAssetTypes.Add(asset.Type);
                }
            }
            // Se o grupo tiver pelo menos um tipo de ativo que atenda às regras de exibição:
            if(filteredAssets.Count > 0) {
                // Cria uma nova instância do grupo com os ativos filtrados.
                var filteredGroup = new AssetGroup {
                    // Define o nome do grupo com o nome original.
                    Name = group.Name,
                    // Mantém o status de habilitação original do grupo (embora, na prática, a exibição dependa da filtragem acima).
                    Enabled = group.Enabled,
                    // Associa ao grupo apenas os tipos de ativos que devem ser exibidos.
                    Assets = filteredAssets
                };
                // Adiciona o grupo filtrado à coleção de estratégias visíveis na interface.
                Strategy.Add(filteredGroup);
            }
        }

        // Após processar todos os grupos existentes, verifica se há tipos de ativos "soltos".
        var miscellaneousAssets = new List<AssetAllocation>();
        // Obtém a cor do grupo de ativos diversos.
        var miscellaneousColorIndex = ColorUtility.DefaultGruopColor();
        // Um índice para as cores dos ativos, para as cores do grupo de ativos diversos.
        int assetColorIndex = 0;
        // Percorre todas as alocações conhecidas da carteira.
        foreach(var allocation in Wallet.Allocations) {
            // Se o tipo de ativo ainda não foi usado e tem quantidade maior que zero:
            if(!usedAssetTypes.Contains(allocation.Key) && allocation.Value.Count > 0) {
                // Cria uma instância de AssetAllocation representando o tipo de ativo.
                var asset = new AssetAllocation { Type = allocation.Key, GroupColor = miscellaneousColorIndex, Color = assetColorIndex++ };
                // Define a carteira atual.
                asset.SetWallet(Wallet);
                // Adiciona o tipo de ativo na lista de diversos.
                miscellaneousAssets.Add(asset);
            }
        }
        // Se houver algum ativo solto, cria o grupo "Diversos".
        if(miscellaneousAssets.Count > 0) {
            var miscellaneousGroup = new AssetGroup {
                Name = "Diversos",
                Assets = miscellaneousAssets
            };
            // Adiciona o grupo "Diversos" ao final da lista de estratégias visíveis.
            Strategy.Add(miscellaneousGroup);
        }
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
    /// Comando responsável por navegar até a tela de notificações da carteira.
    /// </summary>
    /// <returns>Uma tarefa assíncrona que representa a operação de navegação.</returns>
    [RelayCommand]
    private async Task NavigateToNotification() {
        // Impede execução simultânea do comando.
        if(!IsRunning) {
            // Sinaliza que a execução está em andamento.
            IsRunning = true;

            // Navega até a página de seleção de carteira.
            await navigationService.NavigateTo("notify");
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