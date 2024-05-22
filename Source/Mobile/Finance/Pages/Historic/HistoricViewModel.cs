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

        MinDate = new DateTime(2024, 3, 19);

        MaxDate = new DateTime(2024, 4, 20);
        SelectedDate = new DateTime(2024, 4, 20);
        DisplayDate = new DateTime(2024, 4, 20);
    }
}