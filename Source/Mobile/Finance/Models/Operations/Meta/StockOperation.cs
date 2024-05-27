namespace Finance.Models.Operations;

internal abstract class StockOperation : Operation {
    public string Logo { get; set; }
    public int Count { get; set; }
}