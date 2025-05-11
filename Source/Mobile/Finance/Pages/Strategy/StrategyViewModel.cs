using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Finance.Pages.Strategy;

internal partial class StrategyViewModel : ViewModel {

    [ObservableProperty]
    private bool isBrazilStocksRunning;

    [ObservableProperty]
    private bool isFIIsRunning;

    [RelayCommand]
    private async Task BrazilStocksEnabled() {
        if(!IsRunning && !IsBrazilStocksRunning) {
            IsRunning = true;
            IsBrazilStocksRunning = true;

            //if(await walletService.SetBrazilStocksEnabled(!walletService.Wallet.BrazilStocksEnabled)) {
            // Mudou
            //}

            IsBrazilStocksRunning = false;
            IsRunning = false;
        }
    }

    [RelayCommand]
    private async Task FIIsEnabled() {
        if(!IsRunning && !IsFIIsRunning) {
            IsRunning = true;
            IsFIIsRunning = true;

            //if(await walletService.SetFIIsEnabled(!walletService.Wallet.FIIsEnabled)) {
            // Mudou
            //}

            IsFIIsRunning = false;
            IsRunning = false;
        }
    }
}