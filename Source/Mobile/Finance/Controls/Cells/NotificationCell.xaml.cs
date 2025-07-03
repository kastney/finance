using Finance.Enumerations;
using Finance.Utilities;

namespace Finance.Controls.Cells;

/// <summary>
/// Representa uma célula visual que exibe uma notificação da carteira.
/// </summary>
public partial class NotificationCell : ContentView {

    #region Fields

    /// <summary>
    /// Propriedade vinculável para armazenar o código de identificação da notificação.
    /// </summary>
    public static readonly BindableProperty CodeProperty = BindableProperty.Create(nameof(Code), typeof(NotificationCodes?), typeof(NotificationCell), null);

    /// <summary>
    /// Propriedade vinculável para armazenar a chave da notificação.
    /// </summary>
    public static readonly BindableProperty KeyProperty = BindableProperty.Create(nameof(Key), typeof(string), typeof(NotificationCell), null);

    /// <summary>
    /// Propriedade vinculável para armazenar o ícone da notificação. Sempre que alterado, dispara o método <c>OnIconChanged</c>.
    /// </summary>
    public static readonly BindableProperty IconProperty = BindableProperty.Create(nameof(Icon), typeof(string), typeof(NotificationCell), string.Empty, propertyChanged: OnIconChanged);

    /// <summary>
    /// Propriedade vinculável para armazenar o título da notificação. Sempre que alterado, dispara o método <c>OnTitleChanged</c>.
    /// </summary>
    public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(NotificationCell), string.Empty, propertyChanged: OnTitleChanged);

    /// <summary>
    /// Propriedade vinculável para armazenar a descrição da notificação. Sempre que alterado, dispara o método <c>OnDescriptionChanged</c>.
    /// </summary>
    public static readonly BindableProperty DescriptionProperty = BindableProperty.Create(nameof(Description), typeof(string), typeof(NotificationCell), string.Empty, propertyChanged: OnDescriptionChanged);

    /// <summary>
    /// Propriedade vinculável para armazenar a tag de categoria da notificação. Sempre que alterado, dispara o método <c>OnTagChanged</c>.
    /// </summary>
    public static readonly BindableProperty TagProperty = BindableProperty.Create(nameof(Tag), typeof(string), typeof(NotificationCell), string.Empty, propertyChanged: OnTagChanged);

    /// <summary>
    /// Propriedade vinculável para armazenar o nível de severidade da notificação. Sempre que alterado, dispara o método <c>OnLevelChanged</c>.
    /// </summary>
    public static readonly BindableProperty LevelProperty = BindableProperty.Create(nameof(Level), typeof(NotificationLevel?), typeof(NotificationCell), null, propertyChanged: OnLevelChanged);

    #endregion Fields

    #region Properties

    /// <summary>
    /// O identificador da notificação.
    /// </summary>
    public NotificationCodes? Code {
        get => (NotificationCodes?)GetValue(CodeProperty);
        set => SetValue(CodeProperty, value);
    }

    /// <summary>
    /// A chave da notificação.
    /// </summary>
    public string Key {
        get => (string)GetValue(KeyProperty);
        set => SetValue(KeyProperty, value);
    }

    /// <summary>
    /// Obtém ou define o ícone da notificação.
    /// </summary>
    public string Icon {
        get => (string)GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    /// <summary>
    /// Obtém ou define o título da notificação.
    /// </summary>
    public string Title {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    /// <summary>
    /// Obtém ou define a descrição da notificação.
    /// </summary>
    public string Description {
        get => (string)GetValue(DescriptionProperty);
        set => SetValue(DescriptionProperty, value);
    }

    /// <summary>
    /// Obtém ou define a tag de categoria da notificação.
    /// </summary>
    public string Tag {
        get => (string)GetValue(TagProperty);
        set => SetValue(TagProperty, value);
    }

    /// <summary>
    /// Obtém ou define o nível de severidade da notificação.
    /// </summary>
    public NotificationLevel? Level {
        get => (NotificationLevel?)GetValue(LevelProperty);
        set => SetValue(LevelProperty, value);
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
    /// Inicializa uma nova instância de <see cref="NotificationCell"/>.
    /// Configura os componentes visuais definidos no XAML.
    /// </summary>
    public NotificationCell() {
        // Inicializa os componentes definidos na interface XAML.
        InitializeComponent();
        // Registra o evento de mudança de tema do aplicativo para atualizar a aparência conforme o tema atual.
        Application.Current.RequestedThemeChanged += Current_RequestedThemeChanged;
    }

    #endregion Constructor

    #region Methods

    /// <summary>
    /// Método chamado automaticamente quando a propriedade <c>Icon</c> for alterada.
    /// Atualiza o texto do componente visual correspondente.
    /// </summary>
    /// <param name="bindable">Instância da célula que sofreu a alteração.</param>
    /// <param name="oldValue">Valor antigo da propriedade.</param>
    /// <param name="newValue">Novo valor da propriedade.</param>
    private static void OnIconChanged(BindableObject bindable, object oldValue, object newValue) {
        // Garante que o bindable é uma instância válida de NotificationCell e que o novo valor é uma string.
        if(bindable is not NotificationCell control || newValue is not string value) { return; }
        // Atualiza o texto do componente iconText para refletir o novo nome.
        control.iconText.Text = value;
    }

    /// <summary>
    /// Método chamado automaticamente quando a propriedade <c>Title</c> for alterada.
    /// Atualiza o texto do componente visual correspondente.
    /// </summary>
    /// <param name="bindable">Instância da célula que sofreu a alteração.</param>
    /// <param name="oldValue">Valor antigo da propriedade.</param>
    /// <param name="newValue">Novo valor da propriedade.</param>
    private static void OnTitleChanged(BindableObject bindable, object oldValue, object newValue) {
        // Garante que o bindable é uma instância válida de NotificationCell e que o novo valor é uma string.
        if(bindable is not NotificationCell control || newValue is not string value) { return; }

        // Verifica se a propriedade Key está definida e substitui a chave no texto do título, se necessário.
        if(!string.IsNullOrEmpty(control.Key)) {
            // Substitui a chave no texto do título, se a propriedade Key estiver definida.
            value = value.Replace("{key}", control.Key, StringComparison.OrdinalIgnoreCase);
        }

        // Atualiza o texto do componente titleText para refletir o novo nome.
        control.titleText.Text = value;
    }

    /// <summary>
    /// Método chamado automaticamente quando a propriedade <c>Description</c> for alterada.
    /// Atualiza o texto do componente visual correspondente.
    /// </summary>
    /// <param name="bindable">Instância da célula que sofreu a alteração.</param>
    /// <param name="oldValue">Valor antigo da propriedade.</param>
    /// <param name="newValue">Novo valor da propriedade.</param>
    private static void OnDescriptionChanged(BindableObject bindable, object oldValue, object newValue) {
        // Garante que o bindable é uma instância válida de NotificationCell e que o novo valor é uma string.
        if(bindable is not NotificationCell control || newValue is not string value) { return; }

        // Verifica se a propriedade Key está definida e substitui a chave no texto do título, se necessário.
        if(!string.IsNullOrEmpty(control.Key)) {
            // Substitui a chave no texto do título, se a propriedade Key estiver definida.
            value = value.Replace("{key}", control.Key, StringComparison.OrdinalIgnoreCase);
        }

        // Atualiza o texto do componente descriptionText para refletir o novo nome.
        control.descriptionText.Text = value;
    }

    /// <summary>
    /// Método chamado automaticamente quando a propriedade <c>Tag</c> for alterada.
    /// Atualiza o texto do componente visual correspondente.
    /// </summary>
    /// <param name="bindable">Instância da célula que sofreu a alteração.</param>
    /// <param name="oldValue">Valor antigo da propriedade.</param>
    /// <param name="newValue">Novo valor da propriedade.</param>
    private static void OnTagChanged(BindableObject bindable, object oldValue, object newValue) {
        // Garante que o bindable é uma instância válida de NotificationCell e que o novo valor é uma string.
        if(bindable is not NotificationCell control || newValue is not string value) { return; }
        // Atualiza o texto do componente tagText para refletir o novo nome.
        control.tagText.Text = value;
    }

    /// <summary>
    /// Método chamado automaticamente quando a propriedade <c>Level</c> for alterada.
    /// Atualiza o texto do componente visual correspondente.
    /// </summary>
    /// <param name="bindable">Instância da célula que sofreu a alteração.</param>
    /// <param name="oldValue">Valor antigo da propriedade.</param>
    /// <param name="newValue">Novo valor da propriedade.</param>
    private static void OnLevelChanged(BindableObject bindable, object oldValue, object newValue) {
        // Garante que o bindable é uma instância válida de NotificationCell e que o novo valor é uma string.
        if(bindable is not NotificationCell control || newValue is not NotificationLevel value) { return; }
        // Atualiza a cor do componente conforme o nível de severidade.
        ApplyColor(control.titleText, value);
    }

    /// <summary>
    /// Atualiza as cores do título ao mudar o tema do sistema.
    /// </summary>
    /// <param name="sender">Objeto que disparou o evento.</param>
    /// <param name="e">Argumentos da mudança de tema.</param>
    private void Current_RequestedThemeChanged(object sender, AppThemeChangedEventArgs e) {
        // Ontém o nível de severidade.
        var level = Level is null ? NotificationLevel.Infor : Level.Value;
        // Aplica a cor na célula.
        ApplyColor(titleText, level);
    }

    /// <summary>
    /// Aplica uma cor a um rótulo (<see cref="Label"/>) com base no valor fornecido.
    /// </summary>
    /// <param name="label">Rótulo que receberá a cor.</param>
    /// <param name="level">O nível de severidade da notificação.</param>
    private static void ApplyColor(Label label, NotificationLevel level) {
        // Atualiza a cor do componente conforme o nível de severidade.
        switch(level) {
            case NotificationLevel.Infor: {
                // Atualiza a cor para a notificação com o nível de severidade de informação.
                label.TextColor = Application.Current.RequestedTheme == AppTheme.Light ? StaticResourceUtility.Get<Color>("TextColor") : StaticResourceUtility.Get<Color>("TextColorDark");
                break;
            }
            case NotificationLevel.Warning: {
                // Atualiza a cor para a notificação com o nível de severidade de atenção.
                label.TextColor = Application.Current.RequestedTheme == AppTheme.Light ? StaticResourceUtility.Get<Color>("Warning") : StaticResourceUtility.Get<Color>("WarningDark");
                break;
            }
        }
    }

    /// <summary>
    /// Manipula o evento de toque no botão.
    /// </summary>
    /// <param name="sender">Objeto que disparou o evento.</param>
    /// <param name="e">Dados do evento de toque.</param>
    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e) {
        // Dispara o evento Clicked para notificar listeners externos.
        Clicked?.Invoke(this, e);
    }

    #endregion Methods
}