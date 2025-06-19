using Finance.Models;
using Finance.Utilities;

namespace Finance.Controls.Cards;

/// <summary>
/// Representa um cart�o visual com gr�fico de pizza, exibindo a estrat�gia de aloca��o de um grupo de ativos.
/// </summary>
public partial class GroupChartCard : ContentView {

    #region Fields

    /// <summary>
    /// Propriedade vincul�vel que representa a porcentagem ainda n�o alocada da carteira.
    /// </summary>
    public static readonly BindableProperty PercentageAvailableProperty = BindableProperty.Create(nameof(PercentageAvailable), typeof(int), typeof(GroupChartCard), -1, propertyChanged: OnPercentageAvailableChanged);

    /// <summary>
    /// Propriedade vincul�vel que representa os dados para exibi��o no gr�fico de pizza.
    /// </summary>
    public static readonly BindableProperty DataProperty = BindableProperty.Create(nameof(Data), typeof(List<PieData>), typeof(GroupChartCard), null, propertyChanged: OnDataChanged);

    /// <summary>
    /// Propriedade vincul�vel para indicar se mostra o aviso ao usu�rio.
    /// </summary>
    public static readonly BindableProperty HasWarningProperty = BindableProperty.Create(nameof(HasWarning), typeof(bool), typeof(GroupChartCard), false, propertyChanged: OnHasWarningChanged);

    /// <summary>
    /// Propriedade vincul�vel para armazenar a cor do grupo de ativos. Sempre que alterado, dispara o m�todo OnGruopColorChanged.
    /// </summary>
    public static readonly BindableProperty GroupColorProperty = BindableProperty.Create(nameof(GroupColor), typeof(int?), typeof(GroupChartCard), null);

    #endregion Fields

    #region Properties

    /// <summary>
    /// Obt�m ou define a porcentagem n�o alocada da carteira. Atualiza o r�tulo e a visibilidade do aviso.
    /// </summary>
    public int PercentageAvailable {
        get => (int)GetValue(PercentageAvailableProperty);
        set => SetValue(PercentageAvailableProperty, value);
    }

    /// <summary>
    /// Obt�m ou define a lista de dados que alimenta o gr�fico de pizza.
    /// </summary>
    public List<PieData> Data {
        get => (List<PieData>)GetValue(DataProperty);
        set => SetValue(DataProperty, value);
    }

    /// <summary>
    /// Obt�m ou define o valor se mostra ou n�o a mensagem de avisos.
    /// </summary>
    public bool HasWarning {
        get => (bool)GetValue(HasWarningProperty);
        set => SetValue(HasWarningProperty, value);
    }

    /// <summary>
    /// Obt�m ou define a cor do grupo de ativos.
    /// </summary>
    public int? GroupColor {
        get => (int?)GetValue(GroupColorProperty);
        set => SetValue(GroupColorProperty, value);
    }

    #endregion Properties

    #region Constructor

    /// <summary>
    /// Inicializa uma nova inst�ncia do componente <see cref="GroupChartCard"/>.
    /// </summary>
    public GroupChartCard() {
        // Inicializa os componentes do cart�o e configura o tema.
        InitializeComponent();
        // Inscreve-se no evento de altera��o do tema do aplicativo.
        Application.Current.RequestedThemeChanged += Current_RequestedThemeChanged;
        // Aplica a configura��o de tema atual.
        Current_RequestedThemeChanged(null, null);
    }

    #endregion Constructor

    #region Methods

    /// <summary>
    /// Manipula as altera��es na propriedade <see cref="PercentageAvailable"/>, atualizando o r�tulo de porcentagem e a visibilidade do aviso.
    /// </summary>
    /// <param name="bindable">O objeto associado � propriedade.</param>
    /// <param name="oldValue">O valor anterior da propriedade.</param>
    /// <param name="newValue">O novo valor da propriedade.</param>
    private static void OnPercentageAvailableChanged(BindableObject bindable, object oldValue, object newValue) {
        // Verifica se o objeto associado � do tipo GroupChartCard e se o novo valor � um inteiro.
        if(bindable is not GroupChartCard control || newValue is not int value) { return; }
        // Atualiza o r�tulo de porcentagem.
        control.percentageLabel.Text = $"{value}%";
    }

    /// <summary>
    /// Manipula as altera��es na propriedade <see cref="Data"/>, atualizando a paleta de cores e o datasource do gr�fico.
    /// </summary>
    /// <param name="bindable">O objeto associado � propriedade.</param>
    /// <param name="oldValue">O valor anterior da propriedade.</param>
    /// <param name="newValue">O novo valor da propriedade.</param>
    private static void OnDataChanged(BindableObject bindable, object oldValue, object newValue) {
        // Verifica se o objeto associado � do tipo GroupChartCard e se o novo valor � uma lista de PieData.
        if(bindable is not GroupChartCard control || newValue is not List<PieData> list) { return; }

        // Ob�m o n�mero de segmentos de dados existentes no gr�fico.
        int count = list.Count;

        // Se houver porcentagem dispon�vel, adiciona o segmento "N�o alocado" ao gr�fico.
        if(control.PercentageAvailable > 0) {
            // Adiciona um novo segmento de dados representando a porcentagem n�o alocada.
            list.Add(new PieData("N�o alocado", control.PercentageAvailable, -2));
            // Incrementa o contador de segmentos de dados.
            count++;
        }

        // Cria a paleta de cores com base na lista de dados.
        var pallet = new Color[count];

        // Percorre cada segmento de dados na lista para preencher a paleta de cores.
        for(int i = 0; i < pallet.Length; i++) {
            // Preenche a paleta de cores com as cores correspondentes aos segmentos de dados.
            // Se o �ndice for -1, usa uma cor padr�o cinza.
            pallet[i] = ColorUtility.GetColor(control.GroupColor.Value, list[i].Color);
        }

        // Define a paleta de cores do gr�fico com as cores obtidas.
        control.chartStyle.Palette = pallet;

        // Define o datasource do gr�fico com a lista de dados atualizada.
        control.seriesData.DataSource = list;
    }

    /// <summary>
    /// M�todo chamado automaticamente quando a propriedade HasWarning for alterada.
    /// Mostra ou n�o a mensagem de aviso ao usu�rio.
    /// </summary>
    /// <param name="bindable">Inst�ncia da c�lula que sofreu a altera��o.</param>
    /// <param name="oldValue">Valor antigo da propriedade.</param>
    /// <param name="newValue">Novo valor da propriedade.</param>
    private static void OnHasWarningChanged(BindableObject bindable, object oldValue, object newValue) {
        // Verifica se o bindable � do tipo GroupChartCard e define o valor da porcentagem dispon�vel para os tipos de ativos.
        if(bindable is not GroupChartCard control || newValue is not bool value) { return; }
        // Exibe ou oculta o aviso com base na porcentagem dispon�vel.
        control.warningText.IsVisible = value;
    }

    /// <summary>
    /// Manipula a mudan�a de tema do aplicativo, ajustando as cores de fundo e do texto do gr�fico conforme o tema atual.
    /// </summary>
    /// <param name="sender">O objeto que disparou o evento.</param>
    /// <param name="e">Os dados do evento.</param>
    private void Current_RequestedThemeChanged(object sender, AppThemeChangedEventArgs e) {
        // Verifica o tema atual do aplicativo e ajusta as cores do gr�fico e dos r�tulos de acordo com o tema.
        if(Application.Current.RequestedTheme == AppTheme.Light) {
            // Define as cores do gr�fico e dos r�tulos para o tema claro.
            chartStyle.BackgroundColor = StaticResourceUtility.Get<Color>("Gray100");
            // Define a cor do texto dos r�tulos para preto.
            labelStyle.Color = StaticResourceUtility.Get<Color>("Black");
        } else {
            // Define as cores do gr�fico e dos r�tulos para o tema escuro.
            chartStyle.BackgroundColor = StaticResourceUtility.Get<Color>("Gray600");
            // Define a cor do texto dos r�tulos para branco.
            labelStyle.Color = StaticResourceUtility.Get<Color>("White");
        }
    }

    #endregion Methods
}