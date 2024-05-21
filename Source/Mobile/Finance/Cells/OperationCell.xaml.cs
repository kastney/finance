using Finance.Enumerations;
using Finance.Structures;
using System.Globalization;

namespace Finance.Cells;

public partial class OperationCell : ViewCell {
    public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(OperationCell), string.Empty, propertyChanged: OnTitleChanged);
    public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(OperationCell), string.Empty, propertyChanged: OnTextChanged);
    public static readonly BindableProperty AssetTypeProperty = BindableProperty.Create(nameof(AssetType), typeof(AssetType), typeof(OperationCell), AssetType.None, propertyChanged: OnAssetTypeChanged);
    public static readonly BindableProperty DetailProperty = BindableProperty.Create(nameof(Detail), typeof(DetailStruct), typeof(OperationCell), default(DetailStruct), propertyChanged: OnDetailChanged);
    public static readonly BindableProperty TimeProperty = BindableProperty.Create(nameof(Time), typeof(DateTime), typeof(OperationCell), default(DateTime), propertyChanged: OnTimeChanged);
    public static readonly BindableProperty IsBuyProperty = BindableProperty.Create(nameof(IsBuy), typeof(bool), typeof(OperationCell), true, propertyChanged: OnIsBuyChanged);

    public string Title {
        get { return (string)GetValue(TitleProperty); }
        set { SetValue(TitleProperty, value); }
    }

    public string Text {
        get { return (string)GetValue(TextProperty); }
        set { SetValue(TextProperty, value); }
    }

    public AssetType AssetType {
        get { return (AssetType)GetValue(AssetTypeProperty); }
        set { SetValue(AssetTypeProperty, value); }
    }

    public DetailStruct Detail {
        get { return (DetailStruct)GetValue(DetailProperty); }
        set { SetValue(DetailProperty, value); }
    }

    public DateTime Time {
        get { return (DateTime)GetValue(TimeProperty); }
        set { SetValue(TimeProperty, value); }
    }

    public bool IsBuy {
        get { return (bool)GetValue(IsBuyProperty); }
        set { SetValue(IsBuyProperty, value); }
    }

    public OperationCell() {
        InitializeComponent();
    }

    private static void OnTitleChanged(BindableObject bindable, object oldValue, object newValue) {
        if(bindable is not OperationCell control) { return; }
        if(newValue is string title) {
            control.ticketTitle.Text = title;
            control.title.Text = title;
        }
    }

    private static void OnTextChanged(BindableObject bindable, object oldValue, object newValue) {
        if(bindable is not OperationCell control) { return; }
        if(newValue is string text) {
            control.text.Text = text;
        }
    }

    private static void OnAssetTypeChanged(BindableObject bindable, object oldValue, object newValue) {
        if(bindable is not OperationCell control) { return; }
        if(newValue is AssetType assetType) {
            switch(assetType) {
                case AssetType.Stock: {
                    control.assetType.Text = "AÇÃO";
                    control.ticket.IsVisible = true;
                    control.title.IsVisible = false;
                    break;
                }
                case AssetType.FII: {
                    control.assetType.Text = "FII";
                    control.ticket.IsVisible = true;
                    control.title.IsVisible = false;
                    break;
                }
                case AssetType.BDR: {
                    control.assetType.Text = "BDR";
                    control.ticket.IsVisible = true;
                    control.title.IsVisible = false;
                    break;
                }
                case AssetType.CDB: {
                    control.assetType.Text = "CDB";
                    control.ticket.IsVisible = false;
                    control.title.IsVisible = true;
                    break;
                }
            }
        }
    }

    private static void OnDetailChanged(BindableObject bindable, object oldValue, object newValue) {
        if(bindable is not OperationCell control) { return; }
        if(newValue is DetailStruct detail) {
            if(control.AssetType == AssetType.CDB) {
                control.price.Text = detail.Price.ToString("C", new CultureInfo("pt-BR"));
                control.detail.Text = detail.IsPrefixed ? "Prefixado" : "Pós-fixado";
            } else if(control.AssetType == AssetType.Stock || control.AssetType == AssetType.BDR || control.AssetType == AssetType.FII) {
                control.price.Text = (detail.Count * detail.Price).ToString("C", new CultureInfo("pt-BR"));
                control.detail.Text = detail.Count == 1 ? $"{detail.Count} unidade por " : $"{detail.Count} unidades por ";
                control.detail.Text += detail.Price.ToString("C", new CultureInfo("pt-BR"));
            }
        }
    }

    private static void OnTimeChanged(BindableObject bindable, object oldValue, object newValue) {
        if(bindable is not OperationCell control) { return; }
        var time = (DateTime)newValue;
        control.hour.Text = time.ToShortTimeString();
    }

    private static void OnIsBuyChanged(BindableObject bindable, object oldValue, object newValue) {
        if(bindable is not OperationCell control) { return; }
        control.buy.Text = (bool)newValue ? "Compra" : "Venda";
    }
}