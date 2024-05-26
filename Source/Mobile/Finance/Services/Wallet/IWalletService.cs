using Finance.Models;
using Finance.Models.Operations;

namespace Finance.Services;

internal interface IWalletService {
    Wallet Wallet { get; }
    List<Operation> Operations { get; }

    bool Exists();

    void SetWallet(Wallet wallet);

    void Create(Wallet wallet);

    void Delete(Wallet wallet);

    bool Exists(string name);

    List<Wallet> AvailableWallets();

    #region Historic

    DateTime MinData();

    DateTime MaxData();

    public void SetOperation(params Operation[] operations);

    #endregion Historic
}