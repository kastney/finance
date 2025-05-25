using Finance.Utilities;

namespace Finance.Pages.Strategy;

/// <summary>
/// P�gina para cria��o de um novo grupo de ativos, respons�vel pela interface visual
/// e intera��o do usu�rio com a ViewModel correspondente.
/// </summary>
public partial class CreateAssetGroupPage : ContentPage {

    #region Fields

    /// <summary>
    /// Inst�ncia da ViewModel associada, que cont�m a l�gica e dados da p�gina.
    /// </summary>
    private readonly CreateAssetGroupViewModel viewModel;

    #endregion Fields

    #region Constructor

    /// <summary>
    /// Inicializa uma nova inst�ncia da p�gina, configurando componentes visuais
    /// e definindo o BindingContext para a ViewModel obtida via inje��o de depend�ncia.
    /// </summary>
    public CreateAssetGroupPage() {
        // Inicializa os componentes visuais definidos no XAML.
        InitializeComponent();
        // Obt�m a inst�ncia da ViewModel do servi�o de inje��o e define como contexto de dados.
        BindingContext = viewModel = Service.Get<CreateAssetGroupViewModel>();
    }

    #endregion Constructor

    #region Start Methods

    /// <summary>
    /// M�todo chamado quando a p�gina est� prestes a ser exibida.
    /// Executa opera��es de inicializa��o como foco em campo e controle de estado.
    /// </summary>
    protected override async void OnAppearing() {
        // Verifica se n�o est� em execu��o outro processo para evitar duplicidade.
        if(!viewModel.IsRunning) {
            // Define a flag indicando que a opera��o est� em execu��o.
            viewModel.IsRunning = true;

            // Pequena pausa para garantir que a interface esteja pronta para receber foco.
            await Task.Delay(100);
            // Define o foco no campo de entrada para o usu�rio digitar imediatamente.
            entry.Focus();

            // Reset da flag indicando fim da opera��o.
            viewModel.IsRunning = false;
        }
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
}