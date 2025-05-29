using Finance.Pages.DangerZone;
using Finance.Pages.Strategy;
using Finance.Pages.Walleting;

namespace Finance;

/// <summary>
/// Classe parcial responsável pela configuração do Shell da aplicação,
/// definindo rotas de navegação para as páginas principais e secundárias.
/// </summary>
public partial class AppShell : Shell {

    #region Constructor

    /// <summary>
    /// Inicializa uma nova instância da classe <see cref="AppShell"/>,
    /// configurando o componente e registrando as rotas de navegação.
    /// </summary>
    public AppShell() {
        // Inicializa os componentes declarados no XAML associado a essa classe.
        InitializeComponent();

        // Registra a rota para a página de criação de carteiras.
        Routing.RegisterRoute("create", typeof(CreateWalletPage));
        // Registra a rota para a página de seleção de carteiras.
        Routing.RegisterRoute("select", typeof(SelectWalletPage));
        // Registra a rota para a página de estratégia da carteira.
        Routing.RegisterRoute("strategy", typeof(StrategyPage));
        // Registra a rota para a página de criação de Grupos de Ativos.
        Routing.RegisterRoute("strategy/add", typeof(CreateAssetGroupPage));
        // Registra a rota para a página da zona de perigo (ações críticas).
        Routing.RegisterRoute("dangerZone", typeof(DangerZonePage));
        // Registra a rota para a página de exclusão de carteiras dentro da zona de perigo.
        Routing.RegisterRoute("dangerZone/delete", typeof(DeleteWalletPage));
    }

    #endregion Constructor
}