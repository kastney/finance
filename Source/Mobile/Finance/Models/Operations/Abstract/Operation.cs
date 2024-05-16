namespace Finance.Models;

internal abstract class Operation {
    public string Issuer { get; set; }
    public DateTime AppliedDate { get; set; }
    public bool IsBuy { get; set; }
    public float Price { get; set; }
}