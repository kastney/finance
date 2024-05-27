using Finance.Models;
using Finance.Models.Operations;

namespace Finance.Services;

internal interface IWalletService {
    Wallet Wallet { get; }

    #region Wallet Manager

    bool Exists();

    void SetWallet(Wallet wallet);

    void Create(Wallet wallet);

    void Delete(Wallet wallet);

    bool Exists(string name);

    List<Wallet> AvailableWallets();

    #endregion Wallet Manager

    #region Historic

    DateTime MinData();

    DateTime MaxData();

    public void AddOperation(params Operation[] operations);

    #endregion Historic
}