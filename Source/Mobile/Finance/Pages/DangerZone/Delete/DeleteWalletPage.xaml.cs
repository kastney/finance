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
            await Task.Delay(100);
            viewModel.IsRunningInverse = true;
            viewModel.IsRunning = false;
        }
    }

    protected override bool OnBackButtonPressed() {
        BackButtom_Clicked(null, null);
        return true;
    }

    private async void BackButtom_Clicked(object sender, EventArgs e) {
        if(viewModel.CanBack()) {
            await viewModel.NavigationBack();
        }
    }

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e) {
        checkBox.IsChecked = !checkBox.IsChecked;
    }
}