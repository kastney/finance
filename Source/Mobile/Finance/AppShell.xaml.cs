using Finance.Pages.SelectWallet;

namespace Finance;

public partial class AppShell : Shell {

    public AppShell() {
        InitializeComponent();
        Routing.RegisterRoute("select", typeof(SelectWalletPage));
    }
}