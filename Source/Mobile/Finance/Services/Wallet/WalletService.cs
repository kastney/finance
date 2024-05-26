using Finance.Models;
using Finance.Models.Operations;
using LiteDB;

namespace Finance.Services;

internal class WalletService : IWalletService {
    private readonly LiteDatabase database;

    public Wallet Wallet { get; set; }

    public WalletService() {
        database = new LiteDatabase(GetConnectionString());
        Operations = [];
    }

    #region Wallet Manager

    public bool Exists() {
        // Verifica se existe carteiras cadastradas.
        var collection = database.GetCollection<Wallet>(nameof(Wallet)).Query().ToEnumerable();
        if(!collection.Any()) { return false; }

        // Verifica se a carteira atual existe.
        Wallet = collection.FirstOrDefault(a => a.Id.Equals(Guid.Parse(Preferences.Get("WalletId", default(string)))));
        if(Wallet is not null) { return true; }

        // Salva a primeira carteira da lista como atual.
        Wallet = collection.FirstOrDefault();
        Preferences.Set("WalletId", Wallet.Id.ToString());
        return true;
    }

    public bool Exists(string name) {
        var collection = database.GetCollection<Wallet>(nameof(Wallet));
        if(collection.FindOne(a => a.Name.Equals(name)) is null) {
            return true;
        } else {
            return false;
        }
    }

    public void SetWallet(Wallet wallet) {
        Preferences.Set("WalletId", wallet.Id.ToString());
    }

    public List<Wallet> AvailableWallets() {
        var collection = database.GetCollection<Wallet>(nameof(Wallet));
        return collection.Find(a => !a.Id.Equals(Wallet.Id)).ToList();
    }

    public void Create(Wallet wallet) {
        var collection = database.GetCollection<Wallet>(nameof(Wallet));
        collection.Insert(wallet);
    }

    public void Delete(Wallet wallet) {
        var collection = database.GetCollection<Wallet>(nameof(Wallet));
        collection.Delete(wallet.Id);
    }

    public static string GetConnectionString() {
        var path = FileSystem.Current.AppDataDirectory;
        return Path.Combine(path, "finance.db");
    }

    #endregion Wallet Manager

    #region Historic

    public List<Operation> Operations { get; }

    public DateTime MinData() {
        return DateTime.Now;
    }

    public DateTime MaxData() {
        return DateTime.Now;
    }

    public void SetOperation(params Operation[] operations) {
        foreach(var operation in operations) {
        }
    }

    #endregion Historic
}