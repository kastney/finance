using CommunityToolkit.Mvvm.ComponentModel;
using Finance.Services;
using Finance.Services.Walleting;

namespace Finance;

internal partial class ViewModel : ObservableObject {

    #region Fields

    protected readonly IWalletService walletService;
    protected readonly INavigationService navigationService;

    #endregion Fields

    #region Properties

    [ObservableProperty]
    private bool isRunning;

    #endregion Properties

    #region Constructor

    public ViewModel() {
        walletService = Service.Get<IWalletService>();
        navigationService = Service.Get<INavigationService>();
    }

    #endregion Constructor

    #region Virtual Methods

    internal virtual bool CanBack() {
        return !IsRunning;
    }

    internal virtual async Task NavigationBack() {
        await navigationService.NavigateToBack();
        var page = Shell.Current.Navigation.NavigationStack.Count <= 1 ? Shell.Current.CurrentPage : Shell.Current.Navigation.NavigationStack[Shell.Current.Navigation.NavigationStack.Count - 1];
        if(page is not null && page.BindingContext is ViewModel viewModel) { viewModel.Update(); }
    }

    public virtual void Update() {
    }

    #endregion Virtual Methods
}