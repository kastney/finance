namespace Finance.Controls.Buttons;

/// <summary>
/// Botão customizado com ícone, texto e estado de carregamento (Indicator).
/// </summary>
public partial class IndicatorButton : ContentView {

    #region Fields

    /// <summary>
    /// Propriedade vinculável para o ícone do botão (fonte de ícones).
    /// </summary>
    public static readonly BindableProperty GlyphProperty = BindableProperty.Create(nameof(Glyph), typeof(string), typeof(IndicatorButton), string.Empty, propertyChanged: OnGlyphChanged);

    /// <summary>
    /// Propriedade vinculável que define se o botão está no estado de carregamento.
    /// </summary>
    public static readonly BindableProperty IsRunningProperty = BindableProperty.Create(nameof(IsRunning), typeof(bool), typeof(IndicatorButton), false, propertyChanged: OnIsRunningChanged);

    /// <summary>
    /// Propriedade vinculável para o texto exibido no botão.
    /// </summary>
    public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(IndicatorButton), string.Empty, propertyChanged: OnTextChanged);

    /// <summary>
    /// Propriedade vinculável que representa o comando executado ao tocar o botão.
    /// </summary>
    public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(IndicatorButton));

    #endregion Fields

    #region Properties

    /// <summary>
    /// Ícone exibido antes do texto (usualmente um código de fonte de ícones).
    /// </summary>
    public string Glyph {
        get => (string)GetValue(GlyphProperty);
        set => SetValue(GlyphProperty, value);
    }

    /// <summary>
    /// Indica se o botão está em estado de carregamento.
    /// </summary>
    public bool IsRunning {
        get => (bool)GetValue(IsRunningProperty);
        set => SetValue(IsRunningProperty, value);
    }

    /// <summary>
    /// Texto exibido no botão.
    /// </summary>
    public string Text {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    /// <summary>
    /// Comando a ser executado quando o botão for tocado.
    /// </summary>
    public ICommand Command {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    #endregion Properties

    #region Events

    /// <summary>
    /// Evento disparado quando o botão for clicado.
    /// </summary>
    public event EventHandler Clicked;

    #endregion Events

    #region Constructor

    /// <summary>
    /// Inicializa os componentes visuais do botão.
    /// </summary>
    public IndicatorButton() {
        // Chama o método InitializeComponent para carregar o XAML associado a este controle.
        InitializeComponent();
    }

    #endregion Constructor

    #region Methods

    /// <summary>
    /// Atualiza o ícone exibido no botão quando a propriedade Glyph muda.
    /// </summary>
    /// <param name="bindable">Instância do controle IndicatorButton que teve a propriedade alterada.</param>
    /// <param name="oldValue">Valor anterior da propriedade Glyph.</param>
    /// <param name="newValue">Novo valor da propriedade Glyph.</param>
    private static void OnGlyphChanged(BindableObject bindable, object oldValue, object newValue) {
        // Verifica se o bindable é do tipo IndicatorButton.
        if(bindable is not IndicatorButton control) { return; }
        // Atualiza o ícone do botão com o novo valor.
        control.icon.Text = $"{newValue}   ";
    }

    /// <summary>
    /// Alterna entre o botão e o indicador de carregamento com base na propriedade IsRunning.
    /// </summary>
    /// <param name="bindable">Instância do controle IndicatorButton que teve a propriedade alterada.</param>
    /// <param name="oldValue">Valor anterior da propriedade IsRunning.</param>
    /// <param name="newValue">Novo valor da propriedade IsRunning.</param>
    private static void OnIsRunningChanged(BindableObject bindable, object oldValue, object newValue) {
        // Verifica se o bindable é do tipo IndicatorButton.
        if(bindable is not IndicatorButton control) { return; }
        // Exibe o frame com indicador de carregamento se IsRunning for true.
        control.frame.IsVisible = (bool)newValue;
        // Oculta o botão de ação enquanto o indicador estiver visível.
        control.button.IsVisible = !(bool)newValue;
    }

    /// <summary>
    /// Atualiza o texto exibido no botão quando a propriedade Text muda.
    /// </summary>
    /// <param name="bindable">Instância do controle IndicatorButton que teve a propriedade alterada.</param>
    /// <param name="oldValue">Valor anterior da propriedade Text.</param>
    /// <param name="newValue">Novo valor da propriedade Text.</param>
    private static void OnTextChanged(BindableObject bindable, object oldValue, object newValue) {
        // Verifica se o bindable é do tipo IndicatorButton.
        if(bindable is not IndicatorButton control) { return; }
        // Atualiza o texto do botão.
        control.text.Text = newValue.ToString();
    }

    /// <summary>
    /// Manipula o evento de toque no botão.
    /// </summary>
    /// <param name="sender">Objeto que disparou o evento.</param>
    /// <param name="e">Dados do evento de toque.</param>
    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e) {
        // Dispara o evento Clicked para notificar listeners externos.
        Clicked?.Invoke(this, e);
        // Executa o comando associado, se possível.
        if(Command is not null && Command.CanExecute(null)) {
            // Executa o comando associado ao botão.
            Command.Execute(null);
        }
    }

    #endregion Methods
}