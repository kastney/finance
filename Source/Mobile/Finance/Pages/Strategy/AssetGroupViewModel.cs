using Finance.Enumerations;
using Finance.Models;
using Finance.Utilities;

namespace Finance.Pages.Strategy;

/// <summary>
/// ViewModel responsável pela lógica e estado da página de grupo de ativos.
/// </summary>
internal partial class AssetGroupViewModel : ViewModel {

    #region Fields

    /// <summary>
    /// Nome do grupo de ativos que será exibido ou editado na página.
    /// </summary>
    [ObservableProperty]
    private string groupName;

    /// <summary>
    /// Flag para saber se existe ativos na lista do grupo de ativos.
    /// </summary>
    [ObservableProperty]
    private bool isEmpty;

    /// <summary>
    /// Flag que indica se é permitido a adição de um novo tipo de Ativo.
    /// </summary>
    [ObservableProperty]
    private bool hasNewAsset;

    /// <summary>
    /// Indica se é permitido arrastar e soltar itens dentro da lista de ativos do grupo.
    /// </summary>
    [ObservableProperty]
    private bool isAllowDragDropItems;

    /// <summary>
    /// Indica se aparece ou não a barra de pesquisa.
    /// </summary>
    [ObservableProperty]
    private bool isExpanded;

    #endregion Fields

    #region Properties

    /// <summary>
    /// Coleção de ativos disponíveis para a seleção.
    /// </summary>
    public List<AvailableAsset> AvailableAssets { get; set; }

    /// <summary>
    /// Coleção de ativos indisponíveis para a seleção.
    /// </summary>
    private List<AvailableAsset> UnavailableAsset { get; set; }

    /// <summary>
    /// Lista de ativos que compõem o grupo de ativos.
    /// </summary>
    public ObservableCollection<AssetAllocation> Assets { get; set; }

    #endregion Properties

    #region Constructor

    /// <summary>
    /// Inicializa uma nova instância da ViewModel, configurando o nome do grupo de ativos.
    /// </summary>
    public AssetGroupViewModel() {
        // Define o nome do grupo de ativos como vazio por padrão.
        GroupName = string.Empty;
        // Inicializa a coleção de ativos disponíveis.
        AvailableAssets = [];
        // Inicializa a coleção de ativos indisponíveis.
        UnavailableAsset = [];
        // Inicializa a lista de ativos dentro do grupo de ativos.
        Assets = [];
    }

    #endregion Constructor

    #region Start Methods

    /// <summary>
    /// Atualiza a lista de ativos disponíveis para o usuário.
    /// </summary>
    public override void Update() {
        // Limpa a coleção de ativos disponíveis antes de adicionar novos.
        AvailableAssets.Clear();
        // Percorre todos os ativos disponíveis e os adiciona à coleção de ativos observáveis.
        foreach(var asset in AssetMetadata.GetAvailableAssets()) {
            // Obtém os ativos disponíveis utilizando a classe AssetMetadata.
            AvailableAssets.Add(asset);
        }

        // Obtém o grupo de ativos atual.
        var group = walletService.Wallet.Strategy.FirstOrDefault(a => a.Name.Equals(GroupName));

        // Limpa a lista de ativos.
        Assets.Clear();
        // Percorre todos os tipos de ativos existente dentro do grupo.
        foreach(var asset in group.Assets) {
            // Remove o ativo da lista de ativos disponíveis, pois ele já está alocado no grupo.
            RemoveAvailableAsset(asset.Type);
            // Adiciona o ativo na lista de ativos do grupo.
            Assets.Add(asset);
        }

        // Atualiza todas as propriedades vinculáveis da página.
        UpdateProperties(group);
    }

    /// <summary>
    /// Atualiza as propriedades vinculáveis da página.
    /// </summary>
    /// <param name="group">O grupo atual.</param>
    private void UpdateProperties(AssetGroup group = null) {
        // Define o grupo de ativos atual, se não for fornecido.
        group ??= walletService.Wallet.Strategy.FirstOrDefault(a => a.Name.Equals(GroupName));

        // Verifica se é permitido a criação de um novo Grupo de Ativos.
        HasNewAsset = AvailableAssets.Count != 0;

        // Esconde a barra de pesquisa.
        IsExpanded = false;

        // Verifica se a estratégia da carteira possui mais de um grupo.
        IsAllowDragDropItems = group.Assets.Count > 1;

        // Verifica se o grupo de ativos está vazio.
        IsEmpty = group.Assets.Count == 0;
    }

    #endregion Start Methods

    #region Asset Methods

    /// <summary>
    /// Define o nome do grupo de ativos que será exibido ou editado na página.
    /// </summary>
    /// <param name="groupName">Nome do grupo de ativos a ser definido.</param>
    public void SetGroupName(string groupName) {
        // Define o nome do grupo de ativos e atualiza a propriedade.
        GroupName = groupName;
    }

    /// <summary>
    /// Abre ou fecha a barra de pesquisa para o tipo de ativos.
    /// </summary>
    [RelayCommand]
    private void Expanded() {
        // Impede execução simultânea do comando.
        if(!IsRunning) {
            // Sinaliza que a execução está em andamento.
            IsRunning = true;

            // Inverte o valor da expensão da barra de pesquisa.
            IsExpanded = !IsExpanded;

            // Finaliza a execução do comando.
            IsRunning = false;
        }
    }

    /// <summary>
    /// Adiciona um novo tipo de ativo ao grupo atual, garantindo que o tipo não esteja presente em outro grupo.
    /// Caso o tipo de ativo já exista em outro grupo, solicita ao usuário se deseja mover o ativo para o grupo atual.
    /// </summary>
    /// <param name="asset">Ativo disponível que será adicionado ao grupo atual.</param>
    /// <returns>Uma tarefa assíncrona que representa a execução do método.</returns>
    public async Task AddAsset(AvailableAsset asset) {
        // Verifica se o comando já está em execução para evitar sobreposição de chamadas simultâneas.
        if(IsRunning) { return; }

        // Sinaliza que o comando está em execução.
        IsRunning = true;

        try {
            // Indica que não há novo ativo visível na interface no momento.
            HasNewAsset = false;
            // Fecha a barra de pesquisa na UI relacionada.
            IsExpanded = false;
            // Aguarda a finalização de possíveis animações da interface antes de continuar.
            await Task.Delay(300);

            // Procura um grupo que já contenha o mesmo tipo de ativo que está sendo adicionado.
            var (existingGroup, existingAllocation) = FindExistingAssetGroup(asset.Type);
            // Verifica se o tipo de ativo já existe em outro grupo.
            if(existingAllocation is not null) {
                // Pergunta ao usuário se deseja mover o ativo do grupo existente para o grupo atual.
                if(!await Shell.Current.DisplayAlert("Tipo de ativo já utilizado em outro grupo!", $"O tipo de ativo \"{asset.Title}\" que você selecionou já está sendo utilizado no grupo \"{existingGroup.Name}\"!\n\nDeseja mover este tipo de ativo do grupo \"{existingGroup.Name}\" para o grupo \"{GroupName}\"?", "Sim", "Não")) {
                    // Caso o usuário escolha não mover o ativo, marca que há um novo ativo disponível e encerra a execução.
                    HasNewAsset = true;
                    // Finaliza o método de adição de um novo tipo de ativo no grupo.
                    return;
                }
                // Remove o ativo do grupo anterior, preparando para movê-lo para o grupo atual.
                existingGroup.Assets.Remove(existingAllocation);
            }

            // Cria uma nova alocação de ativo com o tipo especificado.
            var newAsset = new AssetAllocation { Type = asset.Type };
            // Localiza o grupo atual dentro da estratégia da carteira.
            var currentGroup = walletService.Wallet.Strategy.FirstOrDefault(g => g.Name.Equals(GroupName));

            // Adiciona o novo ativo ao grupo atual.
            currentGroup.Assets.Add(newAsset);
            // Também adiciona o ativo à coleção local usada pela interface.
            Assets.Add(newAsset);

            // Tenta salvar a estratégia atualizada na fonte de dados (API, serviço, etc.).
            if(!await walletService.UpdateStrategy(walletService.Wallet.Strategy)) {
                // Caso ocorra falha na atualização, exibe uma mensagem de erro ao usuário.
                await Shell.Current.DisplayAlert("Erro ao adicionar ativo", $"Não foi possível adicionar o tipo de ativo \"{asset.Title}\" no grupo \"{GroupName}\".", "OK");

                // Remove o novo ativo do grupo atual, desfazendo a tentativa de adição.
                currentGroup.Assets.Remove(newAsset);
                // Remove o ativo também da coleção local.
                Assets.Remove(newAsset);
                // Se o ativo foi movido de outro grupo, restaura-o ao grupo original.
                if(existingAllocation is not null && existingGroup is not null) {
                    // Remove a troca com o outro grupo.
                    existingGroup.Assets.Add(existingAllocation);
                }

                // Indica que há um novo ativo disponível na interface, provavelmente para que o usuário possa tentar novamente.
                HasNewAsset = true;
                // Encerra a execução após o tratamento do erro.
                return;
            }

            // Remove o ativo da lista de ativos disponíveis para adição, evitando duplicações.
            RemoveAvailableAsset(asset.Type);
            // Atualiza as propriedades do grupo, refletindo as mudanças na interface.
            UpdateProperties(currentGroup);
        } finally {
            // Libera o bloqueio de execução, permitindo novas execuções do comando.
            IsRunning = false;
        }
    }

    /// <summary>
    /// Salva a nova ordem dos ativos dentro do grupo atual na estratégia da carteira.
    /// </summary>
    /// <returns>Uma tarefa assíncrona que representa a execução da operação.</returns>
    public async Task SaveSorting() {
        // Obtém o grupo de ativos correspondente ao nome atual do grupo.
        var group = walletService.Wallet.Strategy.FirstOrDefault(a => a.Name.Equals(GroupName));

        // Atualiza a lista de ativos do grupo com a nova ordenação definida localmente.
        group.Assets = [.. Assets];
        // Persiste a estratégia atualizada (incluindo a nova ordenação) através do serviço de carteiras.   
        await walletService.UpdateStrategy(walletService.Wallet.Strategy);

        // Atualiza as propriedades locais e da interface para refletir a nova ordenação.
        UpdateProperties(group);
    }

    #endregion Asset Methods

    #region Private Methods

    /// <summary>
    /// Procura por um grupo que já contenha o tipo de ativo informado.
    /// </summary>
    /// <param name="assetType">Tipo de ativo a localizar.</param>
    /// <returns>Tupla contendo o grupo e a alocação encontrada, ou (null, null) se não encontrado.</returns>
    private (AssetGroup group, AssetAllocation allocation) FindExistingAssetGroup(AssetType assetType) {
        foreach(var group in walletService.Wallet.Strategy) {
            foreach(var allocation in group.Assets) {
                if(allocation.Type == assetType)
                    return (group, allocation);
            }
        }
        return (null, null);
    }

    private void RemoveAvailableAsset(AssetType type) {
        if(AvailableAssets.FirstOrDefault(a => a.Type == type) is AvailableAsset item) {
            AvailableAssets.Remove(item);
            UnavailableAsset.Add(item);
        }
    }

    private void AddAvailableAsset(AssetType type) {
        if(UnavailableAsset.FirstOrDefault(a => a.Type == type) is AvailableAsset item) {
            UnavailableAsset.Remove(item);
            AvailableAssets.Add(item);
        }
    }

    #endregion Private Methods
}