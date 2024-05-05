using Finance.ViewModels;

namespace Finance.Pages;

public partial class DeleteWalletPage : ContentPage {
    private readonly DeleteWalletViewModel viewModel;

    public DeleteWalletPage() {
        InitializeComponent();
        BindingContext = viewModel = Service.Get<DeleteWalletViewModel>();
    }

    protected override bool OnBackButtonPressed() {
        return viewModel.IsProcessing();
    }

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e) {
        checkBox.IsChecked = !checkBox.IsChecked;
    }
}