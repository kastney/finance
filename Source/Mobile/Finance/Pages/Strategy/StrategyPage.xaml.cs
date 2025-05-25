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
}