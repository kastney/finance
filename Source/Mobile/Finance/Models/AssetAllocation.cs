using Finance.Enumerations;
using Finance.Utilities;

namespace Finance.Models;

/// <summary>
/// Representa a alocação de um tipo específico de ativo dentro de um grupo da estratégia da carteira.
/// </summary>
internal class AssetAllocation {

    #region Fields

    /// <summary>
    /// Referência à carteira à qual esta alocação pertence.
    /// Usado para acessar dados dinâmicos relacionados ao tipo de ativo.
    /// </summary>
    private Wallet wallet;

    #endregion Fields

    #region Properties

    /// <summary>
    /// Tipo de ativo alocado (por exemplo, ações, FIIs, BDRs, etc.).
    /// </summary>
    public AssetType Type { get; set; }

    /// <summary>
    /// Metadados estáticos relacionados ao tipo de ativo, como nome, cultura e bandeira.
    /// São usados apenas para exibição e não são persistidos no banco de dados.
    /// </summary>
    [JsonIgnore]
    public AssetAllocationMeta Meta => AssetMetadata.Data.TryGetValue(Type, out var meta) ? meta : new();

    /// <summary>
    /// Dados dinâmicos da alocação, como quantidade, preço e variação.
    /// Esses dados são persistidos na carteira e podem ser modificados pelo usuário.
    /// </summary>
    [JsonIgnore]
    public AssetAllocationData Data => wallet?.GetAssetAllocationData(Type) ?? new();

    #endregion Properties

    #region Methods

    /// <summary>
    /// Define a referência à carteira à qual esta alocação pertence,
    /// permitindo o acesso a dados dinâmicos relacionados.
    /// </summary>
    /// <param name="wallet">Instância da carteira associada.</param>
    internal void SetWallet(Wallet wallet) {
        // Associa esta alocação a uma instância específica de carteira.
        this.wallet = wallet;
    }

    #endregion Methods
}