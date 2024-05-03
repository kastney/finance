using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Finance.Models;
using Finance.Pages;
using Finance.Services;

namespace Finance.ViewModels;

[QueryProperty(nameof(Wallet), "Entity")]
internal partial class MainViewModel : ObservableObject {
    private readonly INavigationService navigationService;

    [ObservableProperty]
    private Wallet wallet;

    [ObservableProperty]
    private bool isRunning;

    public MainViewModel() {
        navigationService = Service.Get<INavigationService>();
    }

    [RelayCommand]
    private async Task DeleteWallet() {
        IsRunning = true;

        if(await navigationService.NavigateToModal<DeleteWalletPage>() is DeleteWalletPage page) { ((DeleteWalletViewModel)page.BindingContext).Wallet = Wallet; }
        await Task.Delay(1000);

        IsRunning = false;
    }

    [RelayCommand]
    private async Task NewWallet() {
        IsRunning = true;

        await Task.Delay(100);
        await navigationService.NavigateToModal<CreateWalletPage>();
        await Task.Delay(900);

        IsRunning = false;
    }
}