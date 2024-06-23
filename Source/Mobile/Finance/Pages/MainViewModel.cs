using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Finance.Models;

namespace Finance.Pages;

internal partial class MainViewModel : ViewModel {

    [ObservableProperty]
    private Wallet wallet;

    [ObservableProperty]
    private Color[] palette;

    [ObservableProperty]
    private List<PieData> walletPosition;

    [ObservableProperty]
    private bool hasAsset;

    public MainViewModel() {
        Palette = [];
    }

    internal void Initialization() => BackFinish();

    public override void BackFinish() {
        Wallet = walletService.Wallet;
        // ...
        Palette = Wallet.GetPalette();
        WalletPosition = Wallet.GetWalletPosition();
        HasAsset = Wallet.FIIsEnabled;
    }

    [RelayCommand]
    private async Task SelectWallet() {
        if(!IsRunning) {
            IsRunning = true;
            await navigationService.NavigateTo("select");
            await Task.Delay(500);
            IsRunning = false;
        }
    }

    [RelayCommand]
    private async Task Strategy() {
        IsRunning = true;
        await navigationService.NavigateTo("strategy");
        await Task.Delay(500);
        IsRunning = false;
    }

    [RelayCommand]
    private async Task Historic() {
        IsRunning = true;
        await navigationService.NavigateTo("historic");
        await Task.Delay(500);
        IsRunning = false;
    }

    [RelayCommand]
    private async Task DangerZone() {
        IsRunning = true;
        await navigationService.NavigateTo("dangerZone");
        await Task.Delay(500);
        IsRunning = false;
    }
}