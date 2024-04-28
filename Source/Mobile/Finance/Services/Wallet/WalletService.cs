using Finance.Models;
using LiteDB;

namespace Finance.Services;

internal class WalletService : IWalletService {
    private readonly LiteDatabase database;

    public WalletService() {
        database = new LiteDatabase(GetConnectionString());
    }

    public Wallet GetCurrent() {
        // Verifica se existe carteiras cadastradas.
        var collection = database.GetCollection<Wallet>(nameof(Wallet)).Query().ToEnumerable();
        if(!collection.Any()) { return null; }

        // Verifica se a carteira atual existe.
        var wallet = collection.FirstOrDefault(a => a.Id.Equals(Guid.Parse(Preferences.Get("WalletId", default(string)))));
        if(wallet is not null) { return wallet; }

        // Salva a primeira carteira da lista como atual.
        wallet = collection.FirstOrDefault();
        Preferences.Set("WalletId", wallet.Id.ToString());
        return wallet;
    }

    public static string GetConnectionString() {
        var path = FileSystem.Current.AppDataDirectory;
        return Path.Combine(path, "finance.db");
    }
}