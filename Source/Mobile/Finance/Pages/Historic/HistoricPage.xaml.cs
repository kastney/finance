namespace Finance.Pages.Historic;

public partial class HistoricPage : ContentPage {
    private readonly HistoricViewModel viewModel;

    public HistoricPage() {
        InitializeComponent();
        BindingContext = viewModel = new HistoricViewModel();
    }

    protected override async void OnAppearing() {
        if(!viewModel.IsRunning) {
            viewModel.IsRunning = true;
            await Task.Delay(100);
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