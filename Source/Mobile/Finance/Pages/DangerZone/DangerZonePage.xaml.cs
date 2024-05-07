namespace Finance.Pages.DangerZone;

public partial class DangerZonePage : ContentPage {
    private readonly DangerZoneViewModel viewModel;

    public DangerZonePage() {
        InitializeComponent();
        BindingContext = viewModel = Service.Get<DangerZoneViewModel>();
    }

    protected override async void OnAppearing() {
        if(!viewModel.IsRunning) {
            viewModel.IsRunning = true;
            await Task.Delay(500);
            Shell.Current.FlyoutBehavior = FlyoutBehavior.Disabled;
            viewModel.IsRunning = false;
        }
    }

    protected override bool OnBackButtonPressed() {
        return viewModel.CanBack();
    }
}