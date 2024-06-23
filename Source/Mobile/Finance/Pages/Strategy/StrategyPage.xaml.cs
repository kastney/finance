namespace Finance.Pages.Strategy;

public partial class StrategyPage : ContentPage {
    private readonly StrategyViewModel viewModel;

    public StrategyPage() {
        InitializeComponent();
        BindingContext = viewModel = Service.Get<StrategyViewModel>();
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