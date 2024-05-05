namespace Finance.Pages.SelectWallet;

public partial class SelectWalletPage : ContentPage {
    private readonly SelectWalletViewModel viewModel;

    public SelectWalletPage() {
        InitializeComponent();
        BindingContext = viewModel = Service.Get<SelectWalletViewModel>();
    }

    protected override async void OnAppearing() {
        viewModel.IsRunning = true;
        await Task.Delay(500);
        Shell.Current.FlyoutBehavior = FlyoutBehavior.Disabled;
        viewModel.IsRunning = false;
    }

    protected override bool OnBackButtonPressed() {
        return viewModel.CanBack();
    }
}