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
    private bool hasBrazilStocks;

    [ObservableProperty]
    private bool hasFIIs;

    [ObservableProperty]
    private bool hasAssetTitle;

    [ObservableProperty]
    private bool hasAsset;

    public MainViewModel() {
        Palette = [];
    }

    internal void Initialization() => Update();

    public override void Update() {
        Wallet = walletService.Wallet;
        OnPropertyChanged(nameof(Wallet));
        // Set graphics
        //Palette = Wallet.GetPalette();
        //WalletPosition = Wallet.GetWalletPosition();
        // Set has properties
        //HasBrazilStocks = Wallet.HasBrazilStock();
        //HasFIIs = Wallet.HasFIIs();
        //HasAssetTitle = HasBrazilStocks || HasFIIs;
        //HasAsset = Wallet.HasAsset();
    }

    #region Navigation Methods

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
        if(!IsRunning) {
            IsRunning = true;
            await navigationService.NavigateTo("strategy");
            await Task.Delay(500);
            IsRunning = false;
        }
    }

    [RelayCommand]
    private async Task Historic() {
        if(!IsRunning) {
            IsRunning = true;
            await navigationService.NavigateTo("historic");
            await Task.Delay(500);
            IsRunning = false;
        }
    }

    [RelayCommand]
    private async Task DangerZone() {
        if(!IsRunning) {
            IsRunning = true;
            await navigationService.NavigateTo("dangerZone");
            await Task.Delay(500);
            IsRunning = false;
        }
    }

    #endregion Navigation Methods
}