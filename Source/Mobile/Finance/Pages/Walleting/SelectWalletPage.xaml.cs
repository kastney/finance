namespace Finance.Pages.Walleting;

/// <summary>
/// Página responsável por exibir a interface de seleção de carteiras disponíveis,
/// gerenciando seu ciclo de vida e navegação.
/// </summary>
public partial class SelectWalletPage : ContentPage {

    #region Fields

    /// <summary>
    /// Instância do ViewModel associado à página, responsável pela lógica de seleção de carteiras.
    /// </summary>
    private readonly SelectWalletViewModel viewModel;

    #endregion Fields

    #region Constructor

    /// <summary>
    /// Inicializa os componentes visuais e define o contexto de dados da página.
    /// </summary>
    public SelectWalletPage() {
        // Inicializa os componentes definidos no XAML.
        InitializeComponent();
        // Obtém a instância do ViewModel via injeção de dependência e define como contexto de binding.
        BindingContext = viewModel = Service.Get<SelectWalletViewModel>();
    }

    #endregion Constructor

    #region Started Methods

    /// <summary>
    /// Executado automaticamente quando a página aparece na tela.
    /// Utilizado para controle de estado ou carregamento leve.
    /// </summary>
    protected override async void OnAppearing() {
        // Garante que o código de carregamento não será executado se já estiver em execução.
        if(!viewModel.IsRunning) {
            // Ativa o estado de carregamento no ViewModel.
            viewModel.IsRunning = true;

            // Aguarda brevemente antes de desativar (pode ser útil para transições ou atualizações visuais).
            await Task.Delay(100);

            // Finaliza o estado de carregamento.
            viewModel.IsRunning = false;
        }
    }

    #endregion Started Methods

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
}