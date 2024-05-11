using Finance.Pages.CreateWallet;
using Finance.Pages.DangerZone;
using Finance.Pages.DangerZone.Delete;
using Finance.Pages.Historic;
using Finance.Pages.SelectWallet;

namespace Finance;

public partial class AppShell : Shell {

    public AppShell() {
        InitializeComponent();

        Routing.RegisterRoute("select", typeof(SelectWalletPage));
        Routing.RegisterRoute("create", typeof(CreateWalletPage));

        Routing.RegisterRoute("historic", typeof(HistoricPage));
        Routing.RegisterRoute("dangerZone", typeof(DangerZonePage));
        Routing.RegisterRoute("dangerZone/delete", typeof(DeleteWalletPage));
    }
}