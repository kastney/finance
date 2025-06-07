using Finance.Models;
using Finance.Utilities;

namespace Finance.Pages.Strategy;

/// <summary>
/// Representa a p�gina do grupo de ativos com a lista de ativos.
/// </summary>
[QueryProperty(nameof(GroupName), "name")]
public partial class AssetGroupPage : ContentPage {

    #region Fields

    /// <summary>
    /// Inst�ncia da ViewModel associada, que cont�m a l�gica e dados da p�gina.
    /// </summary>
    private readonly AssetGroupViewModel viewModel;

    #endregion Fields

    #region Properties

    /// <summary>
    /// Nome do grupo de ativos. <c>null</c> indica que a p�gina ser� de cria��o; caso contr�rio a p�gina ser� de edi��o.
    /// </summary>
    public string GroupName { get; set; }

    #endregion Properties

    #region Constructor

    /// <summary>
    /// Inicializa uma nova inst�ncia da classe <see cref="AssetGroupPage"/>.
    /// </summary>
    public AssetGroupPage() {
        // Inicializa os componentes visuais da p�gina xaml.
        InitializeComponent();
        // Obt�m a inst�ncia da ViewModel do servi�o de inje��o e define como contexto de dados.
        BindingContext = viewModel = Service.Get<AssetGroupViewModel>();
    }

    #endregion Constructor

    #region Start Methods

    /// <summary>
    /// M�todo chamado quando a p�gina est� prestes a ser exibida.
    /// Executa opera��es de inicializa��o como foco em campo e controle de estado.
    /// </summary>
    protected override void OnAppearing() {
        // Define o viewModel com o nome do grupo de ativos.
        viewModel.SetGroupName(GroupName);
    }

    #endregion Start Methods

    #region Navigation Methods

    /// <summary>
    /// Sobrescreve o comportamento do bot�o de voltar do dispositivo.
    /// Redireciona para o m�todo customizado de navega��o de retorno.
    /// </summary>
    /// <returns>Retorna true para indicar que o evento foi tratado.</returns>
    protected override bool OnBackButtonPressed() {
        // Invoca o m�todo de clique no bot�o voltar, simulando a a��o do usu�rio.
        BackButtom_Clicked(null, null);
        // Indica que o evento foi tratado para evitar comportamento padr�o.
        return true;
    }

    /// <summary>
    /// Evento disparado ao clicar no bot�o voltar da barra de navega��o.
    /// Executa a navega��o para a p�gina anterior se permitido pela ViewModel.
    /// </summary>
    /// <param name="sender">Objeto que disparou o evento (bot�o).</param>
    /// <param name="e">Argumentos do evento.</param>
    private async void BackButtom_Clicked(object sender, EventArgs e) {
        // Verifica se a ViewModel permite o retorno (valida��o ou confirma��o).
        if(viewModel.CanBack()) {
            // Executa a navega��o ass�ncrona para a p�gina anterior.
            await viewModel.NavigationBack();
        }
    }

    #endregion Navigation Methods

    #region Asset Methods

    /// <summary>
    /// Evento disparado quando o CollectionView completa uma opera��o de arrastar e soltar itens.
    /// </summary>
    /// <param name="sender">Objeto que disparou o evento (bot�o).</param>
    /// <param name="e">Argumentos do evento.</param>
    private async void DXCollectionView_CompleteItemDragDrop(object sender, CompleteItemDragDropEventArgs e) {
        // Salva a ordena��o atual da lista de ativos.
        await viewModel.SaveSorting();
    }

    /// <summary>
    /// Evento disparado quando o usu�rio seleciona um ativo na lista de sugest�es abaixo do campo de pesquisa.
    /// </summary>
    /// <param name="sender">Objeto que disparou o evento (bot�o).</param>
    /// <param name="e">Argumentos do evento.</param>

    private async void AutoCompleteEdit_SelectionChanged(object sender, EventArgs e) {
        // Obt�m o item selecionado
        var selectedItem = assetEntry.SelectedItem as AvailableAsset;

        // Limpa o campo de busca para permitir nova entrada.
        assetEntry.Text = string.Empty;

        // Fecha o teclado.
        await EntryUnfocus();

        // Adiciona o novo tipo de ativo no grupo de ativos.
        await viewModel.AddAsset(selectedItem);
    }

    /// <summary>
    /// Evento disparado quando a entrada de texto do campo de ativos � conclu�da.
    /// </summary>
    /// <param name="sender">>Objeto que disparou o evento (campo de entrada de texto).</param>
    /// <param name="e">Argumentos do evento.</param>
    private void AssetEntry_Completed(object sender, EventArgs e) {
        // Limpa o campo de entrada de texto ap�s a conclus�o da entrada.
        assetEntry.Text = string.Empty;
        viewModel.IsExpanded = false;
    }

    /// <summary>
    /// Abre o teclado caso a barra de pesquisa esteja vis�vel.
    /// </summary>
    /// <param name="sender">>Objeto que disparou o evento (campo de entrada de texto).</param>
    /// <param name="e">Argumentos do evento.</param>
    private async void ToolbarItem_Clicked(object sender, EventArgs e) {
        // Espera um tempo para verificar se a barra de pesquisa est� aberta.
        await Task.Delay(100);
        // Verifica se a barra de pesquisa est� aberto.
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
        // Desabilita o campo de busca para evitar m�ltiplas sele��es enquanto processa a sele��o atual.
        assetEntry.IsEnabled = false;
        // Adiciona um tempo de espera para evitar problemas de UI.
        await Task.Delay(100);
        // Reativa o campo de busca ap�s o processamento.
        assetEntry.IsEnabled = true;
        // Adiciona um tempo de espera para evitar problemas de UI.
        await Task.Delay(100);
    }

    #endregion
}