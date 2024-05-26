using Finance.Enumerations;

namespace Finance.Models.Operations;

internal sealed class CDBOperation : FixedIncomeOperation {
    public string Issuer { get; set; }
    public FixedType FixedType { get; set; }
    public float Price { get; set; }
    public DateTime AppliedDate { get; set; }
    public DateTime DueDate { get; set; }
    public IndexerType IndexerType { get; set; }
    public float Rate { get; set; }
    public bool isBuy { get; set; }
}