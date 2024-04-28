using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Finance.Pages;
using Finance.Services;

namespace Finance.ViewModels;

internal partial class WalletPresentationViewModel : ObservableObject {
    private readonly INavigationService navigationService;

    [ObservableProperty]
    private bool isRunning;

    public WalletPresentationViewModel() {
        navigationService = Service.Get<INavigationService>();
    }

    [RelayCommand]
    private async Task NewWallet() {
        IsRunning = true;

        await navigationService.NavigateToModal<CreateWalletPage>();
        await Task.Delay(1000);

        IsRunning = false;
    }
}