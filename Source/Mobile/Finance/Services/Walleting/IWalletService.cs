using Finance.Models;

namespace Finance.Services.Walleting;

internal interface IWalletService {

    #region Properties

    Wallet Wallet { get; }

    #endregion Properties

    #region Management Methods

    Task<bool> Exists();

    Task<bool> Exists(string name);

    Task<int> Create(Wallet wallet);

    Task Delete(Wallet wallet);

    void SetWallet(Wallet wallet);

    Task<List<Wallet>> AvailableWallets();

    #endregion Management Methods
}