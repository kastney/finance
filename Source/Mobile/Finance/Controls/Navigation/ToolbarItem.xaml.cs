using System.Windows.Input;

namespace Finance.Controls.Navigation;

public partial class ToolbarItem : ContentView {
    public static readonly BindableProperty GlyphProperty = BindableProperty.Create(nameof(Glyph), typeof(string), typeof(ToolbarItem), string.Empty, propertyChanged: OnGlyphChanged);
    public static readonly BindableProperty FontFamilyProperty = BindableProperty.Create(nameof(FontFamily), typeof(string), typeof(ToolbarItem), "IconsRegular", propertyChanged: OnFontFamilyChanged);
    public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(ToolbarItem));

    public string Glyph {
        get { return (string)GetValue(GlyphProperty); }
        set { SetValue(GlyphProperty, value); }
    }

    public string FontFamily {
        get { return (string)GetValue(FontFamilyProperty); }
        set { SetValue(FontFamilyProperty, value); }
    }

    public ICommand Command {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    public event EventHandler Clicked;

    public ToolbarItem() {
        InitializeComponent();
    }

    private static void OnGlyphChanged(BindableObject bindable, object oldValue, object newValue) {
        if(bindable is not ToolbarItem control) { return; }
        control.icon.Text = newValue.ToString();
    }

    private static void OnFontFamilyChanged(BindableObject bindable, object oldValue, object newValue) {
        if(bindable is not ToolbarItem control) { return; }
        control.icon.FontFamily = newValue.ToString();
    }

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e) {
        Clicked?.Invoke(this, e);
        if(Command is not null && Command.CanExecute(null)) {
            Command.Execute(null);
        }
    }
}