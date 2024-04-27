using Finance.ViewModels;

namespace Finance.Pages;

public partial class LoadingPage : ContentPage {
    private readonly LoadingViewModel viewModel;

    public LoadingPage() {
        InitializeComponent();
        BindingContext = viewModel = Service.Get<LoadingViewModel>();
        LoadingViewModel.Initialization();
    }

    protected override bool OnBackButtonPressed() {
        return true;
    }
}