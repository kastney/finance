using SQLite;

namespace Finance.Models;

[Table("wallets")]
internal class Wallet {

    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    [MaxLength(50), Unique]
    public string Name { get; set; }

    #region FIIs

    [NotNull]
    public bool FIIsEnabled { get; set; }

    [Ignore]
    public string FIIsName { get; } = "FIIs";

    [Ignore]
    public Color FIIsColor { get; } = new(45, 152, 218);

    #endregion FIIs

    #region Initialization

    public Wallet() {
        FIIsEnabled = false;
    }

    #endregion Initialization

    #region Methods

    internal Color[] GetPalette() {
        var palette = new List<Color>();
        if(FIIsEnabled) { palette.Add(FIIsColor); }
        if(palette.Count == 0) { palette.Add(new Color(0, 0, 0, 50)); }
        return [.. palette];
    }

    internal List<PieData> GetWalletPosition() {
        var palette = new List<PieData>();
        if(FIIsEnabled) { palette.Add(new PieData(FIIsName, 1)); }
        if(palette.Count == 0) { palette.Add(new PieData("Vazio", 1)); }
        return palette;
    }

    #endregion Methods
}

//#region Brazil Stock

//public int BrazilStocksCount { get => BrazilStocks.Count; }

//public float BrazilStocksPrice { get; private set; }
//public float BrazilStocksVariation { get; private set; }
//public float BrazilStocksPerformance { get; private set; }

//public ObservableCollection<BrazilStockOperation> BrazilStocks { get; }

//#endregion Brazil Stock

//#region FII

//public int FIIsCount { get => FIIs.Count; }

//public float FIIsPrice { get; private set; }
//public float FIIsVariation { get; private set; }
//public float FIIsPerformance { get; private set; }

//public ObservableCollection<FIIOperation> FIIs { get; }

//#endregion FII

//#region BDR

//public string BDRsName { get; } = "BDRs";
//public Color BDRsColor { get; } = new(252, 80, 80);

//public int BDRsCount { get => BDRs.Count; }

//public float BDRsPrice { get; private set; }
//public float BDRsVariation { get; private set; }
//public float BDRsPerformance { get; private set; }

//public ObservableCollection<BDROperation> BDRs { get; }

//#endregion BDR

//#region FixedIncome

//public string FixedIncomeName { get; } = "Renda Fixa";
//public Color FixedIncomeColor { get; } = new(3, 143, 89);

//public int FixedIncomeCount { get => FixedIncome.Count; }

//public float FixedIncomePrice { get; private set; }
//public float FixedIncomeVariation { get; private set; }
//public float FixedIncomePerformance { get; private set; }

//public ObservableCollection<FixedIncomeOperation> FixedIncome { get; }

//#endregion FixedIncome

//#region Crypto

//public string CryptoName { get; } = "Criptomoedas";
//public Color CryptoColor { get; } = new(225, 111, 0);

//public int CryptoCount { get => Crypto.Count; }

//public float CryptoPrice { get; private set; }
//public float CryptoVariation { get; private set; }
//public float CryptoPerformance { get; private set; }

//public ObservableCollection<Operation> Crypto { get; }

//#endregion Crypto

//public Wallet() {
//    BrazilStocks = [
//        new BrazilStockOperation {
//            AppliedDate = new DateTime(2024, 5, 3, 16, 20, 0),
//            Ticket = "BBAS3",
//            Issuer = "BRASIL ON NM",
//            Logo = "https://s3-symbol-logo.tradingview.com/banco-do-brasil--big.svg",
//            Price = 28.33f,
//            Count = 2,
//            IsBuy = true
//        }
//    ];
//    BrazilStocksPrice = 400;
//    BrazilStocksVariation = 0.0f;
//    BrazilStocksPerformance = 0.03f;

//    // ...

//    FIIs = [
//        new FIIOperation {
//            AppliedDate = new DateTime(2024, 4, 12, 14, 24, 0),
//            Ticket = "GARE11",
//            Issuer = "FII GUARDIANCI ER",
//            Count = 2,
//            Price = 9.13f,
//            IsBuy = true
//        }
//    ];
//    FIIsPrice = 300;
//    FIIsVariation = -0.5f;
//    FIIsPerformance = 0.4f;

//    // ...

//    BDRs = [
//        new BDROperation {
//            AppliedDate = new DateTime(2024, 5, 20, 14, 31, 0),
//            Ticket = "ROXO34",
//            Issuer = "NU HOLDINGS DRN",
//            Logo = "https://s3-symbol-logo.tradingview.com/nu-holdings--big.svg",
//            Price = 9.84f,
//            Count = 4,
//            IsBuy = true
//        }
//    ];
//    BDRsPrice = 40;
//    BDRsVariation = 2.5f;
//    BDRsPerformance = 2.7f;

//    // ...

//    FixedIncome = [
//        new CDBOperation {
//            Issuer = "PICPAY BANK",
//            FixedType = FixedType.Postfixed,
//            Price = 1400,
//            AppliedDate = new DateTime(2024, 5, 14, 13, 34, 0),
//            DueDate = new DateTime(2027, 5, 14),
//            IndexerType = IndexerType.CDI,
//            Rate = 102,
//            IsBuy = true
//        }
//    ];
//    FixedIncomePrice = 1500;
//    FixedIncomeVariation = 2.5f;
//    FixedIncomePerformance = 2.7f;

//    // ...

//    Crypto = [
//        new CDBOperation {
//            Issuer = "PICPAY BANK",
//            FixedType = FixedType.Postfixed,
//            Price = 1400,
//            AppliedDate = new DateTime(2024, 5, 14, 13, 34, 0),
//            DueDate = new DateTime(2027, 5, 14),
//            IndexerType = IndexerType.CDI,
//            Rate = 102,
//            IsBuy = true
//        }
//    ];
//    CryptoPrice = 50;
//    CryptoVariation = 42.5f;
//    CryptoPerformance = 42.7f;
//}

//#region Methods

//public Color[] GetPalette() {
//    var palette = new List<Color>();
//    //    if(HasBrazilStocks()) { palette.Add(BrazilStocksColor); }
//    //    if(HasBDRs()) { palette.Add(BDRsColor); }
//    //    if(HasFIIs()) { palette.Add(FIIsColor); }
//    //    if(HasFixedIncome()) { palette.Add(FixedIncomeColor); }
//    //    if(HasCrypto()) { palette.Add(CryptoColor); }
//    if(palette.Count == 0) { palette.Add(new Color(0, 0, 0, 50)); }
//    return [.. palette];
//}

//public List<PieData> GetWalletPosition() {
//    var palette = new List<PieData>();
//    if(HasBrazilStocks()) { palette.Add(new PieData(BrazilStocksName, BrazilStocksPrice)); }
//    if(HasBDRs()) { palette.Add(new PieData(BDRsName, BDRsPrice)); }
//    if(HasFIIs()) { palette.Add(new PieData(FIIsName, FIIsPrice)); }
//    if(HasFixedIncome()) { palette.Add(new PieData(FixedIncomeName, FixedIncomePrice)); }
//    if(HasCrypto()) { palette.Add(new PieData(CryptoName, CryptoPrice)); }
//    if(palette.Count == 0) { palette.Add(new PieData("Vazio", 1)); }
//    return palette;
//}

//public bool HasBrazilStocks() {
//    return BrazilStocks.Count != 0;
//}

//public bool HasFIIs() {
//    return FIIs.Count != 0;
//}

//public bool HasBDRs() {
//    return BDRs.Count != 0;
//}

//public bool HasFixedIncome() {
//    return FixedIncome.Count != 0;
//}

//public bool HasCrypto() {
//    return Crypto.Count != 0;
//}

//#endregion Methods
/*
 * //public SortedDictionary<int, SortedDictionary<int, SortedDictionary<int, SortedDictionary<DateTime, Operation>>>> Operations { get; set; }

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

            AddOperation(new BrazilStockOperation {
                AppliedDate = new DateTime(2024, 6, 5, 11, 19, 0),
                Ticket = "VALE3",
                Issuer = "VALE ON NM",
                Logo = "https://s3-symbol-logo.tradingview.com/vale--big.svg",
                Price = 60.84f,
                Count = 4,
                IsBuy = true
            });

            AddOperation(new BrazilStockOperation {
                AppliedDate = new DateTime(2024, 6, 6, 10, 29, 0),
                Ticket = "PETR4",
                Issuer = "PETROBRAS PN EDR N2",
                Logo = "https://s3-symbol-logo.tradingview.com/brasileiro-petrobras--big.svg",
                Price = 38.5f,
                Count = 4,
                IsBuy = false
            });

            new BrazilStockOperation {
                AppliedDate = new DateTime(2024, 6, 7, 13, 52, 0),
                Ticket = "BBAS3",
                Issuer = "BRASIL ON NM",
                Logo = "https://s3-symbol-logo.tradingview.com/banco-do-brasil--big.svg",
                Price = 27.37f,
                Count = 4,
                IsBuy = true
            }

            new BrazilStockOperation {
                AppliedDate = new DateTime(2024, 6, 12, 11, 23, 0),
                Ticket = "BBAS3",
                Issuer = "BRASIL ON NM",
                Logo = "https://s3-symbol-logo.tradingview.com/banco-do-brasil--big.svg",
                Price = 26.72f,
                Count = 2,
                IsBuy = true
            }

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

            AddOperation(new FIIOperation {
                AppliedDate = new DateTime(2024, 6, 10, 12, 07, 0),
                Ticket = "RZTR11",
                Issuer = "FII RIZA TX CI ER",
                Count = 2,
                Price = 94,65f,
                IsBuy = true
            });

            AddOperation(new FIIOperation {
                AppliedDate = new DateTime(2024, 6, 12, 10, 48, 0),
                Ticket = "GARE11",
                Issuer = "FII GUARDIANCI ER",
                Count = 11,
                Price = 9.03f,
                IsBuy = true
            });

            AddOperation(new FIIOperation {
                AppliedDate = new DateTime(2024, 6, 18, 16, 43, 0),
                Ticket = "RZTR11",
                Issuer = "FII RIZA TX CI ER",
                Count = 4,
                Price = 92,10f,
                IsBuy = true
            });

            AddOperation(new FIIOperation {
                AppliedDate = new DateTime(2024, 6, 20, 17, 37, 0),
                Ticket = "GARE11",
                Issuer = "FII GUARDIANCI ER",
                Count = 23,
                Price = 8.98f,
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

*/