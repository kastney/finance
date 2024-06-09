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

    private async Task Init() {
        if(database is not null) { return; }
        database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        //await database.DropTableAsync<Wallet>();
        await database.CreateTableAsync<Wallet>();
    }

    //#region Historic
    //public void AddOperation(Operation operation) {
    //operation.Id = Guid.NewGuid();
    //operation.WalletId = Wallet.Id;
    //var collection = database.GetCollection<Operation>("Historic");
    //collection.Insert(operation);
    //}
    //public IEnumerable<Operation> GetHistoric() {
    //var collection = database.GetCollection<Operation>("Historic");
    //return collection.FindAll();
    //}
    //#endregion Historic
    //public static string GetConnectionString() {
    //var path = FileSystem.Current.AppDataDirectory;
    //return Path.Combine(path, "finance.db");
    //}
}