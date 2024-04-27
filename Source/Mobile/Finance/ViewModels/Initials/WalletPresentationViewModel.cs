using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Finance.ViewModels;

internal partial class WalletPresentationViewModel : ObservableObject {

    [ObservableProperty]
    private bool isRunning;

    [RelayCommand]
    private async Task NewWallet() {
        IsRunning = true;
        await Task.Delay(3000);
        await Shell.Current.GoToAsync("///dashboard");
        IsRunning = false;
    }
}