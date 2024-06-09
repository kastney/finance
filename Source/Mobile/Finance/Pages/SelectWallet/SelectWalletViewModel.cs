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
        Loading();
    }

    private async void Loading() {
        foreach(var wallet in await walletService.AvailableWallets()) {
            Wallets.Add(wallet);
        }
        IsEmpty = Wallets.Count == 0;
    }

    [RelayCommand]
    private async Task NewWallet() {
        if(!IsRunning) {
            IsRunning = true;

            await navigationService.NavigateTo("create");
            await Task.Delay(500);

            IsRunning = false;
        }
    }

    [RelayCommand]
    private async Task SelectWallet() {
        IsRunning = true;

        walletService.SetWallet(SelectedWallet);
        SelectedWallet = null;

        await Task.Delay(250);
        if(await navigationService.NavigateTo("///loading", false) is LoadingPage page) { page.Initialization(); }

        IsRunning = false;
    }
}