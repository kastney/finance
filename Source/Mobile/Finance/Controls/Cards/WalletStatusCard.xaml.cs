namespace Finance.Controls.Cards;

public partial class WalletStatusCard : ContentView {
    public static readonly BindableProperty PriceProperty = BindableProperty.Create(nameof(Price), typeof(float?), typeof(WalletStatusCard), null, propertyChanged: OnPriceChanged);
    public static readonly BindableProperty ProfitabilityProperty = BindableProperty.Create(nameof(Profitability), typeof(float?), typeof(WalletStatusCard), null, propertyChanged: OnProfitabilityChanged);
    public static readonly BindableProperty VariationProperty = BindableProperty.Create(nameof(Variation), typeof(float?), typeof(WalletStatusCard), null, propertyChanged: OnVariationChanged);
    public static readonly BindableProperty PerformanceProperty = BindableProperty.Create(nameof(Performance), typeof(float?), typeof(WalletStatusCard), null, propertyChanged: OnPerformanceChanged);

    public float? Price {
        get { return (float?)GetValue(PriceProperty); }
        set { SetValue(PriceProperty, value); }
    }

    public float? Profitability {
        get { return (float?)GetValue(ProfitabilityProperty); }
        set { SetValue(ProfitabilityProperty, value); }
    }

    public float? Variation {
        get { return (float?)GetValue(VariationProperty); }
        set { SetValue(VariationProperty, value); }
    }

    public float? Performance {
        get { return (float?)GetValue(PerformanceProperty); }
        set { SetValue(PerformanceProperty, value); }
    }

    public WalletStatusCard() {
        InitializeComponent();
        Application.Current.RequestedThemeChanged += Current_RequestedThemeChanged;
    }

    private static void OnPriceChanged(BindableObject bindable, object oldValue, object newValue) {
        if(bindable is not WalletStatusCard control) { return; }
        if(newValue is not float price) { return; }
        control.price.Text = price.ToString("C", new CultureInfo("pt-BR"));
    }

    private static void OnProfitabilityChanged(BindableObject bindable, object oldValue, object newValue) {
        if(bindable is not WalletStatusCard control) { return; }
        if(newValue is not float profitability) { return; }
        control.profitability.Text = (profitability / 100).ToString("P2");
        if(profitability > 0) {
            control.profitability.TextColor = Application.Current.RequestedTheme == AppTheme.Light ? StaticResourceUtility.Get<Color>("Green") : StaticResourceUtility.Get<Color>("GreenDark");
        } else if(profitability < 0) {
            control.profitability.TextColor = Application.Current.RequestedTheme == AppTheme.Light ? StaticResourceUtility.Get<Color>("Red") : StaticResourceUtility.Get<Color>("RedDark");
        } else {
            control.profitability.TextColor = Application.Current.RequestedTheme == AppTheme.Light ? StaticResourceUtility.Get<Color>("Gray600") : StaticResourceUtility.Get<Color>("Gray300");
        }
    }

    private static void OnVariationChanged(BindableObject bindable, object oldValue, object newValue) {
        if(bindable is not WalletStatusCard control) { return; }
        if(newValue is not float variation) { return; }
        control.variation.Text = (variation / 100).ToString("P2");
        if(variation > 0) {
            control.variation.TextColor = Application.Current.RequestedTheme == AppTheme.Light ? StaticResourceUtility.Get<Color>("Green") : StaticResourceUtility.Get<Color>("GreenDark");
        } else if(variation < 0) {
            control.variation.TextColor = Application.Current.RequestedTheme == AppTheme.Light ? StaticResourceUtility.Get<Color>("Red") : StaticResourceUtility.Get<Color>("RedDark");
        } else {
            control.variation.TextColor = Application.Current.RequestedTheme == AppTheme.Light ? StaticResourceUtility.Get<Color>("Gray600") : StaticResourceUtility.Get<Color>("Gray300");
        }
    }

    private static void OnPerformanceChanged(BindableObject bindable, object oldValue, object newValue) {
        if(bindable is not WalletStatusCard control) { return; }
        if(newValue is not float performance) { return; }
        control.performance.Text = (performance / 100).ToString("P2");
        if(performance > 0) {
            control.performance.TextColor = Application.Current.RequestedTheme == AppTheme.Light ? StaticResourceUtility.Get<Color>("Green") : StaticResourceUtility.Get<Color>("GreenDark");
        } else if(performance < 0) {
            control.performance.TextColor = Application.Current.RequestedTheme == AppTheme.Light ? StaticResourceUtility.Get<Color>("Red") : StaticResourceUtility.Get<Color>("RedDark");
        } else {
            control.performance.TextColor = Application.Current.RequestedTheme == AppTheme.Light ? StaticResourceUtility.Get<Color>("Gray600") : StaticResourceUtility.Get<Color>("Gray300");
        }
    }

    private void Current_RequestedThemeChanged(object sender, AppThemeChangedEventArgs e) {
        if(Profitability > 0) {
            profitability.TextColor = Application.Current.RequestedTheme == AppTheme.Light ? StaticResourceUtility.Get<Color>("Green") : StaticResourceUtility.Get<Color>("GreenDark");
        } else if(Profitability < 0) {
            profitability.TextColor = Application.Current.RequestedTheme == AppTheme.Light ? StaticResourceUtility.Get<Color>("Red") : StaticResourceUtility.Get<Color>("RedDark");
        } else {
            profitability.TextColor = Application.Current.RequestedTheme == AppTheme.Light ? StaticResourceUtility.Get<Color>("Gray600") : StaticResourceUtility.Get<Color>("Gray300");
        }

        if(Variation > 0) {
            variation.TextColor = Application.Current.RequestedTheme == AppTheme.Light ? StaticResourceUtility.Get<Color>("Green") : StaticResourceUtility.Get<Color>("GreenDark");
        } else if(Variation < 0) {
            variation.TextColor = Application.Current.RequestedTheme == AppTheme.Light ? StaticResourceUtility.Get<Color>("Red") : StaticResourceUtility.Get<Color>("RedDark");
        } else {
            variation.TextColor = Application.Current.RequestedTheme == AppTheme.Light ? StaticResourceUtility.Get<Color>("Gray600") : StaticResourceUtility.Get<Color>("Gray300");
        }

        if(Performance > 0) {
            performance.TextColor = Application.Current.RequestedTheme == AppTheme.Light ? StaticResourceUtility.Get<Color>("Green") : StaticResourceUtility.Get<Color>("GreenDark");
        } else if(Performance < 0) {
            performance.TextColor = Application.Current.RequestedTheme == AppTheme.Light ? StaticResourceUtility.Get<Color>("Gray600") : StaticResourceUtility.Get<Color>("RedDark");
        } else {
            performance.TextColor = Application.Current.RequestedTheme == AppTheme.Light ? StaticResourceUtility.Get<Color>("Gray600") : StaticResourceUtility.Get<Color>("Gray300");
        }
    }
}