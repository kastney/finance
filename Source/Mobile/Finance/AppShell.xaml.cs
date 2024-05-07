using Finance.Pages.CreateWallet;
using Finance.Pages.DangerZone;
using Finance.Pages.DangerZone.Delete;
using Finance.Pages.SelectWallet;

namespace Finance;

public partial class AppShell : Shell {

    public AppShell() {
        InitializeComponent();

        appNameLabel.Text = AppInfo.Current.Name;
        versionLabel.Text = $"Versão {AppInfo.Current.VersionString} ({AppInfo.Current.BuildString})";

        Routing.RegisterRoute("select", typeof(SelectWalletPage));
        Routing.RegisterRoute("create", typeof(CreateWalletPage));
        Routing.RegisterRoute("dangerZone", typeof(DangerZonePage));
        Routing.RegisterRoute("dangerZone/delete", typeof(DeleteWalletPage));
    }
}