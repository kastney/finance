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
    /// A cor do grupo de ativos.
    /// </summary>
    [ObservableProperty]
    private int groupColor;

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

    /// <summary>
    /// A quantidade de porcentagem disponível para ser utilizado entre os grupos de ativos.
    /// </summary>
    [ObservableProperty]
    private int percentageAvailable;

    /// <summary>
    /// Coleção de dados de pizza que representa a estratégia de alocação de ativos.
    /// </summary>
    [ObservableProperty]
    private List<PieData> assetGroupDataSeries;

    /// <summary>
    /// Flag que indica se a mensage de aviso é visível ou não.
    /// </summary>
    [ObservableProperty]
    private bool hasWarning;

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
        // Obtém a cor do grupo de ativos.
        GroupColor = group.Color;

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
        UpdateProperties();
    }

    /// <summary>
    /// Atualiza as propriedades vinculáveis da página.
    /// </summary>
    private void UpdateProperties() {
        // Verifica se é permitido a criação de um novo Grupo de Ativos.
        HasNewAsset = AvailableAssets.Count != 0;

        // Obtém a quantidade de porcentagem disponível para os grupos.
        PercentageAvailable = 100 - Assets.Sum(a => a.Percentage);

        // Inicializa a lista que representará os dados do gráfico de pizza (PieChart).
        var seriesData = new List<PieData>();

        // Percorre todos os tipos de ativos existentes no grupo.
        foreach(var asset in Assets) {
            // Atualiza a porcentagem disponível em cada grupo de ativos.
            asset.PercentageAvailable = PercentageAvailable;
            // Atualiza a cor do grupo de ativos.
            asset.GroupColor = GroupColor;
            // Só adiciona os grupos que possuem porcentagem diferente de zero.
            if(asset.Percentage != 0) {
                // Adiciona os dados do grupo à lista de dados do gráfico.
                seriesData.Add(new PieData(asset.Meta.Title, asset.Percentage, asset.Color));
            }
        }

        // Atualiza a propriedade responsável por fornecer os dados ao gráfico de alocação.
        AssetGroupDataSeries = seriesData;

        // Esconde a barra de pesquisa.
        IsExpanded = false;

        // Verifica se a estratégia da carteira possui mais de um grupo.
        IsAllowDragDropItems = Assets.Count > 1;

        // Verifica se o grupo de ativos está vazio.
        IsEmpty = Assets.Count == 0;

        // Mostra ou não a mensagem de aviso ao usuário.
        HasWarning = Assets.Count != 0 && PercentageAvailable != 0;
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

            // Número total de cores disponíveis na aplicação.
            int totalColors = ColorUtility.GetColors().Count;
            // Lista de todos os índices de cores disponíveis: 0, 1, ..., N-1
            var allColorIndices = Enumerable.Range(0, totalColors).ToList();
            // Obtém todas as cores já utilizadas nos tipos de ativos existentes.
            var usedColors = Assets.Select(g => g.Color).ToList();
            // Busca a primeira cor que ainda não foi utilizada.
            var nextAvailableColor = allColorIndices.Except(usedColors).FirstOrDefault();

            // Cria uma nova alocação de ativo com o tipo especificado.
            var newAsset = new AssetAllocation { Type = asset.Type, Color = nextAvailableColor, Enabled = true, Percentage = 0 };
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
            UpdateProperties();
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
        UpdateProperties();
    }

    /// <summary>
    /// Atualiza o estado de habilitação de um tipo de ativo no serviço de carteiras.
    /// </summary>
    /// <param name="name">Nome do tipo de ativo que sofreu a mudança.</param>
    /// <param name="oldPercentage">O valor da porcentagem antes da mudança.</param>
    /// <returns>Uma tarefa assíncrona que representa a operação de salvar o status de um tipo de ativo.</returns>
    public async Task UpdateChecked(string name, int oldPercentage) {
        // Obtém o grupo de ativos local.
        var group = walletService.Wallet.Strategy.FirstOrDefault(g => g.Name.Equals(GroupName));
        // Atualiza as informações dos ativos.
        group.Assets = [.. Assets];

        // Atualiza o estado de habilitação do tipo de ativo no serviço de carteiras.
        if(!await walletService.UpdateStrategy(walletService.Wallet.Strategy)) {
            // O tipo de ativo atual.
            var asset = group.Assets.FirstOrDefault(a => a.Type == Enum.Parse<AssetType>(name));

            // Informa ao usuário que ocorreu um erro ao modificar se o grupo de ativos está ativo ou não.
            await Shell.Current.DisplayAlert("Não foi possível alterar o status de ativação!", $"Ocorreu um erro ao tentar salvar o novo status de ativação do tipo de ativos \"{asset.Meta.Title}\".", "OK");

            // Inverte o estado de habilitação do tipo de ativo.
            asset.Enabled = !asset.Enabled;
            // Restaura o valor da porcentagem antiga.
            asset.Percentage = oldPercentage;
        }

        // Atualiza as propriedades da página.
        UpdateProperties();
    }

    /// <summary>
    /// Atualiza a porcentagem de um tipo de ativo no serviço de carteiras.
    /// </summary>
    /// <param name="name">Nome do tipo de ativo que sofreu a mudança.</param>
    /// <param name="oldPercentage">O valor da porcentagem antes da mudança.</param>
    /// <returns>Uma tarefa assíncrona que representa a operação de salvar a porcentagem de um tipo de ativo.</returns>
    public async Task UpdatePercentage(string name, int oldPercentage) {
        // Obtém o grupo de ativos local.
        var group = walletService.Wallet.Strategy.FirstOrDefault(g => g.Name.Equals(GroupName));
        // Atualiza as informações dos ativos.
        group.Assets = [.. Assets];

        // Atualiza a porcentage do tipo de ativo no serviço de carteiras.
        if(!await walletService.UpdateStrategy(walletService.Wallet.Strategy)) {
            // O tipo de ativo atual.
            var asset = group.Assets.FirstOrDefault(a => a.Type == Enum.Parse<AssetType>(name));

            // Informa ao usuário que ocorreu um erro ao modificar a porcentagem do tipo de ativo.
            await Shell.Current.DisplayAlert("Não foi possível alterar a porcentagem!", $"Ocorreu um erro ao tentar salvar a nova porcentagem para o tipo de ativo \"{asset.Meta.Title}\".", "OK");

            // Atualiza o valor da porcentagem antiga.
            asset.Percentage = oldPercentage;
        }

        // Atualiza as propriedades da página.
        UpdateProperties();
    }

    /// <summary>
    /// Atualiza a cor de um tipo de ativo no serviço de carteiras.
    /// </summary>
    /// <param name="name">Nome do tipo de ativo que sofreu a mudança.</param>
    /// <param name="oldColor">O valor da cor antes da mudança.</param>
    /// <returns>Uma tarefa assíncrona que representa a operação de salvar a cor de um tipo de ativo.</returns>
    public async Task UpdateColor(string name, int oldColor) {
        // Obtém o tipo de ativo que sofreu a modificação.
        if(!Enum.TryParse(name, out AssetType type)) { return; }
        // O tipo de ativo atual.
        var asset = Assets.FirstOrDefault(a => a.Type == type);
        // Obtém o tipo de ativo se já estiver usando a cor.
        var existingAsset = Assets.FirstOrDefault(g => g.Type != type && g.Color == asset.Color);

        // Variável para saber se o usuário quer trocar as cores entre os tipos de ativos.
        var swap = false;
        // Variável auxiliar para armazenar a cor atual do tipo de ativo existente antes da troca.
        var currentColor = 0;

        try {
            // Verifica se já existe um tipo de ativo com a cor selecionada.
            if(existingAsset is not null) {
                // Pergunta ao usuário se quer trocar as cores.
                swap = await Shell.Current.DisplayAlert("Cor já em uso!", $"A cor selecionada já está sendo usada pelo tipo de ativo \"{existingAsset.Meta.Title}\".\n\nDeseja trocar as cores entre os tipos de ativos?", "Sim", "Não");

                // Verifica se o usuário quer trocar as cores entre os tipos de ativos.
                if(swap) {
                    // Armazena a cor atual do tipo de ativo existente para possibilitar o rollback, se necessário.
                    currentColor = existingAsset.Color;
                    // Troca as cores entre o tipo de ativo local e o tipo de ativo existente utilizando desestruturação.
                    existingAsset.Color = oldColor;
                } else {
                    // Força uma exceção para realizar o rollback caso o usuário não queira trocar as cores.
                    throw new Exception();
                }
            }

            // Obtém o grupo de ativos local.
            var group = walletService.Wallet.Strategy.FirstOrDefault(g => g.Name.Equals(GroupName));
            // Atualiza as informações dos ativos.
            group.Assets = [.. Assets];

            // Tenta atualizar a estratégia no serviço de carteiras.
            if(!await walletService.UpdateStrategy(walletService.Wallet.Strategy)) {
                // Indica que ocorreu algum erro ao salvar no banco de dados.
                throw new Exception();
            }

            // Atualiza as propriedades da página.
            UpdateProperties();
        } catch {
            // Aguarda um tempo para permitir que a interface reflita as alterações.
            await Task.Delay(100);

            // Rollback: restaura o valor da cor antiga no tipo de ativo local.
            asset.Color = oldColor;

            // Se houve troca de cores, desfaz a troca restaurando a cor original do tipo de ativo existente.
            if(swap && existingAsset is not null) {
                // Informa ao usuário que ocorreu um erro ao modificar a cor do tipo de ativo.
                await Shell.Current.DisplayAlert("Não foi possível alterar a cor!", $"Ocorreu um erro ao tentar salvar a nova cor para o para o tipo de ativo \"{asset.Meta.Title}\".", "OK");

                // Rollback: restaura a cor do tipo de ativo existente para seu valor original.
                existingAsset.Color = currentColor;
            }
        }
    }

    /// <summary>
    /// Realiza a remoção de um tipo de ativo no grupo.
    /// </summary>
    /// <param name="name">O nome do tipo de ativo que será renomeado.</param>
    /// <returns>Uma tarefa que representa a operação assíncrona.</returns>
    public async Task Delete(string name) {
        // Impede execução simultânea do comando.
        if(!IsRunning) {
            // Sinaliza que a execução está em andamento.
            IsRunning = true;

            // Obtém o tipo de ativo que sofreu a modificação.
            var type = Enum.Parse<AssetType>(name);

            // Variável auxiliar para saber o índice do tipo de ativo na lista.
            int index;
            // Interage cada item da lista.
            for(index = 0; index < Assets.Count; index++) {
                // Busca o tipo de ativo informado no parâmetro.
                if(Assets[index].Type == type) {
                    // Finaliza o loop ao encontrar o tipo de ativo.
                    break;
                }
            }

            // Obtém o typo de ativo local.
            var asset = Assets[index];
            // Removendo o tipo de ativo da lista.
            Assets.RemoveAt(index);

            // Obtém o grupo de ativos local.
            var group = walletService.Wallet.Strategy.FirstOrDefault(g => g.Name.Equals(GroupName));
            // Atualiza as informações dos ativos.
            group.Assets = [.. Assets];

            // Adiciona o tipo de ativo da lista de ativos disponíveis para adição.
            AddAvailableAsset(type);

            // Tenta atualizar a estratégia no serviço de carteiras.
            if(!await walletService.UpdateStrategy(walletService.Wallet.Strategy)) {
                // Informa ao usuário que ocorreu um erro ao remover o tipo de ativo.
                await Shell.Current.DisplayAlert("Não foi possível deletar!", $"Ocorreu um erro ao tentar deletar o tipo de ativo \"{asset.Meta.Title}\".", "OK");

                // Rollback: Adicionando o tipo de ativo novamente na lista.
                Assets.Insert(index, asset);
                // Atualiza as informações dos ativos.
                group.Assets = [.. Assets];
                // Remove o ativo da lista de ativos disponíveis para adição, evitando duplicações.
                RemoveAvailableAsset(type);
            }

            // Atualiza as propriedades da página.
            UpdateProperties();

            // Pequeno atraso para garantir estabilidade de navegação.
            await Task.Delay(100);

            // Finaliza a execução do comando.
            IsRunning = false;
        }
    }

    #endregion Asset Methods

    #region Private Methods

    /// <summary>
    /// Procura por um grupo que já contenha o tipo de ativo informado.
    /// </summary>
    /// <param name="assetType">Tipo de ativo a localizar.</param>
    /// <returns>Tupla contendo o grupo e a alocação encontrada, ou (null, null) se não encontrado.</returns>
    private (AssetGroup group, AssetAllocation allocation) FindExistingAssetGroup(AssetType assetType) {
        // Percorre todos os grupos de ativos da carteira.
        foreach(var group in walletService.Wallet.Strategy) {
            // Percorre todas as alocações de ativos dentro do grupo atual.
            foreach(var allocation in group.Assets) {
                // Verifica se o tipo de ativo da alocação é igual ao tipo procurado.
                if(allocation.Type == assetType) {
                    // Retorna o grupo e a alocação encontrados.
                    return (group, allocation);
                }
            }
        }
        // Caso o tipo de ativo não seja encontrado em nenhum grupo, retorna nulo para ambos.
        return (null, null);
    }

    /// <summary>
    /// Remove o ativo informado da lista de ativos disponíveis e o adiciona à lista de ativos indisponíveis.
    /// </summary>
    /// <param name="type">Tipo de ativo que será removido da lista de disponíveis.</param>
    private void RemoveAvailableAsset(AssetType type) {
        // Procura o ativo na lista de ativos disponíveis.
        if(AvailableAssets.FirstOrDefault(a => a.Type == type) is AvailableAsset item) {
            // Remove o ativo da lista de disponíveis.
            AvailableAssets.Remove(item);
            // Adiciona o ativo na lista de ativos atualmente indisponíveis.
            UnavailableAsset.Add(item);
        }
    }

    /// <summary>
    /// Adiciona o ativo informado de volta à lista de ativos disponíveis, removendo-o da lista de indisponíveis.
    /// </summary>
    /// <param name="type">Tipo de ativo que será adicionado de volta à lista de disponíveis.</param>
    private void AddAvailableAsset(AssetType type) {
        // Procura o ativo na lista de ativos indisponíveis.
        if(UnavailableAsset.FirstOrDefault(a => a.Type == type) is AvailableAsset item) {
            // Remove o ativo da lista de indisponíveis.
            UnavailableAsset.Remove(item);
            // Adiciona o ativo na lista de disponíveis.
            AvailableAssets.Add(item);
        }
    }

    #endregion Private Methods
}