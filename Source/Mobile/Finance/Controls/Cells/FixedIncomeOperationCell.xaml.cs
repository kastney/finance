using Finance.Models;
using System.Globalization;

namespace Finance.Controls.Cells;

public partial class FixedIncomeOperationCell : ContentView {

    internal FixedIncomeOperation Entity {
        set {
            switch(value.TitleType) {
                case TitleType.CDB: {
                    icon.Text = "\xf19c";
                    titleType.Text = $"{value.TitleType}";
                    break;
                }
            }
            description.Text = $"({value.Rate}% do {value.IndexerType})";
            fixedType.Text = value.FixedType == FixedType.Postfixed ? "Pós-Fixado" : "Prefixado";
            price.Text = value.Value.ToString("C2", new CultureInfo("pt-BR"));
            isBuy.Text = value.IsBuy ? "Compra" : "Venda";
        }
    }

    public FixedIncomeOperationCell() {
        InitializeComponent();
    }
}