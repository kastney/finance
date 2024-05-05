namespace Finance.Pages.SelectWallet;

public partial class SelectWalletPage : ContentPage {
    private readonly SelectWalletViewModel viewModel;

    public SelectWalletPage() {
        InitializeComponent();

        BindingContext = viewModel = Service.Get<SelectWalletViewModel>();

        Application.Current.RequestedThemeChanged += Current_RequestedThemeChanged;
        Current_RequestedThemeChanged(null, null);
    }

    private void Current_RequestedThemeChanged(object sender, AppThemeChangedEventArgs e) {
        if(Application.Current.RequestedTheme == AppTheme.Light) {
            newItem.IconImageSource = new FontImageSource { Glyph = "\x2b", FontFamily = "IconsSolid", Size = 18, Color = StaticResourceUtility.Get<Color>("Gray900") };
        } else if(Application.Current.RequestedTheme == AppTheme.Dark) {
            newItem.IconImageSource = new FontImageSource { Glyph = "\x2b", FontFamily = "IconsSolid", Size = 18, Color = StaticResourceUtility.Get<Color>("Gray200") };
        }
    }

    protected override async void OnAppearing() {
        if(!viewModel.IsRunning) {
            viewModel.IsRunning = true;
            await Task.Delay(250);
            viewModel.Initialization();
            await Task.Delay(250);
            Shell.Current.FlyoutBehavior = FlyoutBehavior.Disabled;
            viewModel.IsRunning = false;
        }
    }

    protected override bool OnBackButtonPressed() {
        return viewModel.CanBack();
    }
}