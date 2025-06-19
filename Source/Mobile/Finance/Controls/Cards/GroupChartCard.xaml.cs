using Finance.Models;
using Finance.Utilities;

namespace Finance.Controls.Cards;

/// <summary>
/// Representa um cartão visual com gráfico de pizza, exibindo a estratégia de alocação de um grupo de ativos.
/// </summary>
public partial class GroupChartCard : ContentView {

    #region Fields

    /// <summary>
    /// Propriedade vinculável que representa a porcentagem ainda não alocada da carteira.
    /// </summary>
    public static readonly BindableProperty PercentageAvailableProperty = BindableProperty.Create(nameof(PercentageAvailable), typeof(int), typeof(GroupChartCard), -1, propertyChanged: OnPercentageAvailableChanged);

    /// <summary>
    /// Propriedade vinculável que representa os dados para exibição no gráfico de pizza.
    /// </summary>
    public static readonly BindableProperty DataProperty = BindableProperty.Create(nameof(Data), typeof(List<PieData>), typeof(GroupChartCard), null, propertyChanged: OnDataChanged);

    /// <summary>
    /// Propriedade vinculável para indicar se mostra o aviso ao usuário.
    /// </summary>
    public static readonly BindableProperty HasWarningProperty = BindableProperty.Create(nameof(HasWarning), typeof(bool), typeof(GroupChartCard), false, propertyChanged: OnHasWarningChanged);

    /// <summary>
    /// Propriedade vinculável para armazenar a cor do grupo de ativos. Sempre que alterado, dispara o método OnGruopColorChanged.
    /// </summary>
    public static readonly BindableProperty GroupColorProperty = BindableProperty.Create(nameof(GroupColor), typeof(int?), typeof(GroupChartCard), null);

    #endregion Fields

    #region Properties

    /// <summary>
    /// Obtém ou define a porcentagem não alocada da carteira. Atualiza o rótulo e a visibilidade do aviso.
    /// </summary>
    public int PercentageAvailable {
        get => (int)GetValue(PercentageAvailableProperty);
        set => SetValue(PercentageAvailableProperty, value);
    }

    /// <summary>
    /// Obtém ou define a lista de dados que alimenta o gráfico de pizza.
    /// </summary>
    public List<PieData> Data {
        get => (List<PieData>)GetValue(DataProperty);
        set => SetValue(DataProperty, value);
    }

    /// <summary>
    /// Obtém ou define o valor se mostra ou não a mensagem de avisos.
    /// </summary>
    public bool HasWarning {
        get => (bool)GetValue(HasWarningProperty);
        set => SetValue(HasWarningProperty, value);
    }

    /// <summary>
    /// Obtém ou define a cor do grupo de ativos.
    /// </summary>
    public int? GroupColor {
        get => (int?)GetValue(GroupColorProperty);
        set => SetValue(GroupColorProperty, value);
    }

    #endregion Properties

    #region Constructor

    /// <summary>
    /// Inicializa uma nova instância do componente <see cref="GroupChartCard"/>.
    /// </summary>
    public GroupChartCard() {
        // Inicializa os componentes do cartão e configura o tema.
        InitializeComponent();
        // Inscreve-se no evento de alteração do tema do aplicativo.
        Application.Current.RequestedThemeChanged += Current_RequestedThemeChanged;
        // Aplica a configuração de tema atual.
        Current_RequestedThemeChanged(null, null);
    }

    #endregion Constructor

    #region Methods

    /// <summary>
    /// Manipula as alterações na propriedade <see cref="PercentageAvailable"/>, atualizando o rótulo de porcentagem e a visibilidade do aviso.
    /// </summary>
    /// <param name="bindable">O objeto associado à propriedade.</param>
    /// <param name="oldValue">O valor anterior da propriedade.</param>
    /// <param name="newValue">O novo valor da propriedade.</param>
    private static void OnPercentageAvailableChanged(BindableObject bindable, object oldValue, object newValue) {
        // Verifica se o objeto associado é do tipo GroupChartCard e se o novo valor é um inteiro.
        if(bindable is not GroupChartCard control || newValue is not int value) { return; }
        // Atualiza o rótulo de porcentagem.
        control.percentageLabel.Text = $"{value}%";
    }

    /// <summary>
    /// Manipula as alterações na propriedade <see cref="Data"/>, atualizando a paleta de cores e o datasource do gráfico.
    /// </summary>
    /// <param name="bindable">O objeto associado à propriedade.</param>
    /// <param name="oldValue">O valor anterior da propriedade.</param>
    /// <param name="newValue">O novo valor da propriedade.</param>
    private static void OnDataChanged(BindableObject bindable, object oldValue, object newValue) {
        // Verifica se o objeto associado é do tipo GroupChartCard e se o novo valor é uma lista de PieData.
        if(bindable is not GroupChartCard control || newValue is not List<PieData> list) { return; }

        // Obém o número de segmentos de dados existentes no gráfico.
        int count = list.Count;

        // Se houver porcentagem disponível, adiciona o segmento "Não alocado" ao gráfico.
        if(control.PercentageAvailable > 0) {
            // Adiciona um novo segmento de dados representando a porcentagem não alocada.
            list.Add(new PieData("Não alocado", control.PercentageAvailable, -2));
            // Incrementa o contador de segmentos de dados.
            count++;
        }

        // Cria a paleta de cores com base na lista de dados.
        var pallet = new Color[count];

        // Percorre cada segmento de dados na lista para preencher a paleta de cores.
        for(int i = 0; i < pallet.Length; i++) {
            // Preenche a paleta de cores com as cores correspondentes aos segmentos de dados.
            // Se o índice for -1, usa uma cor padrão cinza.
            pallet[i] = ColorUtility.GetColor(control.GroupColor.Value, list[i].Color);
        }

        // Define a paleta de cores do gráfico com as cores obtidas.
        control.chartStyle.Palette = pallet;

        // Define o datasource do gráfico com a lista de dados atualizada.
        control.seriesData.DataSource = list;
    }

    /// <summary>
    /// Método chamado automaticamente quando a propriedade HasWarning for alterada.
    /// Mostra ou não a mensagem de aviso ao usuário.
    /// </summary>
    /// <param name="bindable">Instância da célula que sofreu a alteração.</param>
    /// <param name="oldValue">Valor antigo da propriedade.</param>
    /// <param name="newValue">Novo valor da propriedade.</param>
    private static void OnHasWarningChanged(BindableObject bindable, object oldValue, object newValue) {
        // Verifica se o bindable é do tipo GroupChartCard e define o valor da porcentagem disponível para os tipos de ativos.
        if(bindable is not GroupChartCard control || newValue is not bool value) { return; }
        // Exibe ou oculta o aviso com base na porcentagem disponível.
        control.warningText.IsVisible = value;
    }

    /// <summary>
    /// Manipula a mudança de tema do aplicativo, ajustando as cores de fundo e do texto do gráfico conforme o tema atual.
    /// </summary>
    /// <param name="sender">O objeto que disparou o evento.</param>
    /// <param name="e">Os dados do evento.</param>
    private void Current_RequestedThemeChanged(object sender, AppThemeChangedEventArgs e) {
        // Verifica o tema atual do aplicativo e ajusta as cores do gráfico e dos rótulos de acordo com o tema.
        if(Application.Current.RequestedTheme == AppTheme.Light) {
            // Define as cores do gráfico e dos rótulos para o tema claro.
            chartStyle.BackgroundColor = StaticResourceUtility.Get<Color>("Gray100");
            // Define a cor do texto dos rótulos para preto.
            labelStyle.Color = StaticResourceUtility.Get<Color>("Black");
        } else {
            // Define as cores do gráfico e dos rótulos para o tema escuro.
            chartStyle.BackgroundColor = StaticResourceUtility.Get<Color>("Gray600");
            // Define a cor do texto dos rótulos para branco.
            labelStyle.Color = StaticResourceUtility.Get<Color>("White");
        }
    }

    #endregion Methods
}