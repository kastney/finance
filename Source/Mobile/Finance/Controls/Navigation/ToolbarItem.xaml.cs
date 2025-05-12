namespace Finance.Controls.Navigation;

/// <summary>
/// Componente visual que representa um item de barra de ferramentas com um �cone e suporte a comando.
/// </summary>
public partial class ToolbarItem : ContentView {

    #region Fields

    /// <summary>
    /// Propriedade vincul�vel que define o caractere (�cone) exibido pelo item da toolbar.
    /// </summary>
    public static readonly BindableProperty GlyphProperty = BindableProperty.Create(nameof(Glyph), typeof(string), typeof(ToolbarItem), string.Empty, propertyChanged: OnGlyphChanged);

    /// <summary>
    /// Propriedade vincul�vel que define a fam�lia de fontes usada para o �cone.
    /// </summary>
    public static readonly BindableProperty FontFamilyProperty = BindableProperty.Create(nameof(FontFamily), typeof(string), typeof(ToolbarItem), "IconsRegular", propertyChanged: OnFontFamilyChanged);

    /// <summary>
    /// Propriedade vincul�vel que define o tamanho da fonte usada no �cone.
    /// </summary>
    public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize), typeof(double), typeof(ToolbarItem), default, propertyChanged: OnFontSizeChanged);

    /// <summary>
    /// Propriedade vincul�vel que permite associar um comando ao toque no item.
    /// </summary>
    public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(ToolbarItem));

    #endregion Fields

    #region Properties

    /// <summary>
    /// O �cone (geralmente um caractere Unicode de uma fonte de �cones).
    /// </summary>
    public string Glyph {
        get { return (string)GetValue(GlyphProperty); }
        set { SetValue(GlyphProperty, value); }
    }

    /// <summary>
    /// Fam�lia de fonte usada para renderizar o �cone.
    /// </summary>
    public string FontFamily {
        get { return (string)GetValue(FontFamilyProperty); }
        set { SetValue(FontFamilyProperty, value); }
    }

    /// <summary>
    /// Tamanho da fonte usada no �cone.
    /// </summary>
    public double FontSize {
        get { return (double)GetValue(FontSizeProperty); }
        set { SetValue(FontSizeProperty, value); }
    }

    /// <summary>
    /// Comando a ser executado quando o item for tocado.
    /// </summary>
    public ICommand Command {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    #endregion Properties

    #region Events

    /// <summary>
    /// Evento disparado quando o item � clicado.
    /// </summary>
    public event EventHandler Clicked;

    #endregion Events

    #region Constructor

    /// <summary>
    /// Inicializa o componente <see cref="ToolbarItem"/>.
    /// </summary>
    public ToolbarItem() {
        // Carrega os elementos definidos no XAML.
        InitializeComponent();
    }

    #endregion Constructor

    #region Methods

    /// <summary>
    /// Manipulador chamado quando a propriedade <see cref="Glyph"/> � alterada.
    /// Atualiza o texto do �cone exibido.
    /// </summary>
    /// <param name="bindable">O controle que disparou a altera��o.</param>
    /// <param name="oldValue">Valor anterior da propriedade.</param>
    /// <param name="newValue">Novo valor da propriedade.</param>
    private static void OnGlyphChanged(BindableObject bindable, object oldValue, object newValue) {
        // Verifica se o objeto � do tipo ToolbarItem.
        if(bindable is not ToolbarItem control) { return; }
        // Atualiza o texto do �cone.
        control.icon.Text = newValue.ToString();
    }

    /// <summary>
    /// Manipulador chamado quando a propriedade <see cref="FontFamily"/> � alterada.
    /// Atualiza a fonte utilizada pelo �cone.
    /// </summary>
    /// <param name="bindable">O controle que disparou a altera��o.</param>
    /// <param name="oldValue">Valor anterior da propriedade.</param>
    /// <param name="newValue">Novo valor da propriedade.</param>
    private static void OnFontFamilyChanged(BindableObject bindable, object oldValue, object newValue) {
        // Verifica se o objeto � do tipo ToolbarItem.
        if(bindable is not ToolbarItem control) { return; }
        // Atualiza a fam�lia da fonte do �cone.
        control.icon.FontFamily = newValue.ToString();
    }

    /// <summary>
    /// Manipulador chamado quando a propriedade <see cref="FontSize"/> � alterada.
    /// Atualiza o tamanho da fonte do �cone.
    /// </summary>
    /// <param name="bindable">O controle que disparou a altera��o.</param>
    /// <param name="oldValue">Valor anterior da propriedade.</param>
    /// <param name="newValue">Novo valor da propriedade.</param>
    private static void OnFontSizeChanged(BindableObject bindable, object oldValue, object newValue) {
        // Verifica se o objeto � do tipo ToolbarItem.
        if(bindable is not ToolbarItem control) { return; }
        // Verifica se o novo valor � um n�mero v�lido.
        if(newValue is not double size) { return; }
        // Atualiza o tamanho da fonte do �cone.
        control.icon.FontSize = size;
    }

    /// <summary>
    /// Manipula o toque no item da toolbar.
    /// Dispara o evento <see cref="Clicked"/> e executa o <see cref="Command"/>, se definido.
    /// </summary>
    /// <param name="sender">Objeto que disparou o evento.</param>
    /// <param name="e">Argumentos do evento de toque.</param>
    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e) {
        // Dispara o evento Clicked para notificar ouvintes.
        Clicked?.Invoke(this, e);
        // Executa o comando associado, se dispon�vel e permitido.
        if(Command is not null && Command.CanExecute(null)) {
            Command.Execute(null);
        }
    }

    #endregion Methods
}