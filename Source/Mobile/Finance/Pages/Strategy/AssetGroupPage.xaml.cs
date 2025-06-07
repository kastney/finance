using Finance.Models;
using Finance.Utilities;

namespace Finance.Pages.Strategy;

/// <summary>
/// Representa a página do grupo de ativos com a lista de ativos.
/// </summary>
[QueryProperty(nameof(GroupName), "name")]
public partial class AssetGroupPage : ContentPage {

    #region Fields

    /// <summary>
    /// Instância da ViewModel associada, que contém a lógica e dados da página.
    /// </summary>
    private readonly AssetGroupViewModel viewModel;

    #endregion Fields

    #region Properties

    /// <summary>
    /// Nome do grupo de ativos. <c>null</c> indica que a página será de criação; caso contrário a página será de edição.
    /// </summary>
    public string GroupName { get; set; }

    #endregion Properties

    #region Constructor

    /// <summary>
    /// Inicializa uma nova instância da classe <see cref="AssetGroupPage"/>.
    /// </summary>
    public AssetGroupPage() {
        // Inicializa os componentes visuais da página xaml.
        InitializeComponent();
        // Obtém a instância da ViewModel do serviço de injeção e define como contexto de dados.
        BindingContext = viewModel = Service.Get<AssetGroupViewModel>();
    }

    #endregion Constructor

    #region Start Methods

    /// <summary>
    /// Método chamado quando a página está prestes a ser exibida.
    /// Executa operações de inicialização como foco em campo e controle de estado.
    /// </summary>
    protected override void OnAppearing() {
        // Define o viewModel com o nome do grupo de ativos.
        viewModel.SetGroupName(GroupName);
    }

    #endregion Start Methods

    #region Navigation Methods

    /// <summary>
    /// Sobrescreve o comportamento do botão de voltar do dispositivo.
    /// Redireciona para o método customizado de navegação de retorno.
    /// </summary>
    /// <returns>Retorna true para indicar que o evento foi tratado.</returns>
    protected override bool OnBackButtonPressed() {
        // Invoca o método de clique no botão voltar, simulando a ação do usuário.
        BackButtom_Clicked(null, null);
        // Indica que o evento foi tratado para evitar comportamento padrão.
        return true;
    }

    /// <summary>
    /// Evento disparado ao clicar no botão voltar da barra de navegação.
    /// Executa a navegação para a página anterior se permitido pela ViewModel.
    /// </summary>
    /// <param name="sender">Objeto que disparou o evento (botão).</param>
    /// <param name="e">Argumentos do evento.</param>
    private async void BackButtom_Clicked(object sender, EventArgs e) {
        // Verifica se a ViewModel permite o retorno (validação ou confirmação).
        if(viewModel.CanBack()) {
            // Executa a navegação assíncrona para a página anterior.
            await viewModel.NavigationBack();
        }
    }

    #endregion Navigation Methods

    #region Asset Methods

    /// <summary>
    /// Evento disparado quando o CollectionView completa uma operação de arrastar e soltar itens.
    /// </summary>
    /// <param name="sender">Objeto que disparou o evento (botão).</param>
    /// <param name="e">Argumentos do evento.</param>
    private async void DXCollectionView_CompleteItemDragDrop(object sender, CompleteItemDragDropEventArgs e) {
        // Salva a ordenação atual da lista de ativos.
        await viewModel.SaveSorting();
    }

    /// <summary>
    /// Evento disparado quando o usuário seleciona um ativo na lista de sugestões abaixo do campo de pesquisa.
    /// </summary>
    /// <param name="sender">Objeto que disparou o evento (botão).</param>
    /// <param name="e">Argumentos do evento.</param>

    private async void AutoCompleteEdit_SelectionChanged(object sender, EventArgs e) {
        // Obtém o item selecionado
        var selectedItem = assetEntry.SelectedItem as AvailableAsset;

        // Limpa o campo de busca para permitir nova entrada.
        assetEntry.Text = string.Empty;

        // Fecha o teclado.
        await EntryUnfocus();

        // Adiciona o novo tipo de ativo no grupo de ativos.
        await viewModel.AddAsset(selectedItem);
    }

    /// <summary>
    /// Evento disparado quando a entrada de texto do campo de ativos é concluída.
    /// </summary>
    /// <param name="sender">>Objeto que disparou o evento (campo de entrada de texto).</param>
    /// <param name="e">Argumentos do evento.</param>
    private void AssetEntry_Completed(object sender, EventArgs e) {
        // Limpa o campo de entrada de texto após a conclusão da entrada.
        assetEntry.Text = string.Empty;
        viewModel.IsExpanded = false;
    }

    /// <summary>
    /// Abre o teclado caso a barra de pesquisa esteja visível.
    /// </summary>
    /// <param name="sender">>Objeto que disparou o evento (campo de entrada de texto).</param>
    /// <param name="e">Argumentos do evento.</param>
    private async void ToolbarItem_Clicked(object sender, EventArgs e) {
        // Espera um tempo para verificar se a barra de pesquisa está aberta.
        await Task.Delay(100);
        // Verifica se a barra de pesquisa está aberto.
        if(viewModel.IsExpanded == true) {
            // Abre o teclado.
            assetEntry.Focus();
        } else {
            // Fecha o teclado.
            await EntryUnfocus();
        }
    }

    #endregion Asset Methods

    #region Private Methods

    /// <summary>
    /// Fecha o teclado da barra de pesquisa.
    /// </summary>
    /// <returns>Uma tarefa indicando o fechamento do teclado da barra de pesquisa.</returns>
    private async Task EntryUnfocus() {
        // Desabilita o campo de busca para evitar múltiplas seleções enquanto processa a seleção atual.
        assetEntry.IsEnabled = false;
        // Adiciona um tempo de espera para evitar problemas de UI.
        await Task.Delay(100);
        // Reativa o campo de busca após o processamento.
        assetEntry.IsEnabled = true;
        // Adiciona um tempo de espera para evitar problemas de UI.
        await Task.Delay(100);
    }

    #endregion
}