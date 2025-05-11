namespace Finance.Pages.Walleting;

/// <summary>
/// P�gina respons�vel por exibir a interface de sele��o de carteiras dispon�veis,
/// gerenciando seu ciclo de vida e navega��o.
/// </summary>
public partial class SelectWalletPage : ContentPage {

    #region Fields

    /// <summary>
    /// Inst�ncia do ViewModel associado � p�gina, respons�vel pela l�gica de sele��o de carteiras.
    /// </summary>
    private readonly SelectWalletViewModel viewModel;

    #endregion Fields

    #region Constructor

    /// <summary>
    /// Inicializa os componentes visuais e define o contexto de dados da p�gina.
    /// </summary>
    public SelectWalletPage() {
        // Inicializa os componentes definidos no XAML.
        InitializeComponent();
        // Obt�m a inst�ncia do ViewModel via inje��o de depend�ncia e define como contexto de binding.
        BindingContext = viewModel = Service.Get<SelectWalletViewModel>();
    }

    #endregion Constructor

    #region Started Methods

    /// <summary>
    /// Executado automaticamente quando a p�gina aparece na tela.
    /// Utilizado para controle de estado ou carregamento leve.
    /// </summary>
    protected override async void OnAppearing() {
        // Garante que o c�digo de carregamento n�o ser� executado se j� estiver em execu��o.
        if(!viewModel.IsRunning) {
            // Ativa o estado de carregamento no ViewModel.
            viewModel.IsRunning = true;

            // Aguarda brevemente antes de desativar (pode ser �til para transi��es ou atualiza��es visuais).
            await Task.Delay(100);

            // Finaliza o estado de carregamento.
            viewModel.IsRunning = false;
        }
    }

    #endregion Started Methods

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
}