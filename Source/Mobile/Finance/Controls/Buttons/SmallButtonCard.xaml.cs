using System.Windows.Input;

namespace Finance.Controls.Buttons;

public partial class SmallButtonCard : ContentView {
    public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(SmallButtonCard), string.Empty, propertyChanged: OnTextChanged);
    public static readonly BindableProperty GlyphProperty = BindableProperty.Create(nameof(Glyph), typeof(string), typeof(SmallButtonCard), string.Empty, propertyChanged: OnGlyphChanged);
    public static readonly BindableProperty IsRunningProperty = BindableProperty.Create(nameof(IsRunning), typeof(bool), typeof(SmallButtonCard), false, propertyChanged: OnIsRunningChanged);
    public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(SmallButtonCard));

    public string Text {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public string Glyph {
        get => (string)GetValue(GlyphProperty);
        set => SetValue(GlyphProperty, value);
    }

    public bool IsRunning {
        get => (bool)GetValue(IsRunningProperty);
        set => SetValue(IsRunningProperty, value);
    }

    public ICommand Command {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    public SmallButtonCard() {
        InitializeComponent();
    }

    private static void OnTextChanged(BindableObject bindable, object oldValue, object newValue) {
        if(bindable is not SmallButtonCard control) { return; }
        control.text.Text = newValue.ToString();
    }

    private static void OnGlyphChanged(BindableObject bindable, object oldValue, object newValue) {
        if(bindable is not SmallButtonCard control) { return; }
        control.icon.Text = newValue.ToString();
    }

    private static void OnIsRunningChanged(BindableObject bindable, object oldValue, object newValue) {
        if(bindable is not SmallButtonCard control) { return; }
        control.icon.IsVisible = !(bool)newValue;
        control.indicator.IsVisible = (bool)newValue;
    }

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e) {
        if(!IsRunning && Command is not null && Command.CanExecute(null)) {
            Command.Execute(null);
        }
    }
}