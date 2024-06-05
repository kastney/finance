namespace Finance.Models.Operations;

internal abstract class Operation {
    public Guid Id { get; set; }
    public Guid WalletId { get; set; }
    public DateTime AppliedDate { get; set; }
    public float Price { get; set; }
    public string Issuer { get; set; }
    public bool IsBuy { get; set; }
}