using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Finance.Models;
using Finance.Pages;

namespace Finance.ViewModels;

internal partial class DeleteWalletViewModel : ViewModel {

    [ObservableProperty]
    private bool isRunningInverse;

    public Wallet Wallet { get; set; }

    public DeleteWalletViewModel() {
        IsRunningInverse = true;
        Wallet = walletService.Wallet;
    }

    [RelayCommand]
    private async Task Delete() {
        IsRunning = true;
        IsRunningInverse = false;

        // Deletar carteira
        await walletService.Delete(Wallet);

        // Ir para o dashboard, passando pelo loading
        await Task.Delay(250);
        if(await navigationService.NavigateTo("///loading", false) is LoadingPage page) { page.Initialization(); }

        IsRunning = false;
        IsRunningInverse = true;
    }
}