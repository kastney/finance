using Finance.Delegates;
using Finance.Services.Walleting;
using Finance.Utilities;

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

    /// <summary>
    /// Propriedade vincul�vel para armazenar o valor da porcentagem do grupo de ativos. Sempre que alterado, dispara o m�todo OnPercentageChanged.
    /// </summary>
    public static readonly BindableProperty PercentageProperty = BindableProperty.Create(nameof(Percentage), typeof(int), typeof(AssetGroupCell), -1, defaultBindingMode: BindingMode.TwoWay, propertyChanged: OnPercentageChanged);

    /// <summary>
    /// Propriedade vincul�vel para armazenar o valor da porcentagem dispon�vel para ser utilizado entre os grupos de ativos.
    /// </summary>
    public static readonly BindableProperty PercentageAvailableProperty = BindableProperty.Create(nameof(PercentageAvailable), typeof(int), typeof(AssetGroupCell), 100, propertyChanged: OnPercentageAvailableChanged);

    /// <summary>
    /// Propriedade vincul�vel para armazenar a cor do grupo de ativos. Sempre que alterado, dispara o m�todo OnColorChanged.
    /// </summary>
    public static readonly BindableProperty ColorProperty = BindableProperty.Create(nameof(Color), typeof(int?), typeof(AssetGroupCell), null, defaultBindingMode: BindingMode.TwoWay, propertyChanged: OnColorChanged);

    /// <summary>
    /// O valor m�xima da porcentage para ser selecionado no slider.
    /// </summary>
    private double maximumPercentageSlider;

    /// <summary>
    /// Flag para controlar se o usu�rio est� clicando no slider.
    /// </summary>
    private bool isPressedSlider;

    /// <summary>
    /// O valor atual da porcentagem do grupo de ativos.
    /// </summary>
    private int currentPercentage;

    /// <summary>
    /// Indica se a c�lula est� atualmente em execu��o ou n�o, usado para evitar m�ltiplas execu��es simult�neas de eventos.
    /// </summary>
    private bool isRunning;

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

    /// <summary>
    /// Obt�m ou define o valor da porcentagem do grupo de ativos.
    /// </summary>
    public int Percentage {
        get => (int)GetValue(PercentageProperty);
        set => SetValue(PercentageProperty, value);
    }

    /// <summary>
    /// Obt�m ou define o valor da porcentagem dispon�vel para ser utilizado entre os grupos de ativos.
    /// </summary>
    public int PercentageAvailable {
        get => (int)GetValue(PercentageAvailableProperty);
        set => SetValue(PercentageAvailableProperty, value);
    }

    /// <summary>
    /// Obt�m ou define a cor do grupo de ativos.
    /// </summary>
    public int? Color {
        get => (int?)GetValue(ColorProperty);
        set => SetValue(ColorProperty, value);
    }

    #endregion Properties

    #region Events

    /// <summary>
    /// Dispare quando o status do grupo de ativos for alterado.
    /// </summary>
    public event ValueAssetGroupEventHanler CheckedChanged;

    /// <summary>
    /// Dispara quando a porcentagem do grupo de ativos for alterado.
    /// </summary>
    public event ValueAssetGroupEventHanler PercentageChanged;

    /// <summary>
    /// Dispara quando a cor do grupo de ativos for alterado.
    /// </summary>
    public event ValueAssetGroupEventHanler ColorChanged;

    /// <summary>
    /// Dispara quando o usu�rio aperta o bot�o de renomear.
    /// </summary>
    public event AssetGroupEventHanler RenameClicked;

    /// <summary>
    /// Dispara quando o usu�rio aperta o bot�o de deletar.
    /// </summary>
    public event AssetGroupEventHanler DeleteClicked;

    /// <summary>
    /// Dispara quando o usu�rio aperta o bot�o de abrir grupo.
    /// </summary>
    public event AssetGroupEventHanler OpenGroupClicked;

    #endregion Events

    #region Constructor

    /// <summary>
    /// Inicializa uma nova inst�ncia de <see cref="AssetGroupCell"/>.
    /// Configura os componentes visuais definidos no XAML.
    /// </summary>
    public AssetGroupCell() {
        // Inicializa os componentes definidos na interface XAML.
        InitializeComponent();
        // Obt�m todas as cores dispon�veis.
        colorSelector.ItemsSource = ColorUtility.GetColors();
    }

    #endregion Constructor

    #region Methods

    #region Name

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
        // Atualiza o texto do componente de aviso para refletir o novo nome.
        control.emptyGruopText.Text = control.percentageGruopText.Text = value;
    }

    #endregion Name

    #region Count

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

        // Atualiza o texto do componente countText para refletir a nova quantidade de ativos.
        control.countText.Text = value.ToString();

        // Define visibilidade do aviso de lista vazia.
        bool isEmpty = value <= 0;
        // Define se o aviso de lista vazia deve ser exibido ou n�o.
        control.emptyWarningText.IsVisible = isEmpty;

        // Define se o bot�o de exclus�o pode ser usado.
        control.deleteButton.IsEnabled = isEmpty;

        // Se n�o est� vazio, verifica aloca��o percentual.
        if(!isEmpty) {
            // Obt�m o servi�o de carteira para verificar a aloca��o percentual do grupo de ativos.
            var walletService = Service.Get<IWalletService>();
            // Encontra o grupo de ativos na estrat�gia da carteira com base no nome do controle.
            var group = walletService.Wallet.Strategy.FirstOrDefault(g => g.Name.Equals(control.Name));

            // Mostra aviso se houver percentual n�o alocado (> 0).
            bool hasUnallocated = group != null && (100 - group.Assets.Sum(a => a.Percentage)) > 0;

            // Define a visibilidade do texto de aviso de porcentagem baseado na aloca��o.
            control.percentageWarningText.IsVisible = hasUnallocated;
        } else {
            // Oculta o aviso de aloca��o se estiver vazio.
            control.percentageWarningText.IsVisible = false;
        }
    }

    #endregion Count

    #region Checked

    /// <summary>
    /// M�todo chamado automaticamente quando a propriedade IsChecked for alterada.
    /// Atualiza o valor do interruptor que ativa ou desativa o grupo de ativos.
    /// </summary>
    /// <param name="bindable">Inst�ncia da c�lula que sofreu a altera��o.</param>
    /// <param name="oldValue">Valor antigo da propriedade.</param>
    /// <param name="newValue">Novo valor da propriedade.</param>
    private static void OnIsCheckedChanged(BindableObject bindable, object oldValue, object newValue) {
        // Verifica se o bindable � do tipo AssetGroupCell.
        if(bindable is not AssetGroupCell control || newValue is not bool value) { return; }
        // Define o valor interruptor do grupo de dativos.
        control.checkedSwitch.IsChecked = value;
        // Define se o bot�o de estrat�gia est� ou n�o ativado para intera��o.
        control.percentageButton.IsEnabled = value;
    }

    /// <summary>
    /// M�todo chamado quando o usu�rio interage com o interruptor de ativa��o do grupo de ativos.
    /// Atualiza o valor do interruptor com base na altera��o do usu�rio.
    /// </summary>
    /// <param name="sender">Objeto que chamou este evento.</param>
    /// <param name="e">Os argumentos do evento.</param>
    private async void CheckedSwitch_CheckedChanged(object sender, ValueChangedEventArgs<bool> e) {
        // Verifica se o usu�rio quer de fato desativar o grupo de ativos.
        if(!IsChecked || await Shell.Current.DisplayAlert("Desativar grupo de ativos!", $"Ao desativar o grupo de ativos \"{Name}\", ele n�o ser� mais considerado nos c�lculos de balanceamento da sua carteira.\n\nTem certeza de que deseja desativ�-lo?", "Sim", "N�o")) {
            // Define o valor do interruptor do grupo de ativos.
            IsChecked = e.NewValue;

            // Salva o valor atual da porcentagem antes de come�ar o processo de mudan�a.
            currentPercentage = Percentage;
            // Define o valor da porcentagem do grupo.
            Percentage = 0;

            // Dispara o evento para notificar que o grupo de ativos foi alterado.
            CheckedChanged?.Invoke(Name, currentPercentage);
        } else {
            // Volta ao valor anterior do interruptor.
            checkedSwitch.IsChecked = IsChecked;
        }
    }

    #endregion Checked

    #region Percentage

    /// <summary>
    /// M�todo chamado automaticamente quando a propriedade Porcentage for alterada.
    /// Atualiza o valor do slider de porcentagem do grupo de ativos.
    /// </summary>
    /// <param name="bindable">Inst�ncia da c�lula que sofreu a altera��o.</param>
    /// <param name="oldValue">Valor antigo da propriedade.</param>
    /// <param name="newValue">Novo valor da propriedade.</param>
    private static void OnPercentageChanged(BindableObject bindable, object oldValue, object newValue) {
        // Verifica se o bindable � do tipo AssetGroupCell e define o valor da porcentagem do grupo de ativos.
        if(bindable is not AssetGroupCell control || newValue is not int value) { return; }

        // Normaliza��o do valor da porcentagem.
        double percentage = value < 0 ? 0 : value > 100 ? 100 : value;

        // Define o valor no slider de porcentagem.
        control.percentageSlider.Value = percentage / 100;
        // Define o valor do texto informativo no card.
        control.percentageText.Text = $"{value}%";

        // Atualiza��o do porcentagem m�xima selecion�vel no slider.
        control.maximumPercentageSlider = (double)(control.Percentage + control.PercentageAvailable) / 100;
    }

    /// <summary>
    /// M�todo chamado automaticamente quando a propriedade PorcentageAvailable for alterada.
    /// Atualiza o valor da porcentagem dispon�vel para ser utilizado entre os grupos de ativos.
    /// </summary>
    /// <param name="bindable">Inst�ncia da c�lula que sofreu a altera��o.</param>
    /// <param name="oldValue">Valor antigo da propriedade.</param>
    /// <param name="newValue">Novo valor da propriedade.</param>
    private static void OnPercentageAvailableChanged(BindableObject bindable, object oldValue, object newValue) {
        // Verifica se o bindable � do tipo AssetGroupCell e define o valor da porcentagem dispon�vel para os grupos de ativos.
        if(bindable is not AssetGroupCell control || newValue is not int value) { return; }
        // Normaliza��o do valor da porcentage dispon�vel.
        if(value < 0) {
            // Se for menor do que 0, ent�o deixa no menor valor poss�vel (que � o 0).
            control.PercentageAvailable = 0;
        } else if(value > 100) {
            // Se for maior do que 100, ent�o deixa no maior valor poss�vel (que � o 100).
            control.PercentageAvailable = 100;
        }

        // Atualiza��o do porcentagem m�xima selecion�vel no slider.
        control.maximumPercentageSlider = (double)(control.Percentage + control.PercentageAvailable) / 100;
    }

    /// <summary>
    /// Evento acionado sempre que o valor do slider de porcentagem for alterado.
    /// Respons�vel por validar se o valor atual excede o m�ximo permitido, e ajust�-lo caso necess�rio.
    /// </summary>
    /// <param name="sender">Objeto que disparou o evento, neste caso o pr�prio slider.</param>
    /// <param name="e">Argumentos do evento de altera��o de valor.</param>
    private void PercentageSlider_ValueChanged(object sender, EventArgs e) {
        // Verifica se o slider est� sendo pressionado e se o valor atual excede o m�ximo permitido.
        if(isPressedSlider && percentageSlider.Value > maximumPercentageSlider) {
            // Verifica se a diferen�a entre o valor atual e o m�ximo � significativa, evitando ajustes desnecess�rios devido a pequenas diferen�as de ponto flutuante.
            if(Math.Abs(percentageSlider.Value - maximumPercentageSlider) > 0.0001) {
                // Ajusta o valor do slider para o valor m�ximo permitido.
                percentageSlider.Value = maximumPercentageSlider;
            }
        }
    }

    /// <summary>
    /// Evento acionado quando o usu�rio inicia a intera��o com o slider (pressiona o controle).
    /// Define a flag de que o slider est� sendo pressionado e for�a a valida��o do valor atual.
    /// </summary>
    /// <param name="sender">Objeto que disparou o evento, neste caso o pr�prio slider.</param>
    /// <param name="e">Argumentos do evento de toque do DevExpress.</param>
    private void PercentageSlider_TapPressed(object sender, DXTapEventArgs e) {
        // Salva o valor atual da porcentagem antes de come�ar o processo de mudan�a.
        currentPercentage = Percentage;
        // Indica que o slider est� atualmente sendo pressionado.
        isPressedSlider = true;
        // For�a a valida��o imediata do valor atual do slider.
        PercentageSlider_ValueChanged(null, null);
    }

    /// <summary>
    /// Evento acionado quando o usu�rio finaliza a intera��o com o slider (solta o controle).
    /// Reseta a flag de intera��o e atualiza a propriedade Percentage se houver altera��o real no valor.
    /// </summary>
    /// <param name="sender">Objeto que disparou o evento, neste caso o pr�prio slider.</param>
    /// <param name="e">Argumentos do evento de toque do DevExpress.</param>
    private void PercentageSlider_TapReleased(object sender, DXTapEventArgs e) {
        // Indica que o slider n�o est� mais sendo pressionado.
        isPressedSlider = false;
        // Converte o valor atual do slider para um inteiro representando a porcentagem.
        var newPercentage = (int)(percentageSlider.Value * 100);
        // Se a porcentagem atual for diferente da armazenada, atualiza a propriedade Percentage.
        if(Percentage != newPercentage) {
            // Atualiza a propriedade Percentage com o novo valor.
            Percentage = newPercentage;
            // Dispara o evento para notificar que o grupo de ativos foi alterado.
            PercentageChanged?.Invoke(Name, currentPercentage);
        }
    }

    #endregion Percentage

    #region Color

    /// <summary>
    /// M�todo chamado automaticamente quando a propriedade Color for alterada.
    /// Atualiza a cor do grupo de ativos no card.
    /// </summary>
    /// <param name="bindable">Inst�ncia da c�lula que sofreu a altera��o.</param>
    /// <param name="oldValue">Valor antigo da propriedade.</param>
    /// <param name="newValue">Novo valor da propriedade.</param>
    private static void OnColorChanged(BindableObject bindable, object oldValue, object newValue) {
        // Garante que o bindable � uma inst�ncia v�lida de AssetGroupCell e que o novo valor � um inteiro.
        if(bindable is not AssetGroupCell control || newValue is not int value) { return; }

        // Obt�m a quantidade de cores existentes.
        var count = control.colorSelector.ItemsSource.Count();
        // Normaliza��o do valor da cor.
        if(value < 0) {
            // Se o novo valor for menor que 0, ent�o atribui o primeiro item.
            value = 0;
        } else if(value > count) {
            // Se o novo valor for maior do que a quantidade m�xima de cores, ent�o atribui a �ltima cor dispon�vel.
            value = count;
        }

        // Exibe ou oculta a mensagem de aviso dependendo se a quantidade � zero ou negativa.
        control.colorSelector.SelectedIndex = value;
        // Define a cor na barra final com a cor selecionada.
        control.color.Color = control.colorSelector.SelectedColor;
    }

    /// <summary>
    /// Manipula o evento de mudan�a de cor selecionada no seletor de cores.
    /// Quando o usu�rio seleciona uma nova cor, armazena a cor atual, atualiza a sele��o
    /// e dispara o evento de notifica��o de mudan�a.
    /// </summary>
    /// <param name="sender">O objeto que disparou o evento, normalmente o seletor de cores.</param>
    /// <param name="e">Argumentos do evento contendo informa��es sobre a nova cor selecionada.</param>
    private void ColorSelector_SelectedColorChanged(object sender, ValueChangedEventArgs<Color> e) {
        // Armazena a cor atualmente configurada antes de realizar a altera��o.
        var previousColor = Color is null ? default : Color.Value;

        // Verifica se o seletor de cor est� corretamente instanciado.
        if(colorSelector is null) { return; }
        // Verifica se a cor � uma nova cor.
        if(Color != colorSelector.SelectedIndex) {
            // Atualiza o valor da propriedade Color com o �ndice selecionado no seletor.
            Color = colorSelector.SelectedIndex;
            // Dispara o evento ColorChanged, notificando que a cor foi alterada, passando o nome e a cor anterior.
            ColorChanged?.Invoke(Name, previousColor);
        }
    }

    #endregion Color

    #region Rename

    /// <summary>
    /// Manipula o evento de clique no bot�o de renomear do toolbar.
    /// </summary>
    /// <param name="sender">O objeto que disparou o evento, geralmente o bot�o.</param>
    /// <param name="e">Os dados do evento de clique.</param>
    private void RenameToolbarButton_Clicked(object sender, EventArgs e) {
        // Verifica se a c�lula n�o est� em execu��o para evitar m�ltiplas chamadas simult�neas.
        if(!isRunning) {
            // Define a flag para indicar que a c�lula est� em execu��o, evitando chamadas concorrentes.
            isRunning = true;

            // Dispara o evento RenameClicked, passando o nome atual como argumento para notificar os assinantes.
            RenameClicked?.Invoke(Name);

            // Reseta a flag de execu��o ap�s o t�rmino do processo.
            isRunning = false;
        }
    }

    #endregion Rename

    #region Delete

    /// <summary>
    /// Manipula o evento de clique no bot�o de renomear do toolbar.
    /// </summary>
    /// <param name="sender">O objeto que disparou o evento, geralmente o bot�o.</param>
    /// <param name="e">Os dados do evento de clique.</param>
    private async void DeleteToolbarButton_Clicked(object sender, EventArgs e) {
        // Verifica se a c�lula n�o est� em execu��o para evitar m�ltiplas chamadas simult�neas.
        if(!isRunning) {
            // Define a flag para indicar que a c�lula est� em execu��o, evitando chamadas concorrentes.
            isRunning = true;

            // Exibe uma caixa de di�logo para confirmar a exclus�o do grupo de ativos.
            if(await Shell.Current.DisplayAlert("Deletar o grupo de ativos!", $"Tem certeza de que deseja apagar o grupo de ativos \"{Name}\"?", "Sim", "N�o")) {
                // Dispara o evento DeleteClicked, passando o nome atual como argumento para notificar os assinantes.
                DeleteClicked?.Invoke(Name);
            }

            // Reseta a flag de execu��o ap�s o t�rmino do processo.
            isRunning = false;
        }
    }

    #endregion Delete

    #region ToGroup

    /// <summary>
    /// Manipula o evento de clique no bot�o de abrir grupo do toolbar.
    /// </summary>
    /// <param name="sender">O objeto que disparou o evento, geralmente o bot�o.</param>
    /// <param name="e">Os dados do evento de clique.</param>
    private void OpenGroupToolbarButton_Clicked(object sender, EventArgs e) {
        // Verifica se a c�lula n�o est� em execu��o para evitar m�ltiplas chamadas simult�neas.
        if(!isRunning) {
            // Define a flag para indicar que a c�lula est� em execu��o, evitando chamadas concorrentes.
            isRunning = true;

            // Dispara o evento OpenGroupClicked, passando o nome atual como argumento para notificar os assinantes.
            OpenGroupClicked?.Invoke(Name);

            // Reseta a flag de execu��o ap�s o t�rmino do processo.
            isRunning = false;
        }
    }

    #endregion ToGroup

    #endregion Methods
}