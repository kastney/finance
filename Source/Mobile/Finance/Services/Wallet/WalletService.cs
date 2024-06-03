using Finance.Enumerations;
using Finance.Models;
using Finance.Models.Operations;
using LiteDB;

namespace Finance.Services;

internal class WalletService : IWalletService {
    private readonly LiteDatabase database;

    public Wallet Wallet { get; set; }

    public WalletService() {
        database = new LiteDatabase(GetConnectionString());
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

    //public DateTime MinData() {
    //if(Wallet.Operations.Count == 0) {
    //    return DateTime.Now;
    //}

    //var year = Wallet.Operations.First();
    //var month = year.Value.First();
    //var day = month.Value.First();
    //return new DateTime(year.Key, month.Key, day.Key);
    //}

    //public DateTime MaxData() {
    //    if(Wallet.Operations.Count == 0) {
    //        return DateTime.Now;
    //    }

    //    var year = Wallet.Operations.Last();
    //    var month = year.Value.Last();
    //    var day = month.Value.Last();
    //    return new DateTime(year.Key, month.Key, day.Key);
    //}

    //public bool AddOperation(Operation operation) {
    //    try {
    //        if(!Wallet.Operations.ContainsKey(operation.AppliedDate.Year)) {
    //            Wallet.Operations.Add(operation.AppliedDate.Year, []);
    //        }
    //        var year = Wallet.Operations[operation.AppliedDate.Year];
    //        if(!year.ContainsKey(operation.AppliedDate.Month)) {
    //            year.Add(operation.AppliedDate.Month, []);
    //        }
    //        var month = year[operation.AppliedDate.Month];
    //        if(!month.ContainsKey(operation.AppliedDate.Day)) {
    //            month.Add(operation.AppliedDate.Day, []);
    //        }
    //        var day = month[operation.AppliedDate.Day];
    //        while(day.ContainsKey(operation.AppliedDate)) {
    //            operation.AppliedDate = operation.AppliedDate.AddMilliseconds(1);
    //        }
    //        day.Add(operation.AppliedDate, operation);
    //        return true;
    //    } catch {
    //        return false;
    //    }
    //}

    #endregion Historic
}