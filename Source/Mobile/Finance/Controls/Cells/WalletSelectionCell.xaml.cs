namespace Finance.Controls.Cells;

/// <summary>
/// Representa uma c�lula personalizada para exibir e selecionar uma carteira na interface.
/// Cont�m um texto e um bot�o de sele��o visual.
/// </summary>
public partial class WalletSelectionCell : ContentView {

    #region Fields

    /// <summary>
    /// Propriedade vincul�vel para o texto exibido na c�lula, representando o nome da carteira.
    /// </summary>
    public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(WalletSelectionCell), string.Empty, propertyChanged: OnTextChanged);

    /// <summary>
    /// Propriedade vincul�vel que indica se a carteira est� selecionada ou n�o.
    /// </summary>
    public static readonly BindableProperty IsCheckedProperty = BindableProperty.Create(nameof(IsChecked), typeof(bool), typeof(WalletSelectionCell), false, propertyChanged: OnIsCheckedChanged);

    #endregion Fields

    #region Properties

    /// <summary>
    /// Obt�m ou define o texto que ser� exibido na c�lula.
    /// </summary>
    public string Text {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    /// <summary>
    /// Obt�m ou define um valor que indica se a c�lula est� marcada como selecionada.
    /// </summary>
    public bool IsChecked {
        get => (bool)GetValue(IsCheckedProperty);
        set => SetValue(IsCheckedProperty, value);
    }

    #endregion Properties

    #region Constructor

    /// <summary>
    /// Inicializa uma nova inst�ncia da classe <see cref="WalletSelectionCell"/>.
    /// Respons�vel por configurar os componentes visuais da c�lula.
    /// </summary>
    public WalletSelectionCell() {
        // Inicializa os componentes definidos no XAML associado.
        InitializeComponent();
    }

    #endregion Constructor

    #region Methods

    /// <summary>
    /// M�todo chamado automaticamente quando a propriedade Text � alterada.
    /// Atualiza o conte�do visual da c�lula com o novo texto.
    /// </summary>
    /// <param name="bindable">Inst�ncia da c�lula que est� sendo atualizada.</param>
    /// <param name="oldValue">Valor anterior da propriedade.</param>
    /// <param name="newValue">Novo valor da propriedade.</param>
    private static void OnTextChanged(BindableObject bindable, object oldValue, object newValue) {
        // Verifica se o bindable � do tipo WalletSelectionCell e se o novo valor � uma string.
        if(bindable is not WalletSelectionCell control || newValue is not string value) { return; }
        // Atualiza o texto exibido na c�lula com o novo valor.
        control.text.Text = value;
    }

    /// <summary>
    /// M�todo chamado automaticamente quando a propriedade IsChecked � alterada.
    /// Atualiza o estado visual do bot�o de sele��o.
    /// </summary>
    /// <param name="bindable">Inst�ncia da c�lula que est� sendo atualizada.</param>
    /// <param name="oldValue">Valor anterior da propriedade.</param>
    /// <param name="newValue">Novo valor da propriedade.</param>
    private static void OnIsCheckedChanged(BindableObject bindable, object oldValue, object newValue) {
        // Verifica se o bindable � do tipo WalletSelectionCell e se o novo valor � um booleano.
        if(bindable is not WalletSelectionCell control || newValue is not bool value) { return; }
        // Atualiza o estado do bot�o de sele��o visual conforme o novo valor.
        control.button.IsChecked = value;
    }

    #endregion Methods
}