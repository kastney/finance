using Finance.Models;

namespace Finance.Services;

internal class WalletService : IWalletService {

    public Wallet GetCurrent() {
        return new Wallet { Name = "Oi" };
    }
}