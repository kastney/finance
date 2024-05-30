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
        if(Wallet is not null) {

            #region Data

            #region BDR

            AddOperation(new BDROperation {
                AppliedDate = new DateTime(2024, 5, 20, 14, 31, 0),
                Ticket = "ROXO34",
                Issuer = "NU HOLDINGS DRN",
                Logo = "https://s3-symbol-logo.tradingview.com/nu-holdings--big.svg",
                Price = 9.84f,
                Count = 4,
                IsBuy = true
            });

            #endregion BDR

            #region Brazil Stock

            AddOperation(new BrazilStockOperation {
                AppliedDate = new DateTime(2024, 5, 3, 16, 20, 0),
                Ticket = "BBAS3",
                Issuer = "BRASIL ON NM",
                Logo = "https://s3-symbol-logo.tradingview.com/banco-do-brasil--big.svg",
                Price = 28.33f,
                Count = 2,
                IsBuy = true
            });

            AddOperation(new BrazilStockOperation {
                AppliedDate = new DateTime(2024, 5, 3, 16, 21, 0),
                Ticket = "ITSA3",
                Issuer = "ITAUSA ON N1",
                Logo = "https://s3-symbol-logo.tradingview.com/itausa--big.svg",
                Price = 9.87f,
                Count = 5,
                IsBuy = true
            });

            AddOperation(new BrazilStockOperation {
                AppliedDate = new DateTime(2024, 5, 13, 15, 38, 0),
                Ticket = "TAEE3",
                Issuer = "TAESA ON ED N2",
                Logo = "https://s3-symbol-logo.tradingview.com/taesa--big.svg",
                Price = 11.53f,
                Count = 5,
                IsBuy = true
            });

            AddOperation(new BrazilStockOperation {
                AppliedDate = new DateTime(2024, 5, 15, 16, 19, 0),
                Ticket = "PETR4",
                Issuer = "PETROBRAS PN EDR N2",
                Logo = "https://s3-symbol-logo.tradingview.com/brasileiro-petrobras--big.svg",
                Price = 38.45f,
                Count = 2,
                IsBuy = true
            });

            AddOperation(new BrazilStockOperation {
                AppliedDate = new DateTime(2024, 5, 17, 11, 43, 0),
                Ticket = "BBAS3",
                Issuer = "BRASIL ON NM",
                Logo = "https://s3-symbol-logo.tradingview.com/banco-do-brasil--big.svg",
                Price = 27.69f,
                Count = 2,
                IsBuy = true
            });

            AddOperation(new BrazilStockOperation {
                AppliedDate = new DateTime(2024, 5, 17, 16, 51, 0),
                Ticket = "SAPR4",
                Issuer = "SANEPAR PN N2",
                Logo = "https://s3-symbol-logo.tradingview.com/sanepar--big.svg",
                Price = 5.8f,
                Count = 4,
                IsBuy = true
            });

            AddOperation(new BrazilStockOperation {
                AppliedDate = new DateTime(2024, 5, 23, 14, 26, 0),
                Ticket = "PETR4",
                Issuer = "PETROBRAS PN EDR N2",
                Logo = "https://s3-symbol-logo.tradingview.com/brasileiro-petrobras--big.svg",
                Price = 37,
                Count = 2,
                IsBuy = true
            });

            AddOperation(new BrazilStockOperation {
                AppliedDate = new DateTime(2024, 5, 28, 11, 49, 0),
                Ticket = "SAPR4",
                Issuer = "SANEPAR PN N2",
                Logo = "https://s3-symbol-logo.tradingview.com/sanepar--big.svg",
                Price = 5.65f,
                Count = 6,
                IsBuy = true
            });

            #endregion Brazil Stock

            #region FII

            AddOperation(new FIIOperation {
                AppliedDate = new DateTime(2024, 4, 12, 14, 0, 0),
                Ticket = "BTCI11",
                Issuer = "FII BTG CRI CI ER",
                Count = 2,
                Price = 10.24f,
                IsBuy = true
            });

            AddOperation(new FIIOperation {
                AppliedDate = new DateTime(2024, 4, 12, 14, 5, 0),
                Ticket = "VGHF11",
                Issuer = "FII VALOR HECI ER",
                Count = 2,
                Price = 9.18f,
                IsBuy = true
            });

            AddOperation(new FIIOperation {
                AppliedDate = new DateTime(2024, 4, 12, 14, 6, 0),
                Ticket = "XPCA11",
                Issuer = "FIAGRO XP CACI ER",
                Count = 2,
                Price = 8.62f,
                IsBuy = true
            });

            AddOperation(new FIIOperation {
                AppliedDate = new DateTime(2024, 4, 12, 14, 16, 0),
                Ticket = "VINO11",
                Issuer = "FII VINCI OFCI ER",
                Count = 2,
                Price = 8.16f,
                IsBuy = true
            });

            AddOperation(new FIIOperation {
                AppliedDate = new DateTime(2024, 4, 12, 14, 24, 0),
                Ticket = "GARE11",
                Issuer = "FII GUARDIANCI ER",
                Count = 2,
                Price = 9.13f,
                IsBuy = true
            });

            AddOperation(new FIIOperation {
                AppliedDate = new DateTime(2024, 5, 3, 16, 24, 0),
                Ticket = "GARE11",
                Issuer = "FII GUARDIANCI ER",
                Count = 4,
                Price = 9.07f,
                IsBuy = true
            });

            AddOperation(new FIIOperation {
                AppliedDate = new DateTime(2024, 5, 3, 16, 25, 0),
                Ticket = "XPCA11",
                Issuer = "FIAGRO XP CACI ER",
                Count = 4,
                Price = 8.57f,
                IsBuy = true
            });

            AddOperation(new FIIOperation {
                AppliedDate = new DateTime(2024, 5, 3, 16, 26, 0),
                Ticket = "BTCI11",
                Issuer = "FII BTG CRI CI ER",
                Count = 3,
                Price = 10.21f,
                IsBuy = true
            });

            AddOperation(new FIIOperation {
                AppliedDate = new DateTime(2024, 5, 28, 13, 54, 0),
                Ticket = "GARE11",
                Issuer = "FII GUARDIANCI ER",
                Count = 6,
                Price = 9.03f,
                IsBuy = true
            });

            #endregion FII

            #region CDB

            AddOperation(new CDBOperation {
                Issuer = "BANCO INTER S.A.",
                FixedType = FixedType.Postfixed,
                Price = 100,
                AppliedDate = new DateTime(2023, 10, 3, 10, 23, 0),
                DueDate = new DateTime(2025, 9, 23),
                IndexerType = IndexerType.CDI,
                Rate = 100,
                IsBuy = true
            });

            AddOperation(new CDBOperation {
                Issuer = "PICPAY BANK",
                FixedType = FixedType.Postfixed,
                Price = 1400,
                AppliedDate = new DateTime(2024, 5, 14, 13, 34, 0),
                DueDate = new DateTime(2027, 5, 14),
                IndexerType = IndexerType.CDI,
                Rate = 102,
                IsBuy = true
            });

            #endregion CDB

            #endregion Data

            return true;
        }

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

    public DateTime MinData() {
        if(Wallet.Operations.Count == 0) {
            return DateTime.Now;
        }

        var year = Wallet.Operations.First();
        var month = year.Value.First();
        var day = month.Value.First();
        return new DateTime(year.Key, month.Key, day.Key);
    }

    public DateTime MaxData() {
        if(Wallet.Operations.Count == 0) {
            return DateTime.Now;
        }

        var year = Wallet.Operations.Last();
        var month = year.Value.Last();
        var day = month.Value.Last();
        return new DateTime(year.Key, month.Key, day.Key);
    }

    public bool AddOperation(Operation operation) {
        try {
            if(!Wallet.Operations.ContainsKey(operation.AppliedDate.Year)) {
                Wallet.Operations.Add(operation.AppliedDate.Year, []);
            }
            var year = Wallet.Operations[operation.AppliedDate.Year];
            if(!year.ContainsKey(operation.AppliedDate.Month)) {
                year.Add(operation.AppliedDate.Month, []);
            }
            var month = year[operation.AppliedDate.Month];
            if(!month.ContainsKey(operation.AppliedDate.Day)) {
                month.Add(operation.AppliedDate.Day, []);
            }
            var day = month[operation.AppliedDate.Day];
            while(day.ContainsKey(operation.AppliedDate)) {
                operation.AppliedDate = operation.AppliedDate.AddMilliseconds(1);
            }
            day.Add(operation.AppliedDate, operation);
            return true;
        } catch {
            return false;
        }
    }

    #endregion Historic
}