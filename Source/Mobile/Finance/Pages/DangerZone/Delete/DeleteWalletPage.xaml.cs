using Finance.ViewModels;

namespace Finance.Pages.DangerZone.Delete;

public partial class DeleteWalletPage : ContentPage {
    private readonly DeleteWalletViewModel viewModel;

    public DeleteWalletPage() {
        InitializeComponent();
        BindingContext = viewModel = Service.Get<DeleteWalletViewModel>();
    }

    protected override async void OnAppearing() {
        if(!viewModel.IsRunning) {
            viewModel.IsRunning = true;
            viewModel.IsRunningInverse = false;
            await Task.Delay(500);
            Shell.Current.FlyoutBehavior = FlyoutBehavior.Disabled;
            viewModel.IsRunningInverse = true;
            viewModel.IsRunning = false;
        }
    }

    protected override bool OnBackButtonPressed() {
        return viewModel.CanBack();
    }

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e) {
        checkBox.IsChecked = !checkBox.IsChecked;
    }
}