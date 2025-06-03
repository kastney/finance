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

    /// <summary>
    /// A quantidade de porcentagem disponível para ser utilizado entre os grupos de ativos.
    /// </summary>
    [ObservableProperty]
    private int percentageAvailable;

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

        // Limpa a coleção de grupos de ativos.
        Strategy.Clear();
        // Percorre cada grupo de ativos na carteira e adiciona à coleção.
        foreach(var strategy in wallet.Strategy) {
            // Adiciona cada grupo de ativos à coleção.
            Strategy.Add(strategy);
        }

        // Atualiza as propriedades da página.
        UpdateProperties(wallet);
    }

    /// <summary>
    /// Atualiza as propriedades da página.
    /// </summary>
    /// <param name="wallet">A carteira atual.</param>
    private void UpdateProperties(Wallet wallet) {
        // Verifica se é permitido a criação de um novo Grupo de Ativos.
        HasNewAssetGroup = wallet.Strategy.Count < AssetMetadata.Data.Count;

        // Obtém a quantidade de porcentagem disponível para os grupos.
        PercentageAvailable = 100 - wallet.Strategy.Sum(a => a.Percentage);

        // Percorrendo todos os grupos de ativos existentes.
        foreach(var group in Strategy) {
            // Atualiza a porcentagem disponível em cada grupo de ativos.
            group.PercentageAvailable = PercentageAvailable;
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
            await navigationService.NavigateTo("edit");
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
    /// <param name="oldPercentage">O valor da porcentagem antes da mudança.</param>
    /// <returns>Uma tarefa assíncrona que representa a operação de salvar o status de um grupo de ativos.</returns>
    public async Task UpdateChecked(string name, int oldPercentage) {
        // Atualiza o estado de habilitação do grupo de ativos no serviço de carteiras.
        if(!await walletService.UpdateStrategy([.. Strategy])) {
            // Obtém o grupo de ativos local.
            var group = Strategy.FirstOrDefault(g => g.Name.Equals(name));
            // Inverte o estado de habilitação do grupo de ativos.

            // Informa ao usuário que ocorreu um erro ao modificar se o grupo de ativos está ativo ou não.
            await Shell.Current.DisplayAlert("Não foi possível alterar o status de ativação!", $"Ocorreu um erro ao tentar salvar o novo status de ativação do grupo de ativos \"{group.Name}\".", "OK");

            group.Enabled = !group.Enabled;
            // Restaura o valor da porcentagem antiga.
            group.Percentage = oldPercentage;
        }

        // Atualiza as propriedades da página.
        UpdateProperties(walletService.Wallet);
    }

    /// <summary>
    /// Atualiza a porcentagem de um grupo de ativos no serviço de carteiras.
    /// </summary>
    /// <param name="name">Nome do grupo de ativos que sofreu a mudança.</param>
    /// <param name="oldPercentage">O valor da porcentagem antes da mudança.</param>
    /// <returns>Uma tarefa assíncrona que representa a operação de salvar a porcentagem de um grupo de ativos.</returns>
    public async Task UpdatePercentage(string name, int oldPercentage) {
        // Atualiza a porcentage do grupo de ativos no serviço de carteiras.
        if(!await walletService.UpdateStrategy([.. Strategy])) {
            // Obtém o grupo de ativos local.
            var group = Strategy.FirstOrDefault(g => g.Name.Equals(name));

            // Informa ao usuário que ocorreu um erro ao modificar a porcentagem do grupo de ativos.
            await Shell.Current.DisplayAlert("Não foi possível alterar a porcentagem!", $"Ocorreu um erro ao tentar salvar a nova porcentagem para o grupo de ativos \"{group.Name}\".", "OK");

            // Atualiza o valor da porcentagem antiga.
            group.Percentage = oldPercentage;
        }

        // Atualiza as propriedades da página.
        UpdateProperties(walletService.Wallet);
    }

    /// <summary>
    /// Atualiza a cor de um grupo de ativos no serviço de carteiras.
    /// </summary>
    /// <param name="name">Nome do grupo de ativos que sofreu a mudança.</param>
    /// <param name="oldColor">O valor da cor antes da mudança.</param>
    /// <returns>Uma tarefa assíncrona que representa a operação de salvar a cor de um grupo de ativos.</returns>
    public async Task UpdateColor(string name, int oldColor) {
        // Obtém o grupo de ativos local.
        var group = Strategy.FirstOrDefault(g => g.Name.Equals(name));
        // Obtém o grupo se já estiver usando essa cor.
        var existingGroup = Strategy.FirstOrDefault(g => !g.Name.Equals(name) && g.Color == group.Color);

        // Variável para saber se o usuário quer trocar as cores entre os grupos.
        bool swap = false;
        // Variável auxiliar para armazenar a cor atual do grupo existente antes da troca.
        int currentColor = 0;

        try {
            // Verifica se já existe um grupo de ativos com a cor selecionada.
            if(existingGroup is not null) {
                // Pergunta ao usuário se quer trocar as cores.
                swap = await Shell.Current.DisplayAlert("Cor já em uso!", $"A cor selecionada já está sendo usada pelo grupo de ativos \"{existingGroup.Name}\".\n\nDeseja trocar as cores entre os grupos?", "Sim", "Não");

                // Verifica se o usuário quer trocar as cores entre os grupos.
                if(swap) {
                    // Armazena a cor atual do grupo existente para possibilitar o rollback, se necessário.
                    currentColor = existingGroup.Color;
                    // Troca as cores entre o grupo local e o grupo existente utilizando desestruturação.
                    existingGroup.Color = oldColor;
                } else {
                    // Força uma exceção para realizar o rollback caso o usuário não queira trocar as cores.
                    throw new Exception();
                }
            }

            // Tenta atualizar a estratégia no serviço de carteiras.
            if(!await walletService.UpdateStrategy([.. Strategy])) {
                // Indica que ocorreu algum erro ao salvar no banco de dados.
                throw new Exception();
            }
        } catch {
            // Aguarda um tempo para permitir que a interface reflita as alterações.
            await Task.Delay(100);

            // Rollback: restaura o valor da cor antiga no grupo local.
            group.Color = oldColor;

            // Se houve troca de cores, desfaz a troca restaurando a cor original do grupo existente.
            if(swap && existingGroup is not null) {
                // Informa ao usuário que ocorreu um erro ao modificar a cor do grupo de ativos.
                await Shell.Current.DisplayAlert("Não foi possível alterar a cor!", $"Ocorreu um erro ao tentar salvar a nova cor para o grupo de ativos \"{group.Name}\".", "OK");

                // Rollback: restaura a cor do grupo existente para seu valor original.
                existingGroup.Color = currentColor;
            }
        }
    }

    /// <summary>
    /// Realiza a navegação para a página de edição de um grupo de ativos na estratégia da carteira.
    /// </summary>
    /// <param name="name">O nome do grupo de ativos que será renomeado.</param>
    /// <returns>Uma tarefa que representa a operação assíncrona.</returns>
    public async Task Rename(string name) {
        // Impede execução simultânea do comando.
        if(!IsRunning) {
            // Sinaliza que a execução está em andamento.
            IsRunning = true;

            // Navega até a página de estratégia da carteira.
            await navigationService.NavigateTo($"edit?group={name}");
            // Pequeno atraso para garantir estabilidade de navegação.
            await Task.Delay(100);

            // Finaliza a execução do comando.
            IsRunning = false;
        }
    }

    /// <summary>
    /// Realiza a remoção de um grupo de ativos na estratégia da carteira.
    /// </summary>
    /// <param name="name">O nome do grupo de ativos que será renomeado.</param>
    /// <returns>Uma tarefa que representa a operação assíncrona.</returns>
    public async Task Delete(string name) {
        // Impede execução simultânea do comando.
        if(!IsRunning) {
            // Sinaliza que a execução está em andamento.
            IsRunning = true;

            // Variável auxiliar para saber o índice do grupo de ativos na lista.
            int index;
            // Interage cada item da lista.
            for(index = 0; index < Strategy.Count; index++) {
                // Busca o grupo de ativos com o nome informado no parâmetro.
                if(Strategy[index].Name.Equals(name)) {
                    // Finaliza o loop ao encontrar o grupo.
                    break;
                }
            }

            // Obtém o grupo de ativos local.
            var group = Strategy[index];
            // Removendo o grupo de ativos da lista.
            Strategy.RemoveAt(index);

            // Tenta atualizar a estratégia no serviço de carteiras.
            if(!await walletService.UpdateStrategy([.. Strategy])) {
                // Informa ao usuário que ocorreu um erro ao modificar a porcentagem do grupo de ativos.
                await Shell.Current.DisplayAlert("Não foi possível deletar!", $"Ocorreu um erro ao tentar deletar o grupo de ativos \"{group.Name}\".", "OK");

                // Rollback: Adicionando o grupo novamente na lista.
                Strategy.Insert(index, group);
            }

            // Atualiza as propriedades da página.
            UpdateProperties(walletService.Wallet);

            // Pequeno atraso para garantir estabilidade de navegação.
            await Task.Delay(100);

            // Finaliza a execução do comando.
            IsRunning = false;
        }
    }

    #endregion Walleting Methods
}