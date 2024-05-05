using Finance.ViewModels;

namespace Finance.Pages;

public partial class CreateWalletPage : ContentPage {
    private readonly CreateWalletViewModel viewModel;

    public CreateWalletPage() {
        InitializeComponent();
        BindingContext = viewModel = Service.Get<CreateWalletViewModel>();
    }

    protected override async void OnAppearing() {
        viewModel.IsRunning = true;
        await Task.Delay(100);
        entry.Focus();
        await Task.Delay(400);
        viewModel.IsRunning = false;
    }

    protected override bool OnBackButtonPressed() {
        return viewModel.CanBack();
    }

    private void Entry_Completed(object sender, EventArgs e) {
        if(viewModel.CreateCommand.CanExecute(null)) {
            viewModel.CreateCommand.Execute(null);
        }
    }
}