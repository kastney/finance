using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Finance.Models;
using System.Collections.ObjectModel;

namespace Finance.Pages.SelectWallet;

internal partial class SelectWalletViewModel : ViewModel {

    [ObservableProperty]
    private Wallet selectedWallet;

    [ObservableProperty]
    private bool isEmpty;

    public ObservableCollection<Wallet> Wallets { get; set; }

    public SelectWalletViewModel() {
        Wallets = [];
    }

    internal void Initialization() {
        Wallets.Clear();
        foreach(var wallet in walletService.AvailableWallets()) {
            Wallets.Add(wallet);
        }
        IsEmpty = Wallets.Count == 0;
    }

    [RelayCommand]
    private async Task NewWallet() {
        if(!IsRunning) {
            IsRunning = true;

            await Task.Delay(5000);

            IsRunning = false;
        }
    }

    [RelayCommand]
    private async Task SelectWallet() {
        IsRunning = true;

        walletService.SetWallet(SelectedWallet);
        SelectedWallet = null;
        await Task.Delay(500);
        if(await navigationService.NavigateTo("///loading") is LoadingPage page) { page.Initialization(); }
        await Task.Delay(500);

        IsRunning = false;
    }
}

//[RelayCommand]
//private async Task DeleteWallet() {
//    IsRunning = true;
//
//    if(await navigationService.NavigateToModal<DeleteWalletPage>() is DeleteWalletPage page) { ((DeleteWalletViewModel)page.BindingContext).Wallet = Wallet; }
//    await Task.Delay(1000);
//
//    IsRunning = false;
//}