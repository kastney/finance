using CommunityToolkit.Mvvm.ComponentModel;
using Finance.Enumerations;
using Finance.Models;
using Finance.Structures;
using System.Collections.ObjectModel;

namespace Finance.Pages.Historic;

internal partial class HistoricViewModel : ViewModel {

    [ObservableProperty]
    private bool isEmpty;

    [ObservableProperty]
    private bool isLoading;

    public ObservableCollection<OperationGroup> OperationsGroup { get; private set; }

    public HistoricViewModel() {
        OperationsGroup = [];
        IsLoading = true;
    }

    public async void Loading() {
        OperationsGroup.Clear();

        await Task.Delay(200);

        #region Loading

        OperationsGroup.Add(new OperationGroup(new DateTime(2024, 5, 17), [
            new Operation {
                Title = "SAPR4",
                Text = "SANEPAR PN N2",
                AssetType = AssetType.Stock,
                Detail = new DetailStruct(14.5f, 4),
                AppliedDate = new DateTime(2024, 5, 17, 15, 51, 0),
                IsBuy = true,
                Icon = @$"<HTML><BODY style=""margin: 0px""><img src=""https://s3-symbol-logo.tradingview.com/sanepar--big.svg"" width=""40"" height=""40""/></BODY></HTML>"
            }
          ]));

        await Task.Delay(200);

        OperationsGroup.Add(new OperationGroup(new DateTime(2023, 5, 3), [
            new Operation {
                Title = "ROXO34",
                Text = "NU HOLDINGS DRN",
                AssetType = AssetType.BDR,
                Detail = new DetailStruct(8.23f, 2),
                AppliedDate = new DateTime(2024, 5, 17, 15, 59, 0),
                IsBuy = true,
                Icon = @$"<HTML><BODY style=""margin: 0px""><img src=""https://s3-symbol-logo.tradingview.com/nu-holdings--big.svg"" width=""40"" height=""40""/></BODY></HTML>"
            },
            new Operation {
                Title = "GARE11",
                Text = "FII GUARDIANCI ER",
                AssetType = AssetType.FII,
                Detail = new DetailStruct(10.3f),
                AppliedDate = new DateTime(2024, 5, 17, 16, 12, 0),
                IsBuy = false
            }
        ]));

        await Task.Delay(200);

        OperationsGroup.Add(new OperationGroup(new DateTime(2023, 10, 3), [
            new Operation {
                Title = "(100% do CDI)",
                Text = "BANCO INTER S.A.",
                AssetType = AssetType.CDB,
                Detail = new DetailStruct(100, false),
                AppliedDate = new DateTime(2024, 5, 17, 16, 51, 0),
                IsBuy = false
            }
        ]));

        #endregion Loading

        IsEmpty = OperationsGroup.Count == 0;

        IsLoading = false;
    }
}