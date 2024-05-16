namespace Finance.Models;

internal abstract class Operation {
    public DateTime AppliedDate { get; set; }
    public bool IsBuy { get; set; }
}