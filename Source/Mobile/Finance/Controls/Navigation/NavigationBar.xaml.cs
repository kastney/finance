namespace Finance.Controls.Navigation;

/// <summary>
/// Componente visual que representa a barra de navegação customizada, com suporte a título,
/// botão de voltar e itens de ferramenta.
/// </summary>
public partial class NavigationBar : ContentView {

    #region Fields

    /// <summary>
    /// Propriedade vinculável que define o título exibido na barra de navegação.
    /// </summary>
    public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(NavigationBar), string.Empty, propertyChanged: OnTitleChanged);

    /// <summary>
    /// Propriedade vinculável que define os itens de ferramenta personalizados da barra de navegação.
    /// </summary>
    public static readonly BindableProperty ToolbarItemsProperty = BindableProperty.Create(nameof(ToolbarItems), typeof(VerticalStackLayout), typeof(NavigationBar), null, propertyChanged: OnToolbarItemsChanged);

    /// <summary>
    /// Propriedade vinculável que define a visibilidade do botão de voltar.
    /// </summary>
    public static readonly BindableProperty IsBackButtonProperty = BindableProperty.Create(nameof(IsBackButton), typeof(bool), typeof(NavigationBar), true, propertyChanged: OnIsBackButtonChanged);

    /// <summary>
    /// Propriedade vinculável que define se o controle está na raiz da navegação.
    /// </summary>
    public static readonly BindableProperty IsRootProperty = BindableProperty.Create(nameof(IsRoot), typeof(bool), typeof(NavigationBar), true, propertyChanged: OnIsRootChanged);

    #endregion Fields

    #region Properties

    /// <summary>
    /// Título exibido na barra de navegação.
    /// </summary>
    public string Title {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    /// <summary>
    /// Coleção de itens personalizados (ícones, botões etc.) a serem exibidos na barra de navegação.
    /// </summary>
    public VerticalStackLayout ToolbarItems {
        get => (VerticalStackLayout)GetValue(ToolbarItemsProperty);
        set => SetValue(ToolbarItemsProperty, value);
    }

    /// <summary>
    /// Define se o botão de voltar deve ser exibido.
    /// </summary>
    public bool IsBackButton {
        get => (bool)GetValue(IsBackButtonProperty);
        set => SetValue(IsBackButtonProperty, value);
    }

    /// <summary>
    /// Define se a página atual é a raiz da navegação.
    /// </summary>
    public bool IsRoot {
        get => (bool)GetValue(IsRootProperty);
        set => SetValue(IsRootProperty, value);
    }

    #endregion Properties

    #region Events

    /// <summary>
    /// Evento disparado quando o botão de voltar é clicado (em páginas que não são raiz).
    /// </summary>
    public event EventHandler Clicked;

    #endregion Events

    #region Constructor

    /// <summary>
    /// Inicializa uma nova instância da <see cref="NavigationBar"/>.
    /// </summary>
    public NavigationBar() {
        // Inicializa os componentes definidos no XAML.
        InitializeComponent();
    }

    #endregion Constructor

    #region Methods

    /// <summary>
    /// Manipula a alteração da propriedade <see cref="Title"/> e atualiza o texto exibido.
    /// </summary>
    /// <param name="bindable">Instância do controle que sofreu alteração.</param>
    /// <param name="oldValue">Valor anterior do título.</param>
    /// <param name="newValue">Novo valor do título.</param>
    private static void OnTitleChanged(BindableObject bindable, object oldValue, object newValue) {
        // Verifica se o controle é do tipo NavigationBar.
        if(bindable is not NavigationBar control) { return; }
        // Atualiza o texto do título.
        control.title.Text = newValue.ToString();
    }

    /// <summary>
    /// Manipula a alteração dos itens da toolbar e atualiza a visualização.
    /// </summary>
    /// <param name="bindable">Instância do controle.</param>
    /// <param name="oldValue">Valor anterior.</param>
    /// <param name="newValue">Novo valor (layout com itens).</param>
    private static void OnToolbarItemsChanged(BindableObject bindable, object oldValue, object newValue) {
        // Verifica o tipo do controle.
        if(bindable is not NavigationBar control) { return; }

        // Verifica se os novos itens são válidos.
        if(newValue is VerticalStackLayout items) {
            // Limpa itens existentes.
            control.toolBarItemsView.Children.Clear();

            // Adiciona os novos itens.
            foreach(var item in items.Children) {
                // Verifica se o item é do tipo View.
                control.toolBarItemsView.Children.Add(item);
            }
        }
    }

    /// <summary>
    /// Manipula a visibilidade do botão de voltar.
    /// </summary>
    /// <param name="bindable">Instância do controle.</param>
    /// <param name="oldValue">Valor anterior.</param>
    /// <param name="newValue">Novo valor booleano.</param>
    private static void OnIsBackButtonChanged(BindableObject bindable, object oldValue, object newValue) {
        // Verifica se o controle é do tipo NavigationBar.
        if(bindable is not NavigationBar control) { return; }
        // Define a visibilidade do botão.
        control.icon.IsVisible = (bool)newValue;
    }

    /// <summary>
    /// Atualiza o ícone do botão (menu ou voltar) conforme o estado raiz.
    /// </summary>
    /// <param name="bindable">Instância do controle.</param>
    /// <param name="oldValue">Valor anterior.</param>
    /// <param name="newValue">Novo valor booleano.</param>
    private static void OnIsRootChanged(BindableObject bindable, object oldValue, object newValue) {
        // Verifica se o controle é do tipo NavigationBar.
        if(bindable is not NavigationBar control) { return; }
        // Atualiza o ícone: menu (☰) se for raiz; seta (←) se não for.
        control.glyph.Text = (bool)newValue ? "\xf0c9" : "\xf060";
    }

    /// <summary>
    /// Manipula o toque no botão esquerdo. Exibe o menu ou dispara o evento de voltar.
    /// </summary>
    /// <param name="sender">Objeto que disparou o evento.</param>
    /// <param name="e">Argumentos do evento.</param>
    private void BackButton_Clicked(object sender, EventArgs e) {
        // Se for a página raiz, exibe o menu lateral.
        if(IsRoot) {
            // Verifica se o menu está disponível.
            Shell.Current.FlyoutIsPresented = true;
        } else {
            // Caso contrário, dispara o evento Clicked.
            Clicked?.Invoke(sender, e);
        }
    }

    #endregion Methods
}