using Finance.Models;
using Finance.Utilities;

namespace Finance.Services.Walleting;

/// <summary>
/// Serviço responsável pelo gerenciamento de carteiras locais utilizando SQLite.
/// </summary>
internal class WalletService : IWalletService {

    #region Fields

    /// <summary>
    /// Conexão assíncrona com o banco de dados SQLite local.
    /// </summary>
    private SQLiteAsyncConnection database;

    #endregion Fields

    #region Properties

    /// <summary>
    /// Obtém ou define a carteira atualmente ativa no contexto do aplicativo.
    /// </summary>
    public Wallet Wallet { get; set; }

    #endregion Properties

    #region Wallet Management Methods

    /// <summary>
    /// Cria e armazena uma nova carteira no banco de dados local.
    /// </summary>
    /// <param name="wallet">Objeto da carteira a ser criada.</param>
    /// <returns>O número de linhas afetadas na operação (espera-se 1).</returns>
    public async Task<int> Create(Wallet wallet) {
        // Inicializa o banco de dados, caso ainda não tenha sido feito.
        await Init();
        // Insere a carteira no banco de dados.
        return await database.InsertAsync(wallet);
    }

    /// <summary>
    /// Remove uma carteira existente do banco de dados local com base no ID.
    /// </summary>
    /// <param name="wallet">Carteira a ser excluída.</param>
    /// <returns>Uma tarefa que representa a operação assíncrona de exclusão.</returns>
    public async Task Delete(Wallet wallet) {
        // Inicializa o banco de dados, caso ainda não tenha sido feito.
        await Init();
        // Remove a carteira com base no ID.
        await database.DeleteAsync<Wallet>(wallet.Id);
    }

    /// <summary>
    /// Verifica se há carteiras existentes e define uma como atual se necessário.
    /// </summary>
    /// <returns><c>true</c> se houver pelo menos uma carteira válida; caso contrário, <c>false</c>.</returns>
    public async Task<bool> Exists() {
        // Inicializa o banco de dados, caso ainda não tenha sido feito.
        await Init();

        // Verifica se existe carteiras cadastradas.
        var result = await database.QueryAsync<Wallet>("SELECT * FROM wallets LIMIT 1");
        // Se nenhuma carteira foi encontrada, retorna falso.
        if(result.Count == 0) { return false; }

        // Obtém o identificador salvo da carteira definida como atual.
        var walletId = Preferences.Get("WalletId", default(string));
        // Verifica se a carteira atual (armazenada nas preferências) ainda existe no banco.
        Wallet = (await database.QueryAsync<Wallet>("SELECT * FROM wallets WHERE id = ? LIMIT 1", walletId)).FirstOrDefault();
        // Se a carteira atual for encontrada, retorna verdadeiro.
        if(Wallet is not null) { return true; }

        // Caso a carteira atual não exista, define a primeira da lista como carteira ativa.
        Wallet = result.FirstOrDefault();
        // Armazena o ID da nova carteira ativa nas preferências.
        Preferences.Set("WalletId", Wallet.Id.ToString());

        // Retorna verdadeiro indicando que uma carteira válida foi configurada.
        return true;
    }

    /// <summary>
    /// Verifica se já existe uma carteira com o nome especificado.
    /// </summary>
    /// <param name="name">Nome da carteira a ser verificada.</param>
    /// <returns><c>true</c> se o nome já estiver em uso; caso contrário, <c>false</c>.</returns>
    public async Task<bool> Exists(string name) {
        // Inicializa o banco de dados, caso ainda não tenha sido feito.
        await Init();

        // Verifica se há alguma carteira no banco com o nome informado.
        var result = await database.QueryAsync<Wallet>("SELECT id FROM wallets WHERE name = ? LIMIT 1", name);

        // Retorna verdadeiro se houver pelo menos uma ocorrência.
        return result.Count != 0;
    }

    /// <summary>
    /// Retorna a lista de carteiras cadastradas, excluindo a carteira atual.
    /// </summary>
    /// <returns>Lista de carteiras disponíveis para seleção.</returns>
    public async Task<List<Wallet>> AvailableWallets() {
        // Inicializa o banco de dados, caso ainda não tenha sido feito.
        await Init();

        // Retorna todas as carteiras exceto a atualmente ativa.
        return await database.QueryAsync<Wallet>("SELECT id,name FROM wallets WHERE name != ?", Wallet.Name);
    }

    /// <summary>
    /// Define a carteira especificada como a carteira atual e salva a preferência.
    /// </summary>
    /// <param name="wallet">Carteira a ser definida como ativa.</param>
    public void SetWallet(Wallet wallet) {
        // Atualiza o ID da carteira ativa nas preferências.
        Preferences.Set("WalletId", wallet.Id.ToString());
    }

    #endregion Wallet Management Methods

    #region Start Methods

    /// <summary>
    /// Inicializa a conexão com o banco de dados SQLite e garante que a tabela de carteiras exista.
    /// </summary>
    /// <returns>Tarefa que representa a operação de inicialização do banco de dados.</returns>
    private async Task Init() {
        // Verifica se a conexão com o banco de dados já foi criada.
        if(database is null) {
            // Cria uma nova conexão com base no caminho e configurações definidos nos constantes.
            database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);

            // Caso necessário, a tabela pode ser descartada antes da criação.
            //await database.DropTableAsync<Wallet>();

            // Cria a tabela de carteiras, se ela ainda não existir.
            await database.CreateTableAsync<Wallet>();
        }
    }

    #endregion Start Methods

    #region Strategy Methods

    /// <summary>
    /// Adiciona um novo grupo de ativos à estratégia da carteira e atualiza o banco de dados.
    /// </summary>
    /// <param name="assetGroupName">Nome do grupo de ativos a ser adicionado.</param>
    /// <returns>
    /// Uma tarefa que representa a operação assíncrona.
    /// O resultado será <c>true</c> indicando que a estratégia foi atualizada com sucesso.
    /// </returns>
    public async Task<bool> AddAssetGroup(string assetGroupName) {
        try {
            // Cria o novo grupo de ativos com o nome especificado e adiciona à estratégia da carteira.
            Wallet.Strategy.Add(new AssetGroup { Name = assetGroupName, Enabled = true });

            // Serializa a estratégia atualizada em uma string JSON.
            var newStrategy = AssetMetadata.SerializeStrategy(Wallet.Strategy);

            // Inicializa o banco de dados, caso ainda não tenha sido feito.
            await Init();

            // Executa a atualização diretamente via comando SQL.
            var rowsAffected = await database.ExecuteAsync("UPDATE wallets SET StrategyJson = ? WHERE id = ?", newStrategy, Wallet.Id);

            // Se a atualização afetou pelo menos uma linha, atualiza a propriedade local.
            if(rowsAffected > 0) {
                // Atualiza a propriedade serializada StrategyJson da carteira.
                Wallet.StrategyJson = newStrategy;

                // Retorna verdadeiro indicando que o grupo foi adicionado e a estratégia atualizada com sucesso.
                return true;
            }

            // Caso não tenha afetado nenhuma linha, considera como falha.
            return false;
        } catch {
            // Em caso de erro durante qualquer etapa, retorna falso indicando falha na atualização.
            return false;
        }
    }

    /// <summary>
    /// Atualiza os grupos de ativos na estratégia.
    /// </summary>
    /// <param name="strategy">Lista de grupos de ativos que representa a nova estratégia a ser salva.</param>
    /// <returns>Uma tarefa que representa a operação assíncrona de salvar a estratégia.</returns>
    public async Task<bool> UpdateStrategy(List<AssetGroup> strategy) {
        try {
            // Serializa a estratégia atualizada em uma string JSON.
            var newStrategy = AssetMetadata.SerializeStrategy(strategy);

            // Inicializa o banco de dados, caso ainda não tenha sido feito.
            await Init();

            // Executa a atualização diretamente via comando SQL.
            var rowsAffected = await database.ExecuteAsync("UPDATE wallets SET StrategyJson = ? WHERE id = ?", newStrategy, Wallet.Id);

            // Se a atualização afetou pelo menos uma linha, atualiza a propriedade local.
            if(rowsAffected > 0) {
                // Atualiza a propriedade serializada StrategyJson da carteira.
                Wallet.StrategyJson = newStrategy;

                // Retorna verdadeiro indicando que a estratégia atualizada com sucesso.
                return true;
            }

            // Caso não tenha afetado nenhuma linha, considera como falha.
            return false;
        } catch {
            // Em caso de erro durante qualquer etapa, retorna falso indicando falha na atualização.
            return false;
        }
    }

    #endregion Strategy Methods
}