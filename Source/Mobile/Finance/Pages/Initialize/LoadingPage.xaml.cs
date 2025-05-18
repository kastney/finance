using Finance.Utilities;

namespace Finance.Pages.Initialize;

/// <summary>
/// P�gina de carregamento utilizada para iniciar processos de inicializa��o ou transi��es.
/// </summary>
public partial class LoadingPage : ContentPage {

    #region Fields

    /// <summary>
    /// ViewModel respons�vel por controlar a l�gica de carregamento e inicializa��o da aplica��o.
    /// </summary>
    private readonly LoadingViewModel viewModel;

    #endregion Fields

    #region Constructor

    /// <summary>
    /// Inicializa os componentes visuais da p�gina e define o contexto de binding com o ViewModel.
    /// </summary>
    public LoadingPage() {
        // Inicializa os componentes da interface definidos no XAML.
        InitializeComponent();
        // Obt�m a inst�ncia do ViewModel pelo servi�o de inje��o de depend�ncia e define como contexto de binding.
        BindingContext = viewModel = Service.Get<LoadingViewModel>();
    }

    #endregion Constructor

    #region Started Methods

    /// <summary>
    /// M�todo chamado externamente para iniciar o processo de inicializa��o definido no ViewModel.
    /// </summary>
    internal void Initialization() {
        // Chama o m�todo de inicializa��o do ViewModel.
        viewModel.Initialization();
    }

    #endregion Started Methods

    #region Navigation Methods

    /// <summary>
    /// Sobrescreve o comportamento do bot�o de voltar para impedir que o usu�rio navegue para tr�s durante o carregamento.
    /// </summary>
    /// <returns>Retorna true para indicar que o evento foi tratado e impedir a navega��o padr�o.</returns>
    protected override bool OnBackButtonPressed() {
        // Bloqueia o bot�o de voltar para evitar interrup��es durante a inicializa��o.
        return true;
    }

    #endregion Navigation Methods
}