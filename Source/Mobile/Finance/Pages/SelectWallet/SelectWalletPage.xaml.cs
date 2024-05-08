namespace Finance.Pages.SelectWallet;

public partial class SelectWalletPage : ContentPage {
    private readonly SelectWalletViewModel viewModel;

    public SelectWalletPage() {
        InitializeComponent();
        BindingContext = viewModel = Service.Get<SelectWalletViewModel>();
    }

    protected override async void OnAppearing() {
        if(!viewModel.IsRunning) {
            viewModel.IsRunning = true;
            await Task.Delay(500);
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
}