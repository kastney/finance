using Finance.Models;
using System.Globalization;

namespace Finance.Controls.Cells;

public partial class StockOperationCell : ContentView {

    internal StockOperation Stock {
        set {
            launch.Text = value.Count == 1 ? $"{value.Count} unidade por " : $"{value.Count} unidades por ";
            launch.Text += value.Price.ToString("C2", new CultureInfo("pt-BR"));
            price.Text = (value.Count * value.Price).ToString("C2", new CultureInfo("pt-BR"));
            ticket.Text = value.Ticket;
            isBuy.Text = value.IsBuy ? "Compra" : "Venda";
        }
    }

    public StockOperationCell() {
        InitializeComponent();
    }
}