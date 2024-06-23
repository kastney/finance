using Finance.Models;

namespace Finance.Services;

internal interface IWalletService {
    Wallet Wallet { get; }

    #region Wallet Manager

    Task<int> Create(Wallet wallet);

    Task Delete(Wallet wallet);

    Task<bool> Exists();

    Task<bool> Exists(string name);

    Task<List<Wallet>> AvailableWallets();

    void SetWallet(Wallet wallet);

    #endregion Wallet Manager

    #region FIIs Manager

    Task<bool> SetBrazilStocksEnabled(bool value);

    Task<bool> SetFIIsEnabled(bool value);

    #endregion FIIs Manager
}