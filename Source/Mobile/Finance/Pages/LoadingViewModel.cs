using Finance.Services;

namespace Finance.Pages;

internal class LoadingViewModel {
    private readonly IWalletService walletService;
    private readonly INavigationService navigationService;

    public LoadingViewModel() {
        walletService = Service.Get<IWalletService>();
        navigationService = Service.Get<INavigationService>();
        Initialization();
    }

    internal async void Initialization() {
        if(await walletService.Exists()) {
            if(await navigationService.NavigateTo("///main") is MainPage page) { page.Initialization(); }
        } else {
            await navigationService.NavigateTo("///presentation");
        }
    }
}