using Finance.Enumerations;
using Finance.Utilities;

namespace Finance.Models;

/// <summary>
/// Representa uma carteira de investimentos armazenada no banco de dados local.
/// Contém informações persistidas (como nome e dados serializados) e propriedades calculadas em tempo de execução.
/// </summary>
[Table("wallets")]
internal class Wallet {

    #region Database Properties

    /// <summary>
    /// Identificador único da carteira (chave primária autoincrementada).
    /// </summary>
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    /// <summary>
    /// Nome da carteira. Deve ser único e ter no máximo 50 caracteres.
    /// </summary>
    [MaxLength(50), Unique]
    public string Name { get; set; }

    /// <summary>
    /// Representação serializada da estratégia de agrupamento de ativos definida para esta carteira.
    /// </summary>
    public string StrategyJson { get; set; }

    /// <summary>
    /// Representação serializada das alocações de dados (quantidade, preço, etc.) por tipo de ativo.
    /// </summary>
    public string AllocationsJson { get; set; }

    #endregion Database Properties

    #region Runtime Properties

    /// <summary>
    /// Estratégia atual da carteira, agrupando os ativos em grupos definidos pelo usuário.
    /// Calculada dinamicamente com base em <see cref="StrategyJson"/>.
    /// </summary>
    [Ignore]
    public List<AssetGroup> Strategy {
        get {
            // Desserializa a estratégia salva ou usa a estratégia padrão caso esteja vazia.
            var strategy = string.IsNullOrWhiteSpace(StrategyJson) ? AssetMetadata.GetDefaultStrategy() : JsonSerializer.Deserialize<List<AssetGroup>>(StrategyJson);
            // Associa cada alocação de ativo a esta instância da carteira.
            strategy.ForEach(group => group.Assets.ForEach(asset => asset.SetWallet(this)));
            // Retorna a lista de grupos de ativos configurados.
            return strategy;
        }
    }

    /// <summary>
    /// Dados das alocações de ativos da carteira, indexados por tipo de ativo.
    /// Inclui informações como quantidade, preço médio, variação e performance.
    /// Calculado dinamicamente com base em <see cref="AllocationsJson"/>.
    /// </summary>
    [Ignore]
    public Dictionary<AssetType, AssetAllocationData> Allocations {
        get {
            // Desserializa os dados de alocação ou retorna a configuração padrão se estiver vazia.
            return string.IsNullOrWhiteSpace(AllocationsJson) ? AssetMetadata.GetDefaultAllocations() : JsonSerializer.Deserialize<Dictionary<AssetType, AssetAllocationData>>(AllocationsJson);
        }
    }

    #endregion Runtime Properties

    #region Methods

    /// <summary>
    /// Recupera os dados de alocação para um tipo específico de ativo.
    /// </summary>
    /// <param name="type">Tipo de ativo a ser consultado.</param>
    /// <returns>Instância de <see cref="AssetAllocationData"/> correspondente ao tipo, ou uma nova instância vazia se não encontrado.</returns>
    internal AssetAllocationData GetAssetAllocationData(AssetType type) {
        // Busca os dados de alocação ou retorna uma instância vazia.
        return Allocations.TryGetValue(type, out var data) ? data : new();
    }

    #endregion Methods
}