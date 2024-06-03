using Finance.Models.Operations;
using System.Collections.ObjectModel;

namespace Finance.Models;

internal class Wallet {
    public Guid Id { get; set; }
    public string Name { get; set; }

    #region Brazil Stock

    public float BrazilStocksPrice { get; private set; }
    public float BrazilStocksVariation { get; private set; }
    public float BrazilStocksPerformance { get; private set; }
    public Color BrazilStocksColor { get; } = new(217, 100, 35);
    public int BrazilStocksCount { get => BrazilStocks.Count; }
    public ObservableCollection<BrazilStockOperation> BrazilStocks { get; }

    #endregion Brazil Stock

    #region FII

    public float FIIsPrice { get; private set; }
    public float FIIsVariation { get; private set; }
    public float FIIsPerformance { get; private set; }
    public Color FIIsColor { get; } = new(45, 152, 218);
    public int FIIsCount { get => FIIs.Count; }
    public ObservableCollection<FIIOperation> FIIs { get; }

    #endregion FII

    #region BDR

    public float BDRsPrice { get; private set; }
    public float BDRsVariation { get; private set; }
    public float BDRsPerformance { get; private set; }
    public Color BDRsColor { get; } = new(252, 92, 101);
    public int BDRsCount { get => BDRs.Count; }
    public ObservableCollection<BDROperation> BDRs { get; }

    #endregion BDR

    public Wallet() {
        BrazilStocks = [
            new BrazilStockOperation {
                AppliedDate = new DateTime(2024, 5, 3, 16, 20, 0),
                Ticket = "BBAS3",
                Issuer = "BRASIL ON NM",
                Logo = "https://s3-symbol-logo.tradingview.com/banco-do-brasil--big.svg",
                Price = 28.33f,
                Count = 2,
                IsBuy = true
            }
        ];
        BrazilStocksPrice = 400;
        BrazilStocksVariation = 0.0f;
        BrazilStocksPerformance = 0.03f;

        // ...

        FIIs = [
            new FIIOperation {
                AppliedDate = new DateTime(2024, 4, 12, 14, 24, 0),
                Ticket = "GARE11",
                Issuer = "FII GUARDIANCI ER",
                Count = 2,
                Price = 9.13f,
                IsBuy = true
            }
        ];
        FIIsPrice = 300;
        FIIsVariation = -0.5f;
        FIIsPerformance = 0.4f;

        // ...

        BDRs = [
            new BDROperation {
                AppliedDate = new DateTime(2024, 5, 20, 14, 31, 0),
                Ticket = "ROXO34",
                Issuer = "NU HOLDINGS DRN",
                Logo = "https://s3-symbol-logo.tradingview.com/nu-holdings--big.svg",
                Price = 9.84f,
                Count = 4,
                IsBuy = true
            }
        ];
        BDRsPrice = 40;
        BDRsVariation = 2.5f;
        BDRsPerformance = 2.7f;
    }

    #region Methods

    public Color[] GetPalette() {
        var palette = new List<Color>();
        if(HasBrazilStocks()) { palette.Add(BrazilStocksColor); }
        if(HasFIIs()) { palette.Add(FIIsColor); }
        if(HasBDRs()) { palette.Add(BDRsColor); }
        if(palette.Count == 0) { palette.Add(new Color(0, 0, 0, 50)); }
        return [.. palette];
    }

    public List<PieData> GetWalletPosition() {
        var palette = new List<PieData>();
        if(HasBrazilStocks()) { palette.Add(new PieData("Ações", BrazilStocksPrice)); }
        if(HasFIIs()) { palette.Add(new PieData("FIIs", FIIsPrice)); }
        if(HasBDRs()) { palette.Add(new PieData("BDRs", BDRsPrice)); }
        if(palette.Count == 0) { palette.Add(new PieData("Vazio", 1)); }
        return palette;
    }

    public bool HasBrazilStocks() {
        return BrazilStocks.Count != 0;
    }

    public bool HasFIIs() {
        return FIIs.Count != 0;
    }

    public bool HasBDRs() {
        return BDRs.Count != 0;
    }

    #endregion Methods
}

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

*/