using CommunityToolkit.Mvvm.ComponentModel;

namespace Finance.Pages.Historic;

internal partial class HistoricViewModel : ViewModel {

    [ObservableProperty]
    private DateTime minDate;

    [ObservableProperty]
    private DateTime maxDate;

    [ObservableProperty]
    private DateTime displayDate;

    [ObservableProperty]
    private DateTime selectedDate;

    public HistoricViewModel() {
        MinDate = walletService.MinData();
        MaxDate = walletService.MaxData();
        DisplayDate = MaxDate;
        SelectedDate = MaxDate;
    }
}