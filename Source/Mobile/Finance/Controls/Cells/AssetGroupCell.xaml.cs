namespace Finance.Controls.Cells;

/// <summary>
/// Representa uma célula visual que exibe informações sobre um grupo de ativos,
/// incluindo nome, quantidade de ativos e um aviso caso esteja vazio.
/// </summary>
public partial class AssetGroupCell : ContentView {

    #region Fields

    /// <summary>
    /// Propriedade vinculável para armazenar o nome do grupo de ativos. Sempre que alterado, dispara o método OnNameChanged.
    /// </summary>
    public static readonly BindableProperty NameProperty = BindableProperty.Create(nameof(Name), typeof(string), typeof(AssetGroupCell), string.Empty, propertyChanged: OnNameChanged);

    /// <summary>
    /// Propriedade vinculável para armazenar a quantidade de ativos no grupo. Sempre que alterado, dispara o método OnCountChanged.
    /// </summary>
    public static readonly BindableProperty CountProperty = BindableProperty.Create(nameof(Count), typeof(int?), typeof(AssetGroupCell), null, propertyChanged: OnCountChanged);

    #endregion Fields

    #region Properties

    /// <summary>
    /// Obtém ou define o nome do grupo de ativos exibido na célula.
    /// </summary>
    public string Name {
        get => (string)GetValue(NameProperty);
        set => SetValue(NameProperty, value);
    }

    /// <summary>
    /// Obtém ou define a quantidade de ativos no grupo exibido na célula.
    /// </summary>
    public int? Count {
        get => (int?)GetValue(CountProperty);
        set => SetValue(CountProperty, value);
    }

    #endregion Properties

    #region Constructor

    /// <summary>
    /// Inicializa uma nova instância de <see cref="AssetGroupCell"/>.
    /// Configura os componentes visuais definidos no XAML.
    /// </summary>
    public AssetGroupCell() {
        // Inicializa os componentes definidos na interface XAML.
        InitializeComponent();
    }

    #endregion Constructor

    #region Methods

    /// <summary>
    /// Método chamado automaticamente quando a propriedade Name for alterada.
    /// Atualiza o texto do componente visual correspondente.
    /// </summary>
    /// <param name="bindable">Instância da célula que sofreu a alteração.</param>
    /// <param name="oldValue">Valor antigo da propriedade.</param>
    /// <param name="newValue">Novo valor da propriedade.</param>
    private static void OnNameChanged(BindableObject bindable, object oldValue, object newValue) {
        // Garante que o bindable é uma instância válida de AssetGroupCell e que o novo valor é uma string.
        if(bindable is not AssetGroupCell control || newValue is not string value) { return; }
        // Atualiza o texto do componente nameText para refletir o novo nome.
        control.nameText.Text = value;
    }

    /// <summary>
    /// Método chamado automaticamente quando a propriedade Count for alterada.
    /// Atualiza o texto da quantidade de ativos e a visibilidade do aviso.
    /// </summary>
    /// <param name="bindable">Instância da célula que sofreu a alteração.</param>
    /// <param name="oldValue">Valor antigo da propriedade.</param>
    /// <param name="newValue">Novo valor da propriedade.</param>
    private static void OnCountChanged(BindableObject bindable, object oldValue, object newValue) {
        // Garante que o bindable é uma instância válida de AssetGroupCell e que o novo valor é um inteiro.
        if(bindable is not AssetGroupCell control || newValue is not int value) { return; }
        // Exibe ou oculta a mensagem de aviso dependendo se a quantidade é zero ou negativa.
        control.warningText.IsVisible = value <= 0;
        // Atualiza o texto do componente countText para refletir a nova quantidade de ativos.
        control.countText.Text = value.ToString();
    }

    #endregion Methods
}