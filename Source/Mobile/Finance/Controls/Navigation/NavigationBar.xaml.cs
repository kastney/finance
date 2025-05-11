namespace Finance.Controls.Navigation;

public partial class NavigationBar : ContentView {
    public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(NavigationBar), string.Empty, propertyChanged: OnTitleChanged);
    public static readonly BindableProperty ToolbarItemsProperty = BindableProperty.Create(nameof(ToolbarItems), typeof(VerticalStackLayout), typeof(NavigationBar), null, propertyChanged: OnToolbarItemsChanged);
    public static readonly BindableProperty IsBackButtonProperty = BindableProperty.Create(nameof(IsBackButton), typeof(bool), typeof(NavigationBar), true, propertyChanged: OnIsBackButtonChanged);
    public static readonly BindableProperty IsRootProperty = BindableProperty.Create(nameof(IsRoot), typeof(bool), typeof(NavigationBar), true, propertyChanged: OnIsRootChanged);

    public event EventHandler Clicked;

    public string Title {
        get { return (string)GetValue(TitleProperty); }
        set { SetValue(TitleProperty, value); }
    }

    public VerticalStackLayout ToolbarItems {
        get { return (VerticalStackLayout)GetValue(ToolbarItemsProperty); }
        set { SetValue(ToolbarItemsProperty, value); }
    }

    public bool IsBackButton {
        get { return (bool)GetValue(IsBackButtonProperty); }
        set { SetValue(IsBackButtonProperty, value); }
    }

    public bool IsRoot {
        get { return (bool)GetValue(IsRootProperty); }
        set { SetValue(IsRootProperty, value); }
    }

    public NavigationBar() {
        InitializeComponent();
    }

    private static void OnTitleChanged(BindableObject bindable, object oldValue, object newValue) {
        if(bindable is not NavigationBar control) { return; }
        control.title.Text = newValue.ToString();
    }

    private static void OnToolbarItemsChanged(BindableObject bindable, object oldValue, object newValue) {
        if(bindable is not NavigationBar control) { return; }
        if(newValue is VerticalStackLayout items) {
            control.toolBarItemsView.Children.Clear();
            foreach(var item in items.Children) {
                control.toolBarItemsView.Children.Add(item);
            }
        }
    }

    private static void OnIsBackButtonChanged(BindableObject bindable, object oldValue, object newValue) {
        if(bindable is not NavigationBar control) { return; }
        control.icon.IsVisible = (bool)newValue;
    }

    private static void OnIsRootChanged(BindableObject bindable, object oldValue, object newValue) {
        if(bindable is not NavigationBar control) { return; }
        if((bool)newValue) {
            control.glyph.Text = "\xf0c9";
        } else {
            control.glyph.Text = "\xf060";
        }
    }

    private void BackButton_Clicked(object sender, EventArgs e) {
        if(IsRoot) {
            Shell.Current.FlyoutIsPresented = true;
        } else {
            Clicked?.Invoke(sender, e);
        }
    }
}