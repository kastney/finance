using Finance.Models;
using Finance.Pages;
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
        if(walletService.GetCurrent() is Wallet wallet) {
            if(await navigationService.NavigateTo("///main", wallet) is MainPage page) { page.Initialization(); }
        } else {
            await navigationService.NavigateTo("///presentation");
        }
    }
}