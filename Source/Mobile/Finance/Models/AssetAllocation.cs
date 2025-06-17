using Finance.Enumerations;
using Finance.Utilities;

namespace Finance.Models;

/// <summary>
/// Representa a alocação de um tipo específico de ativo dentro de um grupo da estratégia da carteira.
/// </summary>
internal class AssetAllocation : INotifyPropertyChanged {

    #region Fields

    /// <summary>
    /// Referência à carteira à qual esta alocação pertence.
    /// Usado para acessar dados dinâmicos relacionados ao tipo de ativo.
    /// </summary>
    private Wallet wallet;

    /// <summary>
    /// O tipo do ativo financeiro.
    /// </summary>
    private AssetType type;

    /// <summary>
    /// Campo que indica se o grupo de ativos está ativado.
    /// </summary>
    private bool enabled;

    /// <summary>
    /// Campo que armazena a porcentagem atual do tipo de ativo.
    /// </summary>
    private int percentage;

    /// <summary>
    /// Campo que armazena a porcentagem disponível para alocação entre os tipos de ativos.
    /// </summary>
    private int percentageAvailable;

    /// <summary>
    /// Campo que armazena a cor do tipo de ativo.
    /// </summary>
    private int color;

    /// <summary>
    /// Campo que armazena a cor do grupo de ativos.
    /// </summary>
    private int groupColor;

    #endregion Fields

    #region Properties

    /// <summary>
    /// Tipo de ativo financeiro alocado (por exemplo, ações, FIIs, BDRs, etc.).
    /// </summary>
    public AssetType Type {
        get => type;
        set => SetProperty(ref type, value);
    }

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

    /// <summary>
    /// Indicador do estado de ativado de um grupo de ativos.
    /// </summary>
    public bool Enabled {
        get => enabled;
        set => SetProperty(ref enabled, value);
    }

    /// <summary>
    /// A porcentagem do grupo ed ativos.
    /// </summary>
    public int Percentage {
        get => percentage;
        set => SetProperty(ref percentage, value);
    }

    /// <summary>
    /// A porcentagem disponível para uso entre os grupos de ativos.
    /// </summary>
    [JsonIgnore]
    public int PercentageAvailable {
        get => percentageAvailable;
        set => SetProperty(ref percentageAvailable, value);
    }

    /// <summary>
    /// A cor do tipo de ativo.
    /// </summary>
    public int Color {
        get => color;
        set => SetProperty(ref color, value);
    }

    /// <summary>
    /// A cor do grupo de ativos.
    /// </summary>
    [JsonIgnore]
    public int GroupColor {
        get => groupColor;
        set => SetProperty(ref groupColor, value);
    }

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

    #region INotifyPropertyChanged

    /// <summary>
    /// Evento disparado quando uma propriedade do grupo de ativos é alterada.
    /// </summary>
    public event PropertyChangedEventHandler PropertyChanged;

    /// <summary>
    /// Dispara o evento PropertyChanged para notificar a alteração de uma propriedade.
    /// </summary>
    /// <param name="propertyName">Nome da propriedade que foi alterada. Preenchido automaticamente pelo compilador.</param>
    protected void OnPropertyChanged([CallerMemberName] string propertyName = null) {
        // Se houver inscritos no evento PropertyChanged, notifica-os sobre a alteração.
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    /// <summary>
    /// Define o valor de um campo e notifica a alteração da propriedade associada.
    /// </summary>
    /// <typeparam name="T">Tipo do campo e da propriedade.</typeparam>
    /// <param name="backingStore">Referência para o campo de armazenamento.</param>
    /// <param name="value">Novo valor a ser definido.</param>
    /// <param name="propertyName">Nome da propriedade associada. Preenchido automaticamente pelo compilador.</param>
    /// <returns>Retorna true se o valor foi alterado; caso contrário, false.</returns>
    protected bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName] string propertyName = null) {
        // Verifica se o novo valor é igual ao atual; se for, não altera nem dispara notificação.
        if(EqualityComparer<T>.Default.Equals(backingStore, value)) { return false; }
        // Atualiza o valor do campo de armazenamento.
        backingStore = value;
        // Notifica que a propriedade associada foi alterada.
        OnPropertyChanged(propertyName);
        // Indica que a alteração foi realizada.
        return true;
    }

    #endregion INotifyPropertyChanged
}