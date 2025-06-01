using Finance.Delegates;

namespace Finance.Controls.Cells;

/// <summary>
/// Representa uma c�lula visual que exibe informa��es sobre um grupo de ativos,
/// incluindo nome, quantidade de ativos e um aviso caso esteja vazio.
/// </summary>
public partial class AssetGroupCell : ContentView {

    #region Fields

    /// <summary>
    /// Propriedade vincul�vel para armazenar o nome do grupo de ativos. Sempre que alterado, dispara o m�todo OnNameChanged.
    /// </summary>
    public static readonly BindableProperty NameProperty = BindableProperty.Create(nameof(Name), typeof(string), typeof(AssetGroupCell), string.Empty, propertyChanged: OnNameChanged);

    /// <summary>
    /// Propriedade vincul�vel para armazenar a quantidade de ativos no grupo. Sempre que alterado, dispara o m�todo OnCountChanged.
    /// </summary>
    public static readonly BindableProperty CountProperty = BindableProperty.Create(nameof(Count), typeof(int?), typeof(AssetGroupCell), null, propertyChanged: OnCountChanged);

    /// <summary>
    /// Propriedade vincul�vel para armazenar o valor de ativo de um grupo. Sempre que alterado, dispara o m�todo OnIsCheckedChanged.
    /// </summary>
    public static readonly BindableProperty IsCheckedProperty = BindableProperty.Create(nameof(IsChecked), typeof(bool), typeof(AssetGroupCell), false, defaultBindingMode: BindingMode.TwoWay, propertyChanged: OnIsCheckedChanged);

    #endregion Fields

    #region Properties

    /// <summary>
    /// Obt�m ou define o nome do grupo de ativos exibido na c�lula.
    /// </summary>
    public string Name {
        get => (string)GetValue(NameProperty);
        set => SetValue(NameProperty, value);
    }

    /// <summary>
    /// Obt�m ou define a quantidade de ativos no grupo exibido na c�lula.
    /// </summary>
    public int? Count {
        get => (int?)GetValue(CountProperty);
        set => SetValue(CountProperty, value);
    }

    /// <summary>
    /// Obt�m ou define o valor de ativo ou desativo do grupo de ativos.
    /// </summary>
    public bool IsChecked {
        get => (bool)GetValue(IsCheckedProperty);
        set => SetValue(IsCheckedProperty, value);
    }

    #endregion Properties

    #region Events

    /// <summary>
    /// Dispare quando o status do grupo de ativos for alterado.
    /// </summary>
    public event EnabledAssetGroupEventHandler CheckedChanged;

    #endregion Events

    #region Constructor

    /// <summary>
    /// Inicializa uma nova inst�ncia de <see cref="AssetGroupCell"/>.
    /// Configura os componentes visuais definidos no XAML.
    /// </summary>
    public AssetGroupCell() {
        // Inicializa os componentes definidos na interface XAML.
        InitializeComponent();
    }

    #endregion Constructor

    #region Methods

    /// <summary>
    /// M�todo chamado automaticamente quando a propriedade Name for alterada.
    /// Atualiza o texto do componente visual correspondente.
    /// </summary>
    /// <param name="bindable">Inst�ncia da c�lula que sofreu a altera��o.</param>
    /// <param name="oldValue">Valor antigo da propriedade.</param>
    /// <param name="newValue">Novo valor da propriedade.</param>
    private static void OnNameChanged(BindableObject bindable, object oldValue, object newValue) {
        // Garante que o bindable � uma inst�ncia v�lida de AssetGroupCell e que o novo valor � uma string.
        if(bindable is not AssetGroupCell control || newValue is not string value) { return; }
        // Atualiza o texto do componente nameText para refletir o novo nome.
        control.nameText.Text = value;
    }

    /// <summary>
    /// M�todo chamado automaticamente quando a propriedade Count for alterada.
    /// Atualiza o texto da quantidade de ativos e a visibilidade do aviso.
    /// </summary>
    /// <param name="bindable">Inst�ncia da c�lula que sofreu a altera��o.</param>
    /// <param name="oldValue">Valor antigo da propriedade.</param>
    /// <param name="newValue">Novo valor da propriedade.</param>
    private static void OnCountChanged(BindableObject bindable, object oldValue, object newValue) {
        // Garante que o bindable � uma inst�ncia v�lida de AssetGroupCell e que o novo valor � um inteiro.
        if(bindable is not AssetGroupCell control || newValue is not int value) { return; }
        // Exibe ou oculta a mensagem de aviso dependendo se a quantidade � zero ou negativa.
        control.warningText.IsVisible = value <= 0;
        // Atualiza o texto do componente countText para refletir a nova quantidade de ativos.
        control.countText.Text = value.ToString();
    }

    /// <summary>
    /// M�todo chamado automaticamente quando a propriedade IsChecked for alterada.
    /// Atualiza o valor do interruptor que ativa ou desativa o grupo de ativos.
    /// </summary>
    /// <param name="bindable">Inst�ncia da c�lula que sofreu a altera��o.</param>
    /// <param name="oldValue">Valor antigo da propriedade.</param>
    /// <param name="newValue">Novo valor da propriedade.</param>
    private static void OnIsCheckedChanged(BindableObject bindable, object oldValue, object newValue) {
        // Verifica se o bindable � do tipo AssetGroupCell.
        if(bindable is not AssetGroupCell control) { return; }
        // Define o valor interruptor do grupo de dativos.
        control.checkedSwitch.IsChecked = (bool)newValue;
    }

    /// <summary>
    /// M�todo chamado quando o usu�rio interage com o interruptor de ativa��o do grupo de ativos.
    /// Atualiza o valor do interruptor com base na altera��o do usu�rio.
    /// </summary>
    /// <param name="sender">Objeto que chamou este evento.</param>
    /// <param name="e">Os argumentos do evento.</param>
    private void CheckedSwitch_CheckedChanged(object sender, ValueChangedEventArgs<bool> e) {
        // Define o valor do interruptor do grupo de dativos.
        IsChecked = e.NewValue;
        // Dispara o evento Changed para notificar que o grupo de ativos foi alterado.
        CheckedChanged?.Invoke(Name);
    }

    #endregion Methods
}