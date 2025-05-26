namespace Finance.Controls.Buttons;

/// <summary>
/// Bot�o compacto customizado com �cone, texto e indicador de carregamento.
/// </summary>
public partial class SmallButtonCard : ContentView {

    #region Fields

    /// <summary>
    /// Propriedade vincul�vel que define o texto exibido abaixo do �cone.
    /// </summary>
    public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(SmallButtonCard), string.Empty, propertyChanged: OnTextChanged);

    /// <summary>
    /// Propriedade vincul�vel que define o �cone exibido no bot�o (normalmente um c�digo de fonte de �cones).
    /// </summary>
    public static readonly BindableProperty GlyphProperty = BindableProperty.Create(nameof(Glyph), typeof(string), typeof(SmallButtonCard), string.Empty, propertyChanged: OnGlyphChanged);

    /// <summary>
    /// Propriedade vincul�vel que define se o bot�o est� no estado de carregamento.
    /// </summary>
    public static readonly BindableProperty IsRunningProperty = BindableProperty.Create(nameof(IsRunning), typeof(bool), typeof(SmallButtonCard), false, propertyChanged: OnIsRunningChanged);

    /// <summary>
    /// Propriedade vincul�vel que define se o �cone de aten��o est� vis�vel ou n�o.
    /// </summary>
    public static readonly BindableProperty IsWarningProperty = BindableProperty.Create(nameof(IsWarning), typeof(bool), typeof(SmallButtonCard), false, propertyChanged: OnIsWarningChanged);

    /// <summary>
    /// Propriedade vincul�vel que representa o comando a ser executado quando o bot�o for tocado.
    /// </summary>
    public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(SmallButtonCard));

    #endregion Fields

    #region Properties

    /// <summary>
    /// Texto exibido abaixo do bot�o.
    /// </summary>
    public string Text {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    /// <summary>
    /// �cone exibido no bot�o.
    /// </summary>
    public string Glyph {
        get => (string)GetValue(GlyphProperty);
        set => SetValue(GlyphProperty, value);
    }

    /// <summary>
    /// Indica se o bot�o est� em estado de carregamento (mostrando um indicador em vez do �cone).
    /// </summary>
    public bool IsRunning {
        get => (bool)GetValue(IsRunningProperty);
        set => SetValue(IsRunningProperty, value);
    }

    /// <summary>
    /// Exibi��o do �cone de aten��o como indicador no bot�o.
    /// </summary>
    public bool IsWarning {
        get => (bool)GetValue(IsWarningProperty);
        set => SetValue(IsWarningProperty, value);
    }

    /// <summary>
    /// Comando executado quando o bot�o � tocado.
    /// </summary>
    public ICommand Command {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    #endregion Properties

    #region Constructor

    /// <summary>
    /// Inicializa os componentes visuais do bot�o.
    /// </summary>
    public SmallButtonCard() {
        // Inicializa os elementos definidos no XAML.
        InitializeComponent();
    }

    #endregion Constructor

    #region Methods

    /// <summary>
    /// Atualiza o texto exibido quando a propriedade Text � alterada.
    /// </summary>
    /// <param name="bindable">Inst�ncia do controle que teve a propriedade alterada.</param>
    /// <param name="oldValue">Valor anterior da propriedade.</param>
    /// <param name="newValue">Novo valor da propriedade.</param>
    private static void OnTextChanged(BindableObject bindable, object oldValue, object newValue) {
        // Verifica se o bindable � do tipo SmallButtonCard.
        if(bindable is not SmallButtonCard control) { return; }
        // Atualiza o texto no label do bot�o.
        control.text.Text = newValue.ToString();
    }

    /// <summary>
    /// Atualiza o �cone exibido quando a propriedade Glyph � alterada.
    /// </summary>
    /// <param name="bindable">Inst�ncia do controle que teve a propriedade alterada.</param>
    /// <param name="oldValue">Valor anterior da propriedade.</param>
    /// <param name="newValue">Novo valor da propriedade.</param>
    private static void OnGlyphChanged(BindableObject bindable, object oldValue, object newValue) {
        // Verifica se o bindable � do tipo SmallButtonCard.
        if(bindable is not SmallButtonCard control) { return; }
        // Atualiza o conte�do do �cone.
        control.icon.Text = newValue.ToString();
    }

    /// <summary>
    /// Alterna entre o �cone e o indicador de carregamento com base na propriedade IsRunning.
    /// </summary>
    /// <param name="bindable">Inst�ncia do controle que teve a propriedade alterada.</param>
    /// <param name="oldValue">Valor anterior da propriedade.</param>
    /// <param name="newValue">Novo valor da propriedade.</param>
    private static void OnIsRunningChanged(BindableObject bindable, object oldValue, object newValue) {
        // Verifica se o bindable � do tipo SmallButtonCard.
        if(bindable is not SmallButtonCard control) { return; }
        // Esconde o �cone e mostra o indicador quando IsRunning for true.
        control.icon.IsVisible = !(bool)newValue;
        // Esconde o indicador e mostra o �cone quando IsRunning for false.
        control.indicator.IsVisible = (bool)newValue;
    }

    /// <summary>
    /// Altera a visibilidade do �cone de aten��o no controle.
    /// </summary>
    /// <param name="bindable">Inst�ncia do controle que teve a propriedade alterada.</param>
    /// <param name="oldValue">Valor anterior da propriedade.</param>
    /// <param name="newValue">Novo valor da propriedade.</param>
    private static void OnIsWarningChanged(BindableObject bindable, object oldValue, object newValue) {
        // Verifica se o bindable � do tipo SmallButtonCard.
        if(bindable is not SmallButtonCard control) { return; }
        // Altera a visibilidade do �cone de aten��o no controle.
        control.warningIcon.IsVisible = (bool)newValue;
    }

    /// <summary>
    /// Manipula o toque no bot�o e executa o comando, se poss�vel.
    /// </summary>
    /// <param name="sender">Objeto que disparou o evento.</param>
    /// <param name="e">Argumentos do evento de toque.</param>
    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e) {
        // Executa o comando apenas se n�o estiver carregando e o comando estiver dispon�vel.
        if(!IsRunning && Command is not null && Command.CanExecute(null)) {
            // Executa o comando.
            Command.Execute(null);
        }
    }

    #endregion Methods
}