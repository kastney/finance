using Finance.Services.Navigation;
using Finance.Services.Walleting;

namespace Finance.Utilities;

/// <summary>
/// Classe base para os ViewModels, fornecendo integração com serviços de navegação e gerenciamento de carteiras,
/// além de propriedades e métodos utilitários comuns para herança.
/// </summary>
internal partial class ViewModel : ObservableObject {

    #region Fields

    /// <summary>
    /// Serviço responsável pelo gerenciamento de carteiras.
    /// </summary>
    protected readonly IWalletService walletService;

    /// <summary>
    /// Serviço responsável pela navegação entre páginas do aplicativo.
    /// </summary>
    protected readonly INavigationService navigationService;

    #endregion Fields

    #region Properties

    /// <summary>
    /// Indica se uma operação assíncrona está em andamento.
    /// </summary>
    [ObservableProperty]
    private bool isRunning;

    #endregion Properties

    #region Constructor

    /// <summary>
    /// Construtor padrão que injeta os serviços necessários utilizando o resolvedor de dependência global.
    /// </summary>
    public ViewModel() {
        // Obtém a instância do serviço de carteiras.
        walletService = Service.Get<IWalletService>();
        // Obtém a instância do serviço de navegação.
        navigationService = Service.Get<INavigationService>();
    }

    #endregion Constructor

    #region Virtual Methods

    /// <summary>
    /// Verifica se é permitido realizar a navegação de retorno com base no estado atual da operação.
    /// </summary>
    /// <returns><c>true</c> se não houver operação em andamento (<c>IsRunning == false</c>); caso contrário, <c>false</c>.</returns>
    internal virtual bool CanBack() {
        // Retorna verdadeiro se nenhuma operação assíncrona estiver em execução.
        return !IsRunning;
    }

    /// <summary>
    /// Realiza a navegação para a página anterior e atualiza o ViewModel da nova página ativa.
    /// </summary>
    /// <returns>Uma tarefa assíncrona que representa a operação de navegação e atualização.</returns>
    internal virtual async Task NavigationBack() {
        // Executa a navegação para a página anterior na pilha de navegação.
        await navigationService.NavigateToBack();
        // Obtém a página atual após a navegação, considerando se é a última da pilha ou não.
        var page = Shell.Current.Navigation.NavigationStack.Count <= 1 ? Shell.Current.CurrentPage : Shell.Current.Navigation.NavigationStack[Shell.Current.Navigation.NavigationStack.Count - 1];
        // Se a página atual possui um contexto de dados do tipo ViewModel, chama o método Update.
        if(page is not null && page.BindingContext is ViewModel viewModel) { viewModel.Update(); }
    }

    /// <summary>
    /// Método virtual que pode ser sobrescrito para atualizar dados ou estado quando necessário.
    /// </summary>
    public virtual void Update() {
        // Implementação padrão vazia; deve ser sobrescrita pelos ViewModels derivados.
    }

    #endregion Virtual Methods
}