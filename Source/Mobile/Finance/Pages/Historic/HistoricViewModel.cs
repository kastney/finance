using CommunityToolkit.Mvvm.ComponentModel;
using Finance.Services;

namespace Finance.Pages.Historic;

internal partial class HistoricViewModel : ViewModel {
    private readonly IWalletService walletService;

    [ObservableProperty]
    private DateTime minDate;

    [ObservableProperty]
    private DateTime maxDate;

    [ObservableProperty]
    private DateTime displayDate;

    [ObservableProperty]
    private DateTime selectedDate;

    public HistoricViewModel() {
        walletService = Service.Get<IWalletService>();
        var date = DateTime.Now;
        MinDate = date;
        MaxDate = date;
        DisplayDate = date;
        SelectedDate = date;
    }
}