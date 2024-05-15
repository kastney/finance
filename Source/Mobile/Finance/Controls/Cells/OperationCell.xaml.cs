using Finance.Controls.Cells;
using Finance.Models;

namespace Finance.Controls;

public partial class OperationCell : ContentView {
    public static readonly BindableProperty EntityProperty = BindableProperty.Create(nameof(Entity), typeof(Operation), typeof(OperationCell), default, propertyChanged: OnEntityChanged);

    internal Operation Entity {
        get { return (Operation)GetValue(EntityProperty); }
        set { SetValue(EntityProperty, value); }
    }

    public OperationCell() {
        InitializeComponent();
    }

    private static void OnEntityChanged(BindableObject bindable, object oldValue, object newValue) {
        if(bindable is not OperationCell control) { return; }
        if(control.Entity is FixedIncomeOperation entity) {
            control.Content = new FixedIncomeOperationCell { Entity = entity };
        }
    }
}