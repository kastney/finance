using Finance.Utilities;

namespace Finance.Pages.Initialize;

/// <summary>
/// Página de carregamento utilizada para iniciar processos de inicialização ou transições.
/// </summary>
public partial class LoadingPage : ContentPage {

    #region Fields

    /// <summary>
    /// ViewModel responsável por controlar a lógica de carregamento e inicialização da aplicação.
    /// </summary>
    private readonly LoadingViewModel viewModel;

    #endregion Fields

    #region Constructor

    /// <summary>
    /// Inicializa os componentes visuais da página e define o contexto de binding com o ViewModel.
    /// </summary>
    public LoadingPage() {
        // Inicializa os componentes da interface definidos no XAML.
        InitializeComponent();
        // Obtém a instância do ViewModel pelo serviço de injeção de dependência e define como contexto de binding.
        BindingContext = viewModel = Service.Get<LoadingViewModel>();
    }

    #endregion Constructor

    #region Started Methods

    /// <summary>
    /// Método chamado externamente para iniciar o processo de inicialização definido no ViewModel.
    /// </summary>
    internal void Initialization() {
        // Chama o método de inicialização do ViewModel.
        viewModel.Initialization();
    }

    #endregion Started Methods

    #region Navigation Methods

    /// <summary>
    /// Sobrescreve o comportamento do botão de voltar para impedir que o usuário navegue para trás durante o carregamento.
    /// </summary>
    /// <returns>Retorna true para indicar que o evento foi tratado e impedir a navegação padrão.</returns>
    protected override bool OnBackButtonPressed() {
        // Bloqueia o botão de voltar para evitar interrupções durante a inicialização.
        return true;
    }

    #endregion Navigation Methods
}