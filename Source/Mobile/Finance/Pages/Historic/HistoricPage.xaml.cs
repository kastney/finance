using DevExpress.Maui.Editors;

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

    private void DXCalendar_DisableDate(object sender, DisableDateEventArgs e) {
        if(e.Date.Year == 2024 && e.Date.Month == 5 && e.Date.Day == 15) {
            e.IsDisabled = true;
        }
    }
}