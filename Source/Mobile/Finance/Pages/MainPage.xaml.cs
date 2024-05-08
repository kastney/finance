namespace Finance.Pages;

public partial class MainPage : ContentPage {
    private readonly MainViewModel viewModel;

    public MainPage() {
        InitializeComponent();
        BindingContext = viewModel = Service.Get<MainViewModel>();
    }

    internal void Initialization() {
        viewModel.Initialization();
    }

    protected override async void OnAppearing() {
        if(!viewModel.IsRunning) {
            viewModel.IsRunning = true;
            await Task.Delay(500);
            viewModel.IsRunning = false;
        }
    }

    protected override bool OnBackButtonPressed() {
        return !viewModel.CanBack();
    }
}