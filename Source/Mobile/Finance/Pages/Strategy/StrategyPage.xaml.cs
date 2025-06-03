using Finance.Utilities;

namespace Finance.Pages.Strategy;

/// <summary>
/// P�gina respons�vel por exibir e gerenciar a interface de sele��o e manipula��o de estrat�gias.
/// </summary>
public partial class StrategyPage : ContentPage {

    #region Fields

    /// <summary>
    /// Inst�ncia do ViewModel associado � p�gina, respons�vel pela l�gica de sele��o de carteiras.
    /// </summary>
    private readonly StrategyViewModel viewModel;

    #endregion Fields

    #region Constructor

    /// <summary>
    /// Inicializa a p�gina, configurando o componente visual e associando o BindingContext ao ViewModel.
    /// </summary>
    public StrategyPage() {
        // Inicializa os componentes visuais declarados no arquivo XAML.
        InitializeComponent();
        // Obt�m a inst�ncia do ViewModel via inje��o de depend�ncia e define o BindingContext.
        BindingContext = viewModel = Service.Get<StrategyViewModel>();
    }

    #endregion Constructor

    #region Navigation Methods

    /// <summary>
    /// Intercepta o bot�o de voltar do dispositivo e executa navega��o personalizada.
    /// </summary>
    /// <returns>Retorna true para impedir o comportamento de navega��o padr�o.</returns>
    protected override bool OnBackButtonPressed() {
        // Executa manualmente o m�todo de clique do bot�o de voltar da interface.
        BackButtom_Clicked(null, null);
        // Indica que a navega��o padr�o do sistema n�o deve ser executada.
        return true;
    }

    /// <summary>
    /// Executado quando o bot�o de voltar na interface de navega��o � clicado.
    /// </summary>
    /// <param name="sender">Objeto que disparou o evento (pode ser nulo).</param>
    /// <param name="e">Argumentos do evento (pode ser nulo).</param>
    private async void BackButtom_Clicked(object sender, EventArgs e) {
        // Verifica se � poss�vel navegar para a tela anterior de forma segura.
        if(viewModel.CanBack()) {
            // Solicita ao ViewModel que execute a navega��o de retorno.
            await viewModel.NavigationBack();
        }
    }

    #endregion Navigation Methods

    #region Walleting Methods

    /// <summary>
    /// Evento disparado quando o CollectionView completa uma opera��o de arrastar e soltar itens.
    /// </summary>
    /// <param name="sender">Objeto que disparou o evento.</param>
    /// <param name="e">Argumentos do evento.</param>
    private async void DXCollectionView_CompleteItemDragDrop(object sender, CompleteItemDragDropEventArgs e) {
        // Chama o m�todo no ViewModel para salvar a nova ordem dos itens ap�s o arrastar e soltar.
        await viewModel.SaveSorting();
    }

    /// <summary>
    /// Evento disparado quando ocorrer uma muan�a do status de ativado de um grupo de ativos.
    /// </summary>
    /// <param name="name">Nome do grupo de ativos que sofreu altera��o.</param>
    /// <param name="oldPercentage">O valor da porcentagem antes da mudan�a.</param>
    private async void AssetGroupCell_CheckedChanged(string name, int oldPercentage) {
        // Chama o m�todo no ViewModel para atualizar o status de ativado do grupo de ativos.
        await viewModel.UpdateChecked(name, oldPercentage);
    }

    /// <summary>
    /// Evento disparado quando ocorrer uma mudan�a na porcentagem de um grupo de ativos.
    /// </summary>
    /// <param name="name">Nome do grupo de ativos que sofreu altera��o.</param>
    /// <param name="oldPercentage">O valor da porcentagem antes da mudan�a.</param>
    private async void AssetGroupCell_PercentageChanged(string name, int oldPercentage) {
        // Chama o m�todo no ViewModel para atualizar a porcentagem do grupo de ativos.
        await viewModel.UpdatePercentage(name, oldPercentage);
    }

    /// <summary>
    /// Evento disparado quando ocorrer uma mudan�a na cor de um grupo de ativos.
    /// </summary>
    /// <param name="name">Nome do grupo de ativos que sofreu altera��o.</param>
    /// <param name="oldColor">O valor da cor antes da mudan�a.</param>
    private async void AssetGroupCell_ColorChanged(string name, int oldColor) {
        // Chama o m�todo no ViewModel para atualizar a porcentagem do grupo de ativos.
        await viewModel.UpdateColor(name, oldColor);
    }

    /// <summary>
    /// Manipula o evento de solicita��o de renomea��o de um grupo de ativos.
    /// </summary>
    /// <param name="name">O nome do grupo de ativos que deve ser renomeado.</param>
    private async void AssetGroupCell_RenameClicked(string name) {
        // Chama o m�todo de renomea��o no viewModel, passando o nome do grupo.
        await viewModel.Rename(name);
    }

    #endregion Walleting Methods
}