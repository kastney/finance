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
            Issuer = "Banco Inter S.A.",
            TitleType = TitleType.CDB,
            FixedType = FixedType.Postfixed,
            Value = 100,
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
            IsBuy = true
        });
        //// BBAS3F

        //// ITSA3F
        //Operations.Add(new Operation {
        //    AppliedDate = new DateTime(2024, 5, 3, 16, 21, 0),
        //    Type = AssetType.Stock
        //});

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

        //// TAEE3F
        //Operations.Add(new Operation {
        //    AppliedDate = new DateTime(2024, 5, 13, 15, 38, 0),
        //    Type = AssetType.Stock
        //});

        Operations.Add(new FixedIncomeOperation {
            AppliedDate = new DateTime(2024, 5, 14, 13, 34, 0),
            DueDate = new DateTime(2027, 5, 14),
            Issuer = "PicPay Bank",
            TitleType = TitleType.CDB,
            FixedType = FixedType.Postfixed,
            Value = 1400,
            Rate = 102,
            IndexerType = IndexerType.CDI,
            IsBuy = true
        });

        #endregion

        IsEmpty = Operations.Count == 0;
    }
}