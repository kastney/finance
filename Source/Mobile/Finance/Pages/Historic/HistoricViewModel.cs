using CommunityToolkit.Mvvm.ComponentModel;
using Finance.Models;
using System.Collections.ObjectModel;

namespace Finance.Pages.Historic;

internal partial class HistoricViewModel : ViewModel {

    [ObservableProperty]
    private bool isEmpty;

    public ObservableCollection<Operation> Operations { get; set; }

    public HistoricViewModel() {
        Operations = [];
    }

    public void Loading() {
        Operations.Clear();

        #region

        Operations.Add(new FixedIncomeOperation {
            AppliedDate = new DateTime(2023, 10, 3, 10, 34, 0),
            DueDate = new DateTime(2025, 9, 23),
            Issuer = "BANCO INTER S.A.",
            TitleType = TitleType.CDB,
            FixedType = FixedType.Postfixed,
            Price = 100,
            Rate = 100,
            IndexerType = IndexerType.CDI,
            IsBuy = true
        });

        //// BTCI11
        //Operations.Add(new Operation {
        //    AppliedDate = new DateTime(2024, 4, 12, 14, 0, 0),
        //    Type = AssetType.FII
        //});

        //// VGHF11
        //Operations.Add(new Operation {
        //    AppliedDate = new DateTime(2024, 4, 12, 14, 5, 0),
        //    Type = AssetType.FII
        //});

        //// XPCA11
        //Operations.Add(new Operation {
        //    AppliedDate = new DateTime(2024, 4, 12, 14, 6, 0),
        //    Type = AssetType.FII
        //});

        //// VINO11
        //Operations.Add(new Operation {
        //    AppliedDate = new DateTime(2024, 4, 12, 14, 16, 0),
        //    Type = AssetType.FII
        //});

        //// GARE11
        //Operations.Add(new Operation {
        //    AppliedDate = new DateTime(2024, 4, 12, 14, 24, 0),
        //    Type = AssetType.FII
        //});

        Operations.Add(new StockOperation {
            AppliedDate = new DateTime(2024, 5, 3, 16, 20, 0),
            Issuer = "BRASIL ON NM",
            Ticket = "BBAS3",
            Count = 2,
            Price = 28.33f,
            IsBuy = true,
            Logo = "https://s3-symbol-logo.tradingview.com/banco-do-brasil--big.svg"
        });

        Operations.Add(new StockOperation {
            AppliedDate = new DateTime(2024, 5, 3, 16, 21, 0),
            Issuer = "ITAUSA ON N1",
            Ticket = "ITSA3",
            Count = 5,
            Price = 9.87f,
            IsBuy = true,
            Logo = "https://s3-symbol-logo.tradingview.com/itausa--big.svg"
        });

        //// GARE11
        //Operations.Add(new Operation {
        //    AppliedDate = new DateTime(2024, 5, 3, 16, 24, 0),
        //    Type = AssetType.FII
        //});

        //// XPCA11
        //Operations.Add(new Operation {
        //    AppliedDate = new DateTime(2024, 5, 3, 16, 25, 0),
        //    Type = AssetType.FII
        //});

        //// BTCI11
        //Operations.Add(new Operation {
        //    AppliedDate = new DateTime(2024, 5, 3, 16, 26, 0),
        //    Type = AssetType.FII
        //});

        Operations.Add(new StockOperation {
            AppliedDate = new DateTime(2024, 5, 13, 15, 38, 0),
            Issuer = "TAESA ON ED N2",
            Ticket = "TAEE3",
            Count = 5,
            Price = 11.53f,
            IsBuy = true,
            Logo = "https://s3-symbol-logo.tradingview.com/taesa--big.svg"
        });

        Operations.Add(new FixedIncomeOperation {
            AppliedDate = new DateTime(2024, 5, 14, 13, 34, 0),
            DueDate = new DateTime(2027, 5, 14),
            Issuer = "PICPAY BANK",
            TitleType = TitleType.CDB,
            FixedType = FixedType.Postfixed,
            Price = 1400,
            Rate = 102,
            IndexerType = IndexerType.CDI,
            IsBuy = true
        });

        Operations.Add(new StockOperation {
            AppliedDate = new DateTime(2024, 5, 15, 16, 19, 0),
            Issuer = "PETROBRAS PN EDR N2",
            Ticket = "PETR4",
            Count = 2,
            Price = 38.45f,
            IsBuy = true,
            Logo = "https://s3-symbol-logo.tradingview.com/brasileiro-petrobras--big.svg"
        });

        Operations.Add(new StockOperation {
            AppliedDate = new DateTime(2024, 5, 17, 11, 45, 0),
            Issuer = "BRASIL ON NM",
            Ticket = "BBAS3",
            Count = 2,
            Price = 27.69f,
            IsBuy = true,
            Logo = "https://s3-symbol-logo.tradingview.com/banco-do-brasil--big.svg"
        });

        Operations.Add(new StockOperation {
            AppliedDate = new DateTime(2024, 5, 17, 16, 51, 0),
            Issuer = "SANEPAR PN N2",
            Ticket = "SAPR4",
            Count = 4,
            Price = 5.8f,
            IsBuy = true,
            Logo = "https://s3-symbol-logo.tradingview.com/sanepar--big.svg"
        });

        #endregion

        IsEmpty = Operations.Count == 0;
    }
}