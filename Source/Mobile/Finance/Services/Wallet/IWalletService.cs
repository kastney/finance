using Finance.Models;

namespace Finance.Services;

internal interface IWalletService {

    Wallet GetCurrent();
}