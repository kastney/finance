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

        #region

        // CDB
        Operations.Add(new Operation {
            Date = new DateTime(2023, 10, 3, 10, 0, 0)
        });

        // BTCI11
        Operations.Add(new Operation {
            Date = new DateTime(2024, 4, 12, 14, 0, 0)
        });

        // VGHF11
        Operations.Add(new Operation {
            Date = new DateTime(2024, 4, 12, 14, 5, 0)
        });

        // XPCA11
        Operations.Add(new Operation {
            Date = new DateTime(2024, 4, 12, 14, 6, 0)
        });

        // VINO11
        Operations.Add(new Operation {
            Date = new DateTime(2024, 4, 12, 14, 16, 0)
        });

        // GARE11
        Operations.Add(new Operation {
            Date = new DateTime(2024, 4, 12, 14, 24, 0)
        });

        // BBAS3F
        Operations.Add(new Operation {
            Date = new DateTime(2024, 5, 3, 16, 20, 0)
        });

        // ITSA3F
        Operations.Add(new Operation {
            Date = new DateTime(2024, 5, 3, 16, 21, 0)
        });

        // GARE11
        Operations.Add(new Operation {
            Date = new DateTime(2024, 5, 3, 16, 24, 0)
        });

        // XPCA11
        Operations.Add(new Operation {
            Date = new DateTime(2024, 5, 3, 16, 25, 0)
        });

        // BTCI11
        Operations.Add(new Operation {
            Date = new DateTime(2024, 5, 3, 16, 26, 0)
        });

        #endregion

        IsEmpty = Operations.Count == 0;
    }
}