using Finance.Pages.DangerZone;
using Finance.Pages.DangerZone.Delete;
using Finance.Pages.Historic;
using Finance.Pages.Strategy;
using Finance.Pages.Walleting;

namespace Finance;

public partial class AppShell : Shell {

    public AppShell() {
        InitializeComponent();

        Routing.RegisterRoute("create", typeof(CreateWalletPage));
        Routing.RegisterRoute("select", typeof(SelectWalletPage));

        Routing.RegisterRoute("strategy", typeof(StrategyPage));
        Routing.RegisterRoute("historic", typeof(HistoricPage));
        Routing.RegisterRoute("dangerZone", typeof(DangerZonePage));
        Routing.RegisterRoute("dangerZone/delete", typeof(DeleteWalletPage));
    }
}