using Finance.Services.Navigation;
using Finance.Services.Walleting;
using Finance.Utilities;

namespace Finance.Pages.Initialize;

/// <summary>
/// ViewModel responsável por controlar o processo de inicialização do aplicativo,
/// decidindo a navegação com base na existência de uma carteira configurada.
/// </summary>
internal class LoadingViewModel {

    #region Fields

    /// <summary>
    /// Serviço responsável por fornecer operações relacionadas às carteiras.
    /// </summary>
    private readonly IWalletService walletService;

    /// <summary>
    /// Serviço de navegação utilizado para transitar entre páginas da aplicação.
    /// </summary>
    private readonly INavigationService navigationService;

    #endregion Fields

    #region Constructor

    /// <summary>
    /// Construtor que inicializa os serviços necessários e inicia o processo de carregamento.
    /// </summary>
    public LoadingViewModel() {
        // Obtém o serviço de carteiras por injeção de dependência.
        walletService = Service.Get<IWalletService>();
        // Obtém o serviço de navegação por injeção de dependência.
        navigationService = Service.Get<INavigationService>();
        // Inicia o processo de inicialização.
        Initialization();
    }

    #endregion Constructor

    #region Start Methods

    /// <summary>
    /// Método assíncrono que decide a página inicial com base na existência de uma carteira.
    /// </summary>
    internal async void Initialization() {
        // Verifica se já existe uma carteira cadastrada.
        if(await walletService.Exists()) {
            // Se existir, navega para a página principal e inicia sua lógica interna.
            if(await navigationService.NavigateTo("///main") is MainPage page) { page.Initialization(); }
        } else {
            // Se não existir, navega para a página de apresentação.
            await navigationService.NavigateTo("///presentation");
        }
    }

    #endregion Start Methods
}