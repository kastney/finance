using Finance.Services;

namespace Finance.ViewModels;

internal class LoadingViewModel {
    private readonly IWalletService walletService;
    private readonly INavigationService navigationService;

    public LoadingViewModel() {
        walletService = Service.Get<IWalletService>();
        navigationService = Service.Get<INavigationService>();
    }

    internal async void Initialization() {
        if(walletService.Exists()) {
            await navigationService.NavigateTo("///main");
        } else {
            await navigationService.NavigateTo("///presentation");
        }
    }
}