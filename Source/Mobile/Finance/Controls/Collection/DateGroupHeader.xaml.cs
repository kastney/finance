using System.Globalization;

namespace Finance.Controls;

public partial class DateGroupHeader : ContentView {
    public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(DateGroupHeader), string.Empty, propertyChanged: OnTextChanged);

    public string Text {
        get { return (string)GetValue(TextProperty); }
        set { SetValue(TextProperty, value); }
    }

    public DateGroupHeader() {
        InitializeComponent();
    }

    private static void OnTextChanged(BindableObject bindable, object oldValue, object newValue) {
        if(bindable is not DateGroupHeader control) { return; }
        if(!DateTime.TryParse(newValue.ToString(), out DateTime date)) { return; }
        control.value.Text = date.ToString("D", new CultureInfo("pt-BR"));
    }
}