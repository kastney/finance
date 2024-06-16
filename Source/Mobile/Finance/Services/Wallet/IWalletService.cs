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
}