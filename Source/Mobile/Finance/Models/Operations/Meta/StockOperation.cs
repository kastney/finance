namespace Finance.Models.Operations;

internal abstract class StockOperation : Operation {
    public string Ticket { get; set; }
    public int Count { get; set; }
}