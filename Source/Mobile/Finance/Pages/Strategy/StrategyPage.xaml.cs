using Finance.Utilities;

namespace Finance.Pages.Strategy;

/// <summary>
/// Página responsável por exibir e gerenciar a interface de seleção e manipulação de estratégias.
/// </summary>
public partial class StrategyPage : ContentPage {

    #region Fields

    /// <summary>
    /// Instância do ViewModel associado à página, responsável pela lógica de seleção de carteiras.
    /// </summary>
    private readonly StrategyViewModel viewModel;

    #endregion Fields

    #region Constructor

    /// <summary>
    /// Inicializa a página, configurando o componente visual e associando o BindingContext ao ViewModel.
    /// </summary>
    public StrategyPage() {
        // Inicializa os componentes visuais declarados no arquivo XAML.
        InitializeComponent();
        // Obtém a instância do ViewModel via injeção de dependência e define o BindingContext.
        BindingContext = viewModel = Service.Get<StrategyViewModel>();
    }

    #endregion Constructor

    #region Navigation Methods

    /// <summary>
    /// Intercepta o botão de voltar do dispositivo e executa navegação personalizada.
    /// </summary>
    /// <returns>Retorna true para impedir o comportamento de navegação padrão.</returns>
    protected override bool OnBackButtonPressed() {
        // Executa manualmente o método de clique do botão de voltar da interface.
        BackButtom_Clicked(null, null);
        // Indica que a navegação padrão do sistema não deve ser executada.
        return true;
    }

    /// <summary>
    /// Executado quando o botão de voltar na interface de navegação é clicado.
    /// </summary>
    /// <param name="sender">Objeto que disparou o evento (pode ser nulo).</param>
    /// <param name="e">Argumentos do evento (pode ser nulo).</param>
    private async void BackButtom_Clicked(object sender, EventArgs e) {
        // Verifica se é possível navegar para a tela anterior de forma segura.
        if(viewModel.CanBack()) {
            // Solicita ao ViewModel que execute a navegação de retorno.
            await viewModel.NavigationBack();
        }
    }

    #endregion Navigation Methods

    #region Walleting Methods

    /// <summary>
    /// Evento disparado quando o CollectionView completa uma operação de arrastar e soltar itens.
    /// </summary>
    /// <param name="sender">Objeto que disparou o evento.</param>
    /// <param name="e">Argumentos do evento.</param>
    private async void DXCollectionView_CompleteItemDragDrop(object sender, CompleteItemDragDropEventArgs e) {
        // Chama o método no ViewModel para salvar a nova ordem dos itens após o arrastar e soltar.
        await viewModel.SaveSorting();
    }

    /// <summary>
    /// Evento disparado quando ocorrer uma muança do status de ativado de um grupo de ativos.
    /// </summary>
    /// <param name="name">Nome do grupo de ativos que sofreu alteração.</param>
    /// <param name="oldPercentage">O valor da porcentagem antes da mudança.</param>
    private async void AssetGroupCell_CheckedChanged(string name, int oldPercentage) {
        // Chama o método no ViewModel para atualizar o status de ativado do grupo de ativos.
        await viewModel.UpdateChecked(name, oldPercentage);
    }

    /// <summary>
    /// Evento disparado quando ocorrer uma mudança na porcentagem de um grupo de ativos.
    /// </summary>
    /// <param name="name">Nome do grupo de ativos que sofreu alteração.</param>
    /// <param name="oldPercentage">O valor da porcentagem antes da mudança.</param>
    private async void AssetGroupCell_PercentageChanged(string name, int oldPercentage) {
        // Chama o método no ViewModel para atualizar a porcentagem do grupo de ativos.
        await viewModel.UpdatePercentage(name, oldPercentage);
    }

    /// <summary>
    /// Evento disparado quando ocorrer uma mudança na cor de um grupo de ativos.
    /// </summary>
    /// <param name="name">Nome do grupo de ativos que sofreu alteração.</param>
    /// <param name="oldColor">O valor da cor antes da mudança.</param>
    private async void AssetGroupCell_ColorChanged(string name, int oldColor) {
        // Chama o método no ViewModel para atualizar a porcentagem do grupo de ativos.
        await viewModel.UpdateColor(name, oldColor);
    }

    /// <summary>
    /// Manipula o evento de solicitação de renomeação de um grupo de ativos.
    /// </summary>
    /// <param name="name">O nome do grupo de ativos que deve ser renomeado.</param>
    private async void AssetGroupCell_RenameClicked(string name) {
        // Chama o método de renomeação no viewModel, passando o nome do grupo.
        await viewModel.Rename(name);
    }

    #endregion Walleting Methods
}