using System.Globalization;

namespace Finance.Controls.Cards;

public partial class AssetCard : ContentView {

    #region Fields

    public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(AssetCard), string.Empty, propertyChanged: OnTitleChanged);
    public static readonly BindableProperty ColorProperty = BindableProperty.Create(nameof(Color), typeof(Color), typeof(AssetCard), default, propertyChanged: OnColorChanged);
    public static readonly BindableProperty AssetCountProperty = BindableProperty.Create(nameof(AssetCount), typeof(int), typeof(AssetCard), -1, propertyChanged: OnAssetCountChanged);
    public static readonly BindableProperty PriceProperty = BindableProperty.Create(nameof(Price), typeof(float?), typeof(AssetCard), null, propertyChanged: OnPriceChanged);
    public static readonly BindableProperty VariationProperty = BindableProperty.Create(nameof(Variation), typeof(float?), typeof(AssetCard), null, propertyChanged: OnVariationChanged);
    public static readonly BindableProperty PerformanceProperty = BindableProperty.Create(nameof(Performance), typeof(float?), typeof(AssetCard), null, propertyChanged: OnPerformanceChanged);
    public static readonly BindableProperty CultureProperty = BindableProperty.Create(nameof(Culture), typeof(string), typeof(AssetCard), default(string), propertyChanged: OnCultureChanged);
    public static readonly BindableProperty FlagProperty = BindableProperty.Create(nameof(Flag), typeof(string), typeof(AssetCard), default(string), propertyChanged: OnFlagChanged);

    #endregion Fields

    #region Properties

    public string Title {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public Color Color {
        get => (Color)GetValue(ColorProperty);
        set => SetValue(ColorProperty, value);
    }

    public int AssetCount {
        get => (int)GetValue(AssetCountProperty);
        set => SetValue(AssetCountProperty, value);
    }

    public float? Price {
        get => (float?)GetValue(PriceProperty);
        set => SetValue(PriceProperty, value);
    }

    public float? Variation {
        get => (float?)GetValue(VariationProperty);
        set => SetValue(VariationProperty, value);
    }

    public float? Performance {
        get => (float?)GetValue(PerformanceProperty);
        set => SetValue(PerformanceProperty, value);
    }

    public string Culture {
        get => (string)GetValue(CultureProperty);
        set => SetValue(CultureProperty, value);
    }

    private CultureInfo ParsedCulture => new(Culture ?? "en-us");

    public string Flag {
        get => (string)GetValue(FlagProperty);
        set => SetValue(FlagProperty, value);
    }

    #endregion Properties

    #region Constructor

    public AssetCard() {
        InitializeComponent();
        Application.Current.RequestedThemeChanged += Current_RequestedThemeChanged;
    }

    #endregion Constructor

    #region Property Methods

    private static void OnTitleChanged(BindableObject bindable, object oldValue, object newValue) {
        if(bindable is not AssetCard control) { return; }
        control.title.Text = newValue.ToString();
    }

    private static void OnColorChanged(BindableObject bindable, object oldValue, object newValue) {
        if(bindable is not AssetCard control || newValue is not Color color) { return; }
        control.color.Color = color;
    }

    private static void OnAssetCountChanged(BindableObject bindable, object oldValue, object newValue) {
        if(bindable is not AssetCard control) { return; }
        control.assetCount.Text = newValue.ToString();
    }

    private static void OnPriceChanged(BindableObject bindable, object oldValue, object newValue) {
        if(bindable is not AssetCard control || newValue is not float price) { return; }
        control.price.Text = price.ToString("C", control.ParsedCulture);
    }

    private static void OnVariationChanged(BindableObject bindable, object oldValue, object newValue) {
        if(bindable is not AssetCard control || newValue is not float variation) { return; }
        control.variation.Text = (variation / 100).ToString("P2");
        ApplyColor(control.variation, variation, "Green", "Red", "TextColor");
    }

    private static void OnPerformanceChanged(BindableObject bindable, object oldValue, object newValue) {
        if(bindable is not AssetCard control || newValue is not float performance) { return; }
        control.performance.Text = (performance / 100).ToString("P2");
        ApplyColor(control.performance, performance, "Green", "Red", "TextColor");
    }

    private static void OnCultureChanged(BindableObject bindable, object oldValue, object newValue) {
        if(bindable is not AssetCard control || control.Price is not float price) { return; }
        control.price.Text = price.ToString("C", control.ParsedCulture);
    }

    private static void OnFlagChanged(BindableObject bindable, object oldValue, object newValue) {
        if(bindable is not AssetCard control || newValue is not string cultureCode || string.IsNullOrWhiteSpace(cultureCode)) { return; }
        var normalizedCode = cultureCode.Replace('-', '_').ToLowerInvariant();
        var imagePath = $"{normalizedCode}.jpg";
        control.flag.Source = ImageSource.FromFile(imagePath);
    }

    #endregion Property Methods

    #region Help Methods

    private void Current_RequestedThemeChanged(object sender, AppThemeChangedEventArgs e) {
        if(Variation.HasValue) {
            ApplyColor(variation, Variation.Value, "Green", "Red", "TextColor");
        }

        if(Performance.HasValue) {
            ApplyColor(performance, Performance.Value, "Green", "Red", "TextColor");
        }
    }

    private static void ApplyColor(Label label, float value, string positiveColorKey, string negativeColorKey, string neutralColorKey) {
        if(value > 0) {
            label.TextColor = Application.Current.RequestedTheme == AppTheme.Light
                ? StaticResourceUtility.Get<Color>(positiveColorKey)
                : StaticResourceUtility.Get<Color>(positiveColorKey + "Dark");
        } else if(value < 0) {
            label.TextColor = Application.Current.RequestedTheme == AppTheme.Light
                ? StaticResourceUtility.Get<Color>(negativeColorKey)
                : StaticResourceUtility.Get<Color>(negativeColorKey + "Dark");
        } else {
            label.TextColor = Application.Current.RequestedTheme == AppTheme.Light
                ? StaticResourceUtility.Get<Color>(neutralColorKey)
                : StaticResourceUtility.Get<Color>(neutralColorKey + "Dark");
        }
    }

    #endregion Help Methods
}