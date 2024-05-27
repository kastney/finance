namespace Finance.Models.Operations;

internal abstract class Operation {
    public DateTime AppliedDate { get; set; }
    public float Price { get; set; }
    public string Issuer { get; set; }
    public bool IsBuy { get; set; }
}