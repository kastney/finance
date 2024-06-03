using System.Globalization;

namespace Finance.Controls;

public partial class AssetCard : ContentView {
    public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(AssetCard), string.Empty, propertyChanged: OnTitleChanged);
    public static readonly BindableProperty IconProperty = BindableProperty.Create(nameof(Icon), typeof(string), typeof(AssetCard), string.Empty, propertyChanged: OnIconChanged);
    public static readonly BindableProperty TitleColorProperty = BindableProperty.Create(nameof(TitleColor), typeof(Color), typeof(AssetCard), default, propertyChanged: OnTitleColorChanged);
    public static readonly BindableProperty AssetCountProperty = BindableProperty.Create(nameof(AssetCount), typeof(int), typeof(AssetCard), 0, propertyChanged: OnAssetCountChanged);
    public static readonly BindableProperty PriceProperty = BindableProperty.Create(nameof(Price), typeof(float), typeof(AssetCard), default, propertyChanged: OnPriceChanged);
    public static readonly BindableProperty VariationProperty = BindableProperty.Create(nameof(Variation), typeof(float?), typeof(AssetCard), null, propertyChanged: OnVariationChanged);
    public static readonly BindableProperty PerformanceProperty = BindableProperty.Create(nameof(Performance), typeof(float?), typeof(AssetCard), null, propertyChanged: OnPerformanceChanged);

    public string Title {
        get { return (string)GetValue(TitleProperty); }
        set { SetValue(TitleProperty, value); }
    }

    public string Icon {
        get { return (string)GetValue(IconProperty); }
        set { SetValue(IconProperty, value); }
    }

    public Color TitleColor {
        get { return (Color)GetValue(TitleProperty); }
        set { SetValue(TitleProperty, value); }
    }

    public int AssetCount {
        get { return (int)GetValue(AssetCountProperty); }
        set { SetValue(AssetCountProperty, value); }
    }

    public float Price {
        get { return (float)GetValue(PriceProperty); }
        set { SetValue(PriceProperty, value); }
    }

    public float? Variation {
        get { return (float?)GetValue(VariationProperty); }
        set { SetValue(VariationProperty, value); }
    }

    public float? Performance {
        get { return (float?)GetValue(PerformanceProperty); }
        set { SetValue(PerformanceProperty, value); }
    }

    public AssetCard() {
        InitializeComponent();
        Application.Current.RequestedThemeChanged += Current_RequestedThemeChanged;
    }

    private static void OnTitleChanged(BindableObject bindable, object oldValue, object newValue) {
        if(bindable is not AssetCard control) { return; }
        control.title.Text = newValue.ToString();
    }

    private static void OnIconChanged(BindableObject bindable, object oldValue, object newValue) {
        if(bindable is not AssetCard control) { return; }
        control.icon.Text = newValue.ToString();
    }

    private static void OnTitleColorChanged(BindableObject bindable, object oldValue, object newValue) {
        if(bindable is not AssetCard control) { return; }
        if(newValue is not Color color) { return; }
        control.title.TextColor = color;
        control.icon.TextColor = color;
    }

    private static void OnAssetCountChanged(BindableObject bindable, object oldValue, object newValue) {
        if(bindable is not AssetCard control) { return; }
        control.assetCount.Text = newValue.ToString();
    }

    private static void OnPriceChanged(BindableObject bindable, object oldValue, object newValue) {
        if(bindable is not AssetCard control) { return; }
        if(newValue is not float price) { return; }
        control.price.Text = price.ToString("C", new CultureInfo("pt-BR"));
    }

    private static void OnVariationChanged(BindableObject bindable, object oldValue, object newValue) {
        if(bindable is not AssetCard control) { return; }
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
        if(bindable is not AssetCard control) { return; }
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
            performance.TextColor = Application.Current.RequestedTheme == AppTheme.Light ? StaticResourceUtility.Get<Color>("Black") : StaticResourceUtility.Get<Color>("Gray300");
        }
    }
}