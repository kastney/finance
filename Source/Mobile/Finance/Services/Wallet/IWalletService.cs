using Finance.Models;

namespace Finance.Services;

internal interface IWalletService {

    Wallet GetCurrent();

    void SetCurrent(Wallet wallet);

    void Create(Wallet wallet);

    void Delete(Wallet wallet);
}