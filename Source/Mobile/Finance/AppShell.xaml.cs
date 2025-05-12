using Finance.Pages.DangerZone;
using Finance.Pages.Walleting;

namespace Finance;

public partial class AppShell : Shell {

    public AppShell() {
        InitializeComponent();

        Routing.RegisterRoute("create", typeof(CreateWalletPage));
        Routing.RegisterRoute("select", typeof(SelectWalletPage));
        Routing.RegisterRoute("dangerZone", typeof(DangerZonePage));
        Routing.RegisterRoute("dangerZone/delete", typeof(DeleteWalletPage));
    }
}