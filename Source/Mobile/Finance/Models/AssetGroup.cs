namespace Finance.Models;

/// <summary>
/// Representa um agrupamento de ativos dentro de uma carteira, como "Renda Variável" ou "Caixa".
/// Cada grupo possui um nome e uma lista de alocações de ativos.
/// </summary>
internal class AssetGroup : INotifyPropertyChanged {

    #region Fields

    /// <summary>
    /// Campo que armazena o nome do grupo de ativos.
    /// </summary>
    private string name;

    /// <summary>
    /// Campo que armazena a lista de ativos alocados ao grupo.
    /// </summary>
    private List<AssetAllocation> assets;

    /// <summary>
    /// Campo que indica se o grupo de ativos está ativado.
    /// </summary>
    private bool enabled;

    /// <summary>
    /// Campo que armazena a porcentagem atual do grupo de ativos.
    /// </summary>
    private int percentage;

    /// <summary>
    /// Campo que armazena a porcentagem disponível para alocação entre os grupos de ativos.
    /// </summary>
    private int percentageAvailable;

    #endregion Fields

    #region Properties

    /// <summary>
    /// Nome do grupo de ativos, definido pelo usuário para organização (ex: "Ações", "Caixa").
    /// </summary>
    public string Name {
        get => name;
        set => SetProperty(ref name, value);
    }

    /// <summary>
    /// Lista de tipos de ativos que pertencem a este grupo.
    /// </summary>
    public List<AssetAllocation> Assets {
        get => assets;
        set => SetProperty(ref assets, value);
    }

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

    #endregion Properties

    #region Constructor

    /// <summary>
    /// Inicializa uma nova instância do grupo de ativos com a lista de alocações vazia.
    /// </summary>
    public AssetGroup() {
        // Inicializa a lista de ativos do grupo.
        Assets = [];
    }

    #endregion Constructor

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