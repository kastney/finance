using Finance.Services;

namespace Finance.Pages.Initialize;

/// <summary>
/// ViewModel responsável pela lógica da página de apresentação, controlando a navegação e o estado do botão de criação.
/// </summary>
internal partial class PresentationViewModel : ObservableObject {

    #region Fields

    /// <summary>
    /// Serviço de navegação utilizado para transitar entre as páginas da aplicação.
    /// </summary>
    private readonly INavigationService navigationService;

    #endregion Fields

    #region Properties

    /// <summary>
    /// Indica se há uma operação em andamento, desabilitando a interface enquanto estiver verdadeiro.
    /// </summary>
    [ObservableProperty]
    private bool isRunning;

    #endregion Properties

    #region Constructor

    /// <summary>
    /// Inicializa uma nova instância do <see cref="PresentationViewModel"/> e carrega os serviços necessários.
    /// </summary>
    public PresentationViewModel() {
        // Obtém a instância do serviço de navegação.
        navigationService = Service.Get<INavigationService>();
    }

    #endregion Constructor

    #region Methods

    /// <summary>
    /// Comando acionado ao clicar em "Nova Carteira". Navega para a página de criação de carteira se nenhuma operação estiver em execução.
    /// </summary>
    /// <returns>Uma <see cref="Task"/> representando a operação assíncrona.</returns>
    [RelayCommand]
    private async Task NewWallet() {
        // Evita concorrência se já estiver executando outra operação.
        if(!IsRunning) {
            // Define o estado de execução como verdadeiro para bloquear a interface.
            IsRunning = true;

            // Navega para a página de criação de nova carteira.
            await navigationService.NavigateTo("create");
            // Aguarda um pequeno delay para estabilizar a navegação.
            await Task.Delay(100);

            // Retorna o estado de execução para falso, permitindo novas operações.
            IsRunning = false;
        }
    }

    #endregion Methods
}