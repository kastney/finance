using Finance.Enumerations;
using Finance.Utilities;

namespace Finance.Controls.Cells;

/// <summary>
/// Representa uma c�lula visual que exibe uma notifica��o da carteira.
/// </summary>
public partial class NotificationCell : ContentView {

    #region Fields

    /// <summary>
    /// Propriedade vincul�vel para armazenar o c�digo de identifica��o da notifica��o.
    /// </summary>
    public static readonly BindableProperty CodeProperty = BindableProperty.Create(nameof(Code), typeof(NotificationCodes?), typeof(NotificationCell), null);

    /// <summary>
    /// Propriedade vincul�vel para armazenar a chave da notifica��o.
    /// </summary>
    public static readonly BindableProperty KeyProperty = BindableProperty.Create(nameof(Key), typeof(string), typeof(NotificationCell), null);

    /// <summary>
    /// Propriedade vincul�vel para armazenar o �cone da notifica��o. Sempre que alterado, dispara o m�todo <c>OnIconChanged</c>.
    /// </summary>
    public static readonly BindableProperty IconProperty = BindableProperty.Create(nameof(Icon), typeof(string), typeof(NotificationCell), string.Empty, propertyChanged: OnIconChanged);

    /// <summary>
    /// Propriedade vincul�vel para armazenar o t�tulo da notifica��o. Sempre que alterado, dispara o m�todo <c>OnTitleChanged</c>.
    /// </summary>
    public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(NotificationCell), string.Empty, propertyChanged: OnTitleChanged);

    /// <summary>
    /// Propriedade vincul�vel para armazenar a descri��o da notifica��o. Sempre que alterado, dispara o m�todo <c>OnDescriptionChanged</c>.
    /// </summary>
    public static readonly BindableProperty DescriptionProperty = BindableProperty.Create(nameof(Description), typeof(string), typeof(NotificationCell), string.Empty, propertyChanged: OnDescriptionChanged);

    /// <summary>
    /// Propriedade vincul�vel para armazenar a tag de categoria da notifica��o. Sempre que alterado, dispara o m�todo <c>OnTagChanged</c>.
    /// </summary>
    public static readonly BindableProperty TagProperty = BindableProperty.Create(nameof(Tag), typeof(string), typeof(NotificationCell), string.Empty, propertyChanged: OnTagChanged);

    /// <summary>
    /// Propriedade vincul�vel para armazenar o n�vel de severidade da notifica��o. Sempre que alterado, dispara o m�todo <c>OnLevelChanged</c>.
    /// </summary>
    public static readonly BindableProperty LevelProperty = BindableProperty.Create(nameof(Level), typeof(NotificationLevel?), typeof(NotificationCell), null, propertyChanged: OnLevelChanged);

    #endregion Fields

    #region Properties

    /// <summary>
    /// O identificador da notifica��o.
    /// </summary>
    public NotificationCodes? Code {
        get => (NotificationCodes?)GetValue(CodeProperty);
        set => SetValue(CodeProperty, value);
    }

    /// <summary>
    /// A chave da notifica��o.
    /// </summary>
    public string Key {
        get => (string)GetValue(KeyProperty);
        set => SetValue(KeyProperty, value);
    }

    /// <summary>
    /// Obt�m ou define o �cone da notifica��o.
    /// </summary>
    public string Icon {
        get => (string)GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    /// <summary>
    /// Obt�m ou define o t�tulo da notifica��o.
    /// </summary>
    public string Title {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    /// <summary>
    /// Obt�m ou define a descri��o da notifica��o.
    /// </summary>
    public string Description {
        get => (string)GetValue(DescriptionProperty);
        set => SetValue(DescriptionProperty, value);
    }

    /// <summary>
    /// Obt�m ou define a tag de categoria da notifica��o.
    /// </summary>
    public string Tag {
        get => (string)GetValue(TagProperty);
        set => SetValue(TagProperty, value);
    }

    /// <summary>
    /// Obt�m ou define o n�vel de severidade da notifica��o.
    /// </summary>
    public NotificationLevel? Level {
        get => (NotificationLevel?)GetValue(LevelProperty);
        set => SetValue(LevelProperty, value);
    }

    #endregion Properties

    #region Events

    /// <summary>
    /// Evento disparado quando o bot�o for clicado.
    /// </summary>
    public event EventHandler Clicked;

    #endregion Events

    #region Constructor

    /// <summary>
    /// Inicializa uma nova inst�ncia de <see cref="NotificationCell"/>.
    /// Configura os componentes visuais definidos no XAML.
    /// </summary>
    public NotificationCell() {
        // Inicializa os componentes definidos na interface XAML.
        InitializeComponent();
        // Registra o evento de mudan�a de tema do aplicativo para atualizar a apar�ncia conforme o tema atual.
        Application.Current.RequestedThemeChanged += Current_RequestedThemeChanged;
    }

    #endregion Constructor

    #region Methods

    /// <summary>
    /// M�todo chamado automaticamente quando a propriedade <c>Icon</c> for alterada.
    /// Atualiza o texto do componente visual correspondente.
    /// </summary>
    /// <param name="bindable">Inst�ncia da c�lula que sofreu a altera��o.</param>
    /// <param name="oldValue">Valor antigo da propriedade.</param>
    /// <param name="newValue">Novo valor da propriedade.</param>
    private static void OnIconChanged(BindableObject bindable, object oldValue, object newValue) {
        // Garante que o bindable � uma inst�ncia v�lida de NotificationCell e que o novo valor � uma string.
        if(bindable is not NotificationCell control || newValue is not string value) { return; }
        // Atualiza o texto do componente iconText para refletir o novo nome.
        control.iconText.Text = value;
    }

    /// <summary>
    /// M�todo chamado automaticamente quando a propriedade <c>Title</c> for alterada.
    /// Atualiza o texto do componente visual correspondente.
    /// </summary>
    /// <param name="bindable">Inst�ncia da c�lula que sofreu a altera��o.</param>
    /// <param name="oldValue">Valor antigo da propriedade.</param>
    /// <param name="newValue">Novo valor da propriedade.</param>
    private static void OnTitleChanged(BindableObject bindable, object oldValue, object newValue) {
        // Garante que o bindable � uma inst�ncia v�lida de NotificationCell e que o novo valor � uma string.
        if(bindable is not NotificationCell control || newValue is not string value) { return; }

        // Verifica se a propriedade Key est� definida e substitui a chave no texto do t�tulo, se necess�rio.
        if(!string.IsNullOrEmpty(control.Key)) {
            // Substitui a chave no texto do t�tulo, se a propriedade Key estiver definida.
            value = value.Replace("{key}", control.Key, StringComparison.OrdinalIgnoreCase);
        }

        // Atualiza o texto do componente titleText para refletir o novo nome.
        control.titleText.Text = value;
    }

    /// <summary>
    /// M�todo chamado automaticamente quando a propriedade <c>Description</c> for alterada.
    /// Atualiza o texto do componente visual correspondente.
    /// </summary>
    /// <param name="bindable">Inst�ncia da c�lula que sofreu a altera��o.</param>
    /// <param name="oldValue">Valor antigo da propriedade.</param>
    /// <param name="newValue">Novo valor da propriedade.</param>
    private static void OnDescriptionChanged(BindableObject bindable, object oldValue, object newValue) {
        // Garante que o bindable � uma inst�ncia v�lida de NotificationCell e que o novo valor � uma string.
        if(bindable is not NotificationCell control || newValue is not string value) { return; }

        // Verifica se a propriedade Key est� definida e substitui a chave no texto do t�tulo, se necess�rio.
        if(!string.IsNullOrEmpty(control.Key)) {
            // Substitui a chave no texto do t�tulo, se a propriedade Key estiver definida.
            value = value.Replace("{key}", control.Key, StringComparison.OrdinalIgnoreCase);
        }

        // Atualiza o texto do componente descriptionText para refletir o novo nome.
        control.descriptionText.Text = value;
    }

    /// <summary>
    /// M�todo chamado automaticamente quando a propriedade <c>Tag</c> for alterada.
    /// Atualiza o texto do componente visual correspondente.
    /// </summary>
    /// <param name="bindable">Inst�ncia da c�lula que sofreu a altera��o.</param>
    /// <param name="oldValue">Valor antigo da propriedade.</param>
    /// <param name="newValue">Novo valor da propriedade.</param>
    private static void OnTagChanged(BindableObject bindable, object oldValue, object newValue) {
        // Garante que o bindable � uma inst�ncia v�lida de NotificationCell e que o novo valor � uma string.
        if(bindable is not NotificationCell control || newValue is not string value) { return; }
        // Atualiza o texto do componente tagText para refletir o novo nome.
        control.tagText.Text = value;
    }

    /// <summary>
    /// M�todo chamado automaticamente quando a propriedade <c>Level</c> for alterada.
    /// Atualiza o texto do componente visual correspondente.
    /// </summary>
    /// <param name="bindable">Inst�ncia da c�lula que sofreu a altera��o.</param>
    /// <param name="oldValue">Valor antigo da propriedade.</param>
    /// <param name="newValue">Novo valor da propriedade.</param>
    private static void OnLevelChanged(BindableObject bindable, object oldValue, object newValue) {
        // Garante que o bindable � uma inst�ncia v�lida de NotificationCell e que o novo valor � uma string.
        if(bindable is not NotificationCell control || newValue is not NotificationLevel value) { return; }
        // Atualiza a cor do componente conforme o n�vel de severidade.
        ApplyColor(control.titleText, value);
    }

    /// <summary>
    /// Atualiza as cores do t�tulo ao mudar o tema do sistema.
    /// </summary>
    /// <param name="sender">Objeto que disparou o evento.</param>
    /// <param name="e">Argumentos da mudan�a de tema.</param>
    private void Current_RequestedThemeChanged(object sender, AppThemeChangedEventArgs e) {
        // Ont�m o n�vel de severidade.
        var level = Level is null ? NotificationLevel.Infor : Level.Value;
        // Aplica a cor na c�lula.
        ApplyColor(titleText, level);
    }

    /// <summary>
    /// Aplica uma cor a um r�tulo (<see cref="Label"/>) com base no valor fornecido.
    /// </summary>
    /// <param name="label">R�tulo que receber� a cor.</param>
    /// <param name="level">O n�vel de severidade da notifica��o.</param>
    private static void ApplyColor(Label label, NotificationLevel level) {
        // Atualiza a cor do componente conforme o n�vel de severidade.
        switch(level) {
            case NotificationLevel.Infor: {
                // Atualiza a cor para a notifica��o com o n�vel de severidade de informa��o.
                label.TextColor = Application.Current.RequestedTheme == AppTheme.Light ? StaticResourceUtility.Get<Color>("TextColor") : StaticResourceUtility.Get<Color>("TextColorDark");
                break;
            }
            case NotificationLevel.Warning: {
                // Atualiza a cor para a notifica��o com o n�vel de severidade de aten��o.
                label.TextColor = Application.Current.RequestedTheme == AppTheme.Light ? StaticResourceUtility.Get<Color>("Warning") : StaticResourceUtility.Get<Color>("WarningDark");
                break;
            }
        }
    }

    /// <summary>
    /// Manipula o evento de toque no bot�o.
    /// </summary>
    /// <param name="sender">Objeto que disparou o evento.</param>
    /// <param name="e">Dados do evento de toque.</param>
    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e) {
        // Dispara o evento Clicked para notificar listeners externos.
        Clicked?.Invoke(this, e);
    }

    #endregion Methods
}