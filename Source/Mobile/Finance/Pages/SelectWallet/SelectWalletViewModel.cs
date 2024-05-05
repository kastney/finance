using CommunityToolkit.Mvvm.ComponentModel;

namespace Finance.Pages.SelectWallet;

internal partial class SelectWalletViewModel : ObservableObject {

    [ObservableProperty]
    private bool isRunning;

    internal bool CanBack() {
        return IsRunning;
    }
}

//[RelayCommand]
//private async Task NewWallet() {
//    IsRunning = true;
//
//    await Task.Delay(100);
//    await navigationService.NavigateToModal<CreateWalletPage>();
//    await Task.Delay(900);
//
//    IsRunning = false;
//}

//[RelayCommand]
//private async Task DeleteWallet() {
//    IsRunning = true;
//
//    if(await navigationService.NavigateToModal<DeleteWalletPage>() is DeleteWalletPage page) { ((DeleteWalletViewModel)page.BindingContext).Wallet = Wallet; }
//    await Task.Delay(1000);
//
//    IsRunning = false;
//}

//[RelayCommand]
//private async Task SelectWallet() {
//    IsRunning = true;
//
//    walletService.SetCurrent(SelectedWallet);
//    SelectedWallet = null;
//    await Task.Delay(500);
//    if(await navigationService.NavigateTo("///loading") is LoadingPage page) { page.Initialization(); }
//    await Task.Delay(500);
//
//    IsRunning = false;
//}

//internal void StartSelectWallet() {
//    Wallets.Clear();
//    foreach(var wallet in walletService.AvailableWallets()) {
//        Wallets.Add(wallet);
//    }
//    IsEmpty = Wallets.Count == 0;
//}