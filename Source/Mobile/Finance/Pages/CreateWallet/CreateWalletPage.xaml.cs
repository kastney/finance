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