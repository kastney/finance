using Finance.Models;

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
        
        // Verifica se a carteira atual (armazenada nas preferências) ainda existe no banco.
        Wallet = (await database.QueryAsync<Wallet>($"SELECT * FROM wallets WHERE id='{Preferences.Get("WalletId", default(string))}' LIMIT 1")).FirstOrDefault();
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
        var result = await database.QueryAsync<Wallet>($"SELECT id FROM wallets WHERE name='{name}' LIMIT 1");
        
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
        return await database.QueryAsync<Wallet>($"SELECT id,name FROM wallets WHERE name!='{Wallet.Name}'");
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

    #region Started Methods

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

    #endregion Started Methods
}