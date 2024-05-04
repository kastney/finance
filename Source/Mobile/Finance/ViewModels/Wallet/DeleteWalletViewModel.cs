using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Finance.Models;
using Finance.Pages;
using Finance.Services;

namespace Finance.ViewModels;

internal partial class DeleteWalletViewModel : ObservableObject {
    private readonly IWalletService walletService;
    private readonly INavigationService navigationService;

    [ObservableProperty]
    private bool isRunning;

    [ObservableProperty]
    private bool isRunningInverse;

    public Wallet Wallet { get; set; }

    public DeleteWalletViewModel() {
        walletService = Service.Get<IWalletService>();
        navigationService = Service.Get<INavigationService>();
        IsRunningInverse = true;
    }

    [RelayCommand]
    private async Task Delete() {
        IsRunning = true;
        IsRunningInverse = false;

        // Deletar carteira
        walletService.Delete(Wallet);

        // Ir para o dashboard, passando pelo loading
        await Task.Delay(500);
        if(await navigationService.NavigateTo("///loading") is LoadingPage page) { page.Initialization(); }
        //await navigationService.NavigateToBackModal();

        IsRunning = false;
        IsRunningInverse = true;
    }

    public bool IsProcessing() {
        return IsRunning;
    }
}