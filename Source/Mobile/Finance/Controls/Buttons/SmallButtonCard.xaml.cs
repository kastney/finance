namespace Finance.Controls.Buttons;

/// <summary>
/// Botão compacto customizado com ícone, texto e indicador de carregamento.
/// </summary>
public partial class SmallButtonCard : ContentView {

    #region Fields

    /// <summary>
    /// Propriedade vinculável que define o texto exibido abaixo do ícone.
    /// </summary>
    public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(SmallButtonCard), string.Empty, propertyChanged: OnTextChanged);

    /// <summary>
    /// Propriedade vinculável que define o ícone exibido no botão (normalmente um código de fonte de ícones).
    /// </summary>
    public static readonly BindableProperty GlyphProperty = BindableProperty.Create(nameof(Glyph), typeof(string), typeof(SmallButtonCard), string.Empty, propertyChanged: OnGlyphChanged);

    /// <summary>
    /// Propriedade vinculável que define se o botão está no estado de carregamento.
    /// </summary>
    public static readonly BindableProperty IsRunningProperty = BindableProperty.Create(nameof(IsRunning), typeof(bool), typeof(SmallButtonCard), false, propertyChanged: OnIsRunningChanged);

    /// <summary>
    /// Propriedade vinculável que define se o ícone de atenção está visível ou não.
    /// </summary>
    public static readonly BindableProperty IsWarningProperty = BindableProperty.Create(nameof(IsWarning), typeof(bool), typeof(SmallButtonCard), false, propertyChanged: OnIsWarningChanged);

    /// <summary>
    /// Propriedade vinculável que representa o comando a ser executado quando o botão for tocado.
    /// </summary>
    public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(SmallButtonCard));

    #endregion Fields

    #region Properties

    /// <summary>
    /// Texto exibido abaixo do botão.
    /// </summary>
    public string Text {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    /// <summary>
    /// Ícone exibido no botão.
    /// </summary>
    public string Glyph {
        get => (string)GetValue(GlyphProperty);
        set => SetValue(GlyphProperty, value);
    }

    /// <summary>
    /// Indica se o botão está em estado de carregamento (mostrando um indicador em vez do ícone).
    /// </summary>
    public bool IsRunning {
        get => (bool)GetValue(IsRunningProperty);
        set => SetValue(IsRunningProperty, value);
    }

    /// <summary>
    /// Exibição do ícone de atenção como indicador no botão.
    /// </summary>
    public bool IsWarning {
        get => (bool)GetValue(IsWarningProperty);
        set => SetValue(IsWarningProperty, value);
    }

    /// <summary>
    /// Comando executado quando o botão é tocado.
    /// </summary>
    public ICommand Command {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    #endregion Properties

    #region Constructor

    /// <summary>
    /// Inicializa os componentes visuais do botão.
    /// </summary>
    public SmallButtonCard() {
        // Inicializa os elementos definidos no XAML.
        InitializeComponent();
    }

    #endregion Constructor

    #region Methods

    /// <summary>
    /// Atualiza o texto exibido quando a propriedade Text é alterada.
    /// </summary>
    /// <param name="bindable">Instância do controle que teve a propriedade alterada.</param>
    /// <param name="oldValue">Valor anterior da propriedade.</param>
    /// <param name="newValue">Novo valor da propriedade.</param>
    private static void OnTextChanged(BindableObject bindable, object oldValue, object newValue) {
        // Verifica se o bindable é do tipo SmallButtonCard.
        if(bindable is not SmallButtonCard control) { return; }
        // Atualiza o texto no label do botão.
        control.text.Text = newValue.ToString();
    }

    /// <summary>
    /// Atualiza o ícone exibido quando a propriedade Glyph é alterada.
    /// </summary>
    /// <param name="bindable">Instância do controle que teve a propriedade alterada.</param>
    /// <param name="oldValue">Valor anterior da propriedade.</param>
    /// <param name="newValue">Novo valor da propriedade.</param>
    private static void OnGlyphChanged(BindableObject bindable, object oldValue, object newValue) {
        // Verifica se o bindable é do tipo SmallButtonCard.
        if(bindable is not SmallButtonCard control) { return; }
        // Atualiza o conteúdo do ícone.
        control.icon.Text = newValue.ToString();
    }

    /// <summary>
    /// Alterna entre o ícone e o indicador de carregamento com base na propriedade IsRunning.
    /// </summary>
    /// <param name="bindable">Instância do controle que teve a propriedade alterada.</param>
    /// <param name="oldValue">Valor anterior da propriedade.</param>
    /// <param name="newValue">Novo valor da propriedade.</param>
    private static void OnIsRunningChanged(BindableObject bindable, object oldValue, object newValue) {
        // Verifica se o bindable é do tipo SmallButtonCard.
        if(bindable is not SmallButtonCard control) { return; }
        // Esconde o ícone e mostra o indicador quando IsRunning for true.
        control.icon.IsVisible = !(bool)newValue;
        // Esconde o indicador e mostra o ícone quando IsRunning for false.
        control.indicator.IsVisible = (bool)newValue;
    }

    /// <summary>
    /// Altera a visibilidade do ícone de atenção no controle.
    /// </summary>
    /// <param name="bindable">Instância do controle que teve a propriedade alterada.</param>
    /// <param name="oldValue">Valor anterior da propriedade.</param>
    /// <param name="newValue">Novo valor da propriedade.</param>
    private static void OnIsWarningChanged(BindableObject bindable, object oldValue, object newValue) {
        // Verifica se o bindable é do tipo SmallButtonCard.
        if(bindable is not SmallButtonCard control) { return; }
        // Altera a visibilidade do ícone de atenção no controle.
        control.warningIcon.IsVisible = (bool)newValue;
    }

    /// <summary>
    /// Manipula o toque no botão e executa o comando, se possível.
    /// </summary>
    /// <param name="sender">Objeto que disparou o evento.</param>
    /// <param name="e">Argumentos do evento de toque.</param>
    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e) {
        // Executa o comando apenas se não estiver carregando e o comando estiver disponível.
        if(!IsRunning && Command is not null && Command.CanExecute(null)) {
            // Executa o comando.
            Command.Execute(null);
        }
    }

    #endregion Methods
}