using Finance.Pages.CreateWallet;
using Finance.Pages.DangerZone;
using Finance.Pages.DangerZone.Delete;
using Finance.Pages.Extract;
using Finance.Pages.SelectWallet;

namespace Finance;

public partial class AppShell : Shell {

    public AppShell() {
        InitializeComponent();

        Routing.RegisterRoute("select", typeof(SelectWalletPage));
        Routing.RegisterRoute("create", typeof(CreateWalletPage));

        Routing.RegisterRoute("extract", typeof(ExtractPage));
        Routing.RegisterRoute("dangerZone", typeof(DangerZonePage));
        Routing.RegisterRoute("dangerZone/delete", typeof(DeleteWalletPage));
    }
}