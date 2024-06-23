using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Finance.Pages.Strategy;

internal partial class StrategyViewModel : ViewModel {

    [ObservableProperty]
    private bool isFIIsRunning;

    [RelayCommand]
    private async Task FIIsEnabled() {
        if(!IsRunning && !IsFIIsRunning) {
            IsRunning = true;
            IsFIIsRunning = true;

            if(await walletService.SetFIIsEnabled(!walletService.Wallet.FIIsEnabled)) {
                // Mudou
            }

            IsFIIsRunning = false;
            IsRunning = false;
        }
    }
}