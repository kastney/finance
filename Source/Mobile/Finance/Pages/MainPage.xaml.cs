namespace Finance.Pages;

public partial class MainPage : ContentPage {

    public MainPage() {
        InitializeComponent();
        Application.Current.RequestedThemeChanged += Current_RequestedThemeChanged;
        Current_RequestedThemeChanged(null, null);
    }

    private void Current_RequestedThemeChanged(object sender, AppThemeChangedEventArgs e) {
        if(Application.Current.RequestedTheme == AppTheme.Light) {
            walletItem.IconImageSource = new FontImageSource { Glyph = "\xf555", FontFamily = "IconsSolid", Size = 18, Color = StaticResourceUtility.Get<Color>("Gray900") };
        } else if(Application.Current.RequestedTheme == AppTheme.Dark) {
            walletItem.IconImageSource = new FontImageSource { Glyph = "\xf555", FontFamily = "IconsSolid", Size = 18, Color = StaticResourceUtility.Get<Color>("Gray200") };
        }
    }
}