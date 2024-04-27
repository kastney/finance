using System.Windows.Input;

namespace Finance.Controls;

public partial class IndicatorButton : ContentView {
    public static readonly BindableProperty GlyphProperty = BindableProperty.Create(nameof(Glyph), typeof(string), typeof(IndicatorButton), string.Empty, propertyChanged: OnGlyphChanged);
    public static readonly BindableProperty IsRunningProperty = BindableProperty.Create(nameof(IsRunning), typeof(bool), typeof(IndicatorButton), false, propertyChanged: OnIsRunningChanged);
    public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(IndicatorButton), string.Empty, propertyChanged: OnTextChanged);
    public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(IndicatorButton));

    public string Glyph {
        get { return (string)GetValue(GlyphProperty); }
        set { SetValue(GlyphProperty, value); }
    }

    public bool IsRunning {
        get { return (bool)GetValue(IsRunningProperty); }
        set { SetValue(IsRunningProperty, value); }
    }

    public string Text {
        get { return (string)GetValue(TextProperty); }
        set { SetValue(TextProperty, value); }
    }

    public ICommand Command {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    public IndicatorButton() {
        InitializeComponent();
    }

    private static void OnGlyphChanged(BindableObject bindable, object oldValue, object newValue) {
        if(bindable is not IndicatorButton control) { return; }
        control.icon.Text = $"{newValue}   ";
    }

    private static void OnIsRunningChanged(BindableObject bindable, object oldValue, object newValue) {
        if(bindable is not IndicatorButton control) { return; }
        control.frame.IsVisible = (bool)newValue;
        control.button.IsVisible = !(bool)newValue;
    }

    private static void OnTextChanged(BindableObject bindable, object oldValue, object newValue) {
        if(bindable is not IndicatorButton control) { return; }
        control.text.Text = newValue.ToString();
    }

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e) {
        if(Command is not null && Command.CanExecute(null)) {
            Command.Execute(null);
        }
    }
}