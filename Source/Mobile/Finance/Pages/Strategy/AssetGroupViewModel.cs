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
        // Inverte o valor da expensão da barra de pesquisa.
        IsExpanded = !IsExpanded;
    }


    public async Task AddAsset(AvailableAsset asset) {
        if(!IsRunning) {
            IsRunning = true;

            IsExpanded = false;
            await Task.Delay(300);

            Assets.Add(new AssetAllocation { Type = asset.Type });
            RemoveAvailableAsset(asset.Type);

            UpdateProperties();

            IsRunning = false;
        }
    }

    public async Task SaveSorting() {
        await Task.Delay(100);
    }

    #endregion Asset Methods

    #region Private Methods

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