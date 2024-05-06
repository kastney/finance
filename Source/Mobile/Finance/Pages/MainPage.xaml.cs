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
            Shell.Current.FlyoutBehavior = FlyoutBehavior.Flyout;
            await Task.Delay(500);
            viewModel.IsRunning = false;
        }
    }

    protected override bool OnBackButtonPressed() {
        return viewModel.CanBack();
    }

    private void Current_RequestedThemeChanged(object sender, AppThemeChangedEventArgs e) {
        if(Application.Current.RequestedTheme == AppTheme.Light) {
            walletItem.IconImageSource = new FontImageSource { Glyph = "\xf555", FontFamily = "IconsSolid", Size = 18, Color = StaticResourceUtility.Get<Color>("Gray900") };
        } else if(Application.Current.RequestedTheme == AppTheme.Dark) {
            walletItem.IconImageSource = new FontImageSource { Glyph = "\xf555", FontFamily = "IconsSolid", Size = 18, Color = StaticResourceUtility.Get<Color>("Gray200") };
        }
    }
}