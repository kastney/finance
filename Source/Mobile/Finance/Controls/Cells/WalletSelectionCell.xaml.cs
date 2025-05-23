namespace Finance.Controls.Cells;

/// <summary>
/// Representa uma célula personalizada para exibir e selecionar uma carteira na interface.
/// Contém um texto e um botão de seleção visual.
/// </summary>
public partial class WalletSelectionCell : ContentView {

    #region Fields

    /// <summary>
    /// Propriedade vinculável para o texto exibido na célula, representando o nome da carteira.
    /// </summary>
    public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(WalletSelectionCell), string.Empty, propertyChanged: OnTextChanged);

    /// <summary>
    /// Propriedade vinculável que indica se a carteira está selecionada ou não.
    /// </summary>
    public static readonly BindableProperty IsCheckedProperty = BindableProperty.Create(nameof(IsChecked), typeof(bool), typeof(WalletSelectionCell), false, propertyChanged: OnIsCheckedChanged);

    #endregion Fields

    #region Properties

    /// <summary>
    /// Obtém ou define o texto que será exibido na célula.
    /// </summary>
    public string Text {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    /// <summary>
    /// Obtém ou define um valor que indica se a célula está marcada como selecionada.
    /// </summary>
    public bool IsChecked {
        get => (bool)GetValue(IsCheckedProperty);
        set => SetValue(IsCheckedProperty, value);
    }

    #endregion Properties

    #region Constructor

    /// <summary>
    /// Inicializa uma nova instância da classe <see cref="WalletSelectionCell"/>.
    /// Responsável por configurar os componentes visuais da célula.
    /// </summary>
    public WalletSelectionCell() {
        // Inicializa os componentes definidos no XAML associado.
        InitializeComponent();
    }

    #endregion Constructor

    #region Methods

    /// <summary>
    /// Método chamado automaticamente quando a propriedade Text é alterada.
    /// Atualiza o conteúdo visual da célula com o novo texto.
    /// </summary>
    /// <param name="bindable">Instância da célula que está sendo atualizada.</param>
    /// <param name="oldValue">Valor anterior da propriedade.</param>
    /// <param name="newValue">Novo valor da propriedade.</param>
    private static void OnTextChanged(BindableObject bindable, object oldValue, object newValue) {
        // Verifica se o bindable é do tipo WalletSelectionCell e se o novo valor é uma string.
        if(bindable is not WalletSelectionCell control || newValue is not string value) { return; }
        // Atualiza o texto exibido na célula com o novo valor.
        control.text.Text = value;
    }

    /// <summary>
    /// Método chamado automaticamente quando a propriedade IsChecked é alterada.
    /// Atualiza o estado visual do botão de seleção.
    /// </summary>
    /// <param name="bindable">Instância da célula que está sendo atualizada.</param>
    /// <param name="oldValue">Valor anterior da propriedade.</param>
    /// <param name="newValue">Novo valor da propriedade.</param>
    private static void OnIsCheckedChanged(BindableObject bindable, object oldValue, object newValue) {
        // Verifica se o bindable é do tipo WalletSelectionCell e se o novo valor é um booleano.
        if(bindable is not WalletSelectionCell control || newValue is not bool value) { return; }
        // Atualiza o estado do botão de seleção visual conforme o novo valor.
        control.button.IsChecked = value;
    }

    #endregion Methods
}