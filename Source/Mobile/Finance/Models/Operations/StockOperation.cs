namespace Finance.Models;

internal class StockOperation : Operation {
    public string Ticket { get; set; }
    public int Count { get; set; }
    public string Logo { get; set; }
}