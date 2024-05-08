using CommunityToolkit.Mvvm.ComponentModel;
using Finance.Services;

namespace Finance;

internal partial class ViewModel : ObservableObject {
    protected readonly IWalletService walletService;
    protected readonly INavigationService navigationService;

    [ObservableProperty]
    private bool isRunning;

    public ViewModel() {
        walletService = Service.Get<IWalletService>();
        navigationService = Service.Get<INavigationService>();
    }

    internal virtual bool CanBack() {
        return !IsRunning;
    }

    internal virtual async Task NavigationBack() {
        await navigationService.NavigateToBack();
    }
}