using CommunityToolkit.Mvvm.ComponentModel;
using Finance.Enumerations;
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

        // CDB
        Operations.Add(new Operation {
            ApplicationDate = new DateTime(2023, 10, 3, 10, 0, 0),
            DueDate = new DateTime(2025, 9, 23),
            Type = AssetType.CDB
        });

        // BTCI11
        Operations.Add(new Operation {
            ApplicationDate = new DateTime(2024, 4, 12, 14, 0, 0),
            Type = AssetType.FII
        });

        // VGHF11
        Operations.Add(new Operation {
            ApplicationDate = new DateTime(2024, 4, 12, 14, 5, 0),
            Type = AssetType.FII
        });

        // XPCA11
        Operations.Add(new Operation {
            ApplicationDate = new DateTime(2024, 4, 12, 14, 6, 0),
            Type = AssetType.FII
        });

        // VINO11
        Operations.Add(new Operation {
            ApplicationDate = new DateTime(2024, 4, 12, 14, 16, 0),
            Type = AssetType.FII
        });

        // GARE11
        Operations.Add(new Operation {
            ApplicationDate = new DateTime(2024, 4, 12, 14, 24, 0),
            Type = AssetType.FII
        });

        // BBAS3F
        Operations.Add(new Operation {
            ApplicationDate = new DateTime(2024, 5, 3, 16, 20, 0),
            Type = AssetType.Stock
        });

        // ITSA3F
        Operations.Add(new Operation {
            ApplicationDate = new DateTime(2024, 5, 3, 16, 21, 0),
            Type = AssetType.Stock
        });

        // GARE11
        Operations.Add(new Operation {
            ApplicationDate = new DateTime(2024, 5, 3, 16, 24, 0),
            Type = AssetType.FII
        });

        // XPCA11
        Operations.Add(new Operation {
            ApplicationDate = new DateTime(2024, 5, 3, 16, 25, 0),
            Type = AssetType.FII
        });

        // BTCI11
        Operations.Add(new Operation {
            ApplicationDate = new DateTime(2024, 5, 3, 16, 26, 0),
            Type = AssetType.FII
        });

        #endregion

        IsEmpty = Operations.Count == 0;
    }
}