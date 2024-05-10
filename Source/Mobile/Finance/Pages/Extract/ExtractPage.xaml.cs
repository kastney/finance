namespace Finance.Pages.Extract;

public partial class ExtractPage : ContentPage {
    private readonly ExtractViewModel viewModel;

    public ExtractPage() {
        InitializeComponent();
        BindingContext = viewModel = new ExtractViewModel();
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