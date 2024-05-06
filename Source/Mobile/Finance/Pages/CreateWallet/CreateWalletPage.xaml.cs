namespace Finance.Pages.CreateWallet;

public partial class CreateWalletPage : ContentPage {
    private readonly CreateWalletViewModel viewModel;

    public CreateWalletPage() {
        InitializeComponent();
        BindingContext = viewModel = Service.Get<CreateWalletViewModel>();
    }

    protected override async void OnAppearing() {
        if(!viewModel.IsRunning) {
            viewModel.IsRunning = true;
            await Task.Delay(500);
            entry.Focus();
            Shell.Current.FlyoutBehavior = FlyoutBehavior.Disabled;
            viewModel.IsRunning = false;
        }
    }

    protected override bool OnBackButtonPressed() {
        return viewModel.CanBack();
    }
}