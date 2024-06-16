using CommunityToolkit.Mvvm.Input;

namespace Finance.Pages.Historic;

internal partial class HistoricViewModel : ViewModel {

    public HistoricViewModel() {
        //var a = walletService.GetHistoric();

        //walletService.AddOperation(new CDBOperation {
        //    Issuer = "PICPAY BANK",
        //    FixedType = FixedType.Postfixed,
        //    Price = 1400,
        //    AppliedDate = new DateTime(2024, 5, 14, 13, 34, 0),
        //    DueDate = new DateTime(2027, 5, 14),
        //    IndexerType = IndexerType.CDI,
        //    Rate = 102,
        //    IsBuy = true
        //});

        //var b = walletService.GetHistoric();
    }

    [RelayCommand]
    private async Task NewOperation() {
        if(!IsRunning) {
            IsRunning = true;

            await Task.Delay(500);

            IsRunning = false;
        }
    }
}