using CommunityToolkit.Mvvm.ComponentModel;
using Finance.Services;

namespace Finance;

internal partial class ViewModel : ObservableObject {
    protected readonly IWalletService walletService;
    protected readonly INavigationService navigationService;

    [ObservableProperty]
    private bool isRunning;

    public ViewModel() {
        walletService = Service.Get<IWalletService>();
        navigationService = Service.Get<INavigationService>();
    }

    internal virtual bool CanBack() {
        return !IsRunning;
    }

    internal virtual async Task NavigationBack() {
        await navigationService.NavigateToBack();
        var page = Shell.Current.Navigation.NavigationStack.Count <= 1 ? Shell.Current.CurrentPage : Shell.Current.Navigation.NavigationStack[Shell.Current.Navigation.NavigationStack.Count - 1];
        if(page is not null && page.BindingContext is ViewModel viewModel) { viewModel.BackFinish(); }
    }

    public virtual void BackFinish() {
    }
}