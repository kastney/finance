using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Finance.Models;
using Finance.Services;

namespace Finance.Pages;

internal partial class MainViewModel : ObservableObject {
    private readonly IWalletService walletService;
    private readonly INavigationService navigationService;

    [ObservableProperty]
    private Wallet wallet;

    [ObservableProperty]
    private bool isRunning;

    public MainViewModel() {
        walletService = Service.Get<IWalletService>();
        navigationService = Service.Get<INavigationService>();

        Wallet = walletService.Wallet;
        IsRunning = true;
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
    private async Task DangerZone() {
        IsRunning = true;

        await Shell.Current.Navigation.PushAsync(new ContentPage { Title = "Zona de Perigo" });
        await Task.Delay(500);

        IsRunning = false;
    }

    internal bool CanBack() {
        return IsRunning;
    }
}