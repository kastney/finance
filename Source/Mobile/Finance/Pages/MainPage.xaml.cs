namespace Finance.Pages;

public partial class MainPage : ContentPage {
    private readonly MainViewModel viewModel;

    public MainPage() {
        InitializeComponent();
        BindingContext = viewModel = Service.Get<MainViewModel>();
        Application.Current.RequestedThemeChanged += Current_RequestedThemeChanged;
        Current_RequestedThemeChanged(null, null);
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

    private void Current_RequestedThemeChanged(object sender, AppThemeChangedEventArgs e) {
        if(Application.Current.RequestedTheme == AppTheme.Light) {
            chartStyle.BackgroundColor = StaticResourceUtility.Get<Color>("Gray100");
            legendStyle.Color = labelStyle.Color = StaticResourceUtility.Get<Color>("Black");
        } else {
            chartStyle.BackgroundColor = StaticResourceUtility.Get<Color>("Gray600");
            legendStyle.Color = labelStyle.Color = StaticResourceUtility.Get<Color>("White");
        }
    }
}