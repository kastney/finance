using Finance.Models;
using Finance.Utilities;

namespace Finance.Pages;

internal partial class MainViewModel : ViewModel {

    [ObservableProperty]
    private Wallet wallet;


    internal void Initialization() => Update();

    public override void Update() {
        Wallet = walletService.Wallet;
        OnPropertyChanged(nameof(Wallet));
    }

    #region Navigation Methods

    [RelayCommand]
    private async Task SelectWallet() {
        if(!IsRunning) {
            IsRunning = true;
            await navigationService.NavigateTo("select");
            await Task.Delay(100);
            IsRunning = false;
        }
    }

    [RelayCommand]
    private async Task DangerZone() {
        if(!IsRunning) {
            IsRunning = true;
            await navigationService.NavigateTo("dangerZone");
            await Task.Delay(100);
            IsRunning = false;
        }
    }

    #endregion Navigation Methods
}