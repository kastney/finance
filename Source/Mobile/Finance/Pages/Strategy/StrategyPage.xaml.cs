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
}