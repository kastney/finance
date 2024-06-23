using Finance.Models;
using SQLite;

namespace Finance.Services;

internal class WalletService : IWalletService {
    private SQLiteAsyncConnection database;

    public Wallet Wallet { get; set; }

    #region Wallet Manager

    public async Task<int> Create(Wallet wallet) {
        await Init();
        return await database.InsertAsync(wallet);
    }

    public async Task Delete(Wallet wallet) {
        await Init();
        await database.DeleteAsync<Wallet>(wallet.Id);
    }

    public async Task<bool> Exists() {
        await Init();
        // Verifica se existe carteiras cadastradas.
        var result = await database.QueryAsync<Wallet>("SELECT * FROM wallets LIMIT 1");
        if(result.Count == 0) { return false; }
        // Verifica se a carteira atual existe.
        Wallet = (await database.QueryAsync<Wallet>($"SELECT * FROM wallets WHERE id='{Preferences.Get("WalletId", default(string))}' LIMIT 1")).FirstOrDefault();
        if(Wallet is not null) { return true; }
        // Salva a primeira carteira da lista como atual.
        Wallet = result.FirstOrDefault();
        Preferences.Set("WalletId", Wallet.Id.ToString());
        return true;
    }

    public async Task<bool> Exists(string name) {
        await Init();
        var result = await database.QueryAsync<Wallet>($"SELECT id FROM wallets WHERE name='{name}' LIMIT 1");
        return result.Count != 0;
    }

    public async Task<List<Wallet>> AvailableWallets() {
        await Init();
        return await database.QueryAsync<Wallet>($"SELECT id,name FROM wallets WHERE name!='{Wallet.Name}'");
    }

    public void SetWallet(Wallet wallet) {
        Preferences.Set("WalletId", wallet.Id.ToString());
    }

    #endregion Wallet Manager

    #region FIIs Manager

    public async Task<bool> SetBrazilStocksEnabled(bool value) {
        try {
            await Init();
            Wallet.BrazilStocksEnabled = value;
            await database.UpdateAsync(Wallet);
            return true;
        } catch {
            return false;
        }
    }

    public async Task<bool> SetFIIsEnabled(bool value) {
        try {
            await Init();
            Wallet.FIIsEnabled = value;
            await database.UpdateAsync(Wallet);
            return true;
        } catch {
            return false;
        }
    }

    #endregion FIIs Manager

    private async Task Init() {
        if(database is not null) { return; }
        database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        //await database.DropTableAsync<Wallet>();
        await database.CreateTableAsync<Wallet>();
    }
}