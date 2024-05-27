using Finance.Enumerations;

namespace Finance.Models.Operations;

internal sealed class CDBOperation : FixedIncomeOperation {
    public FixedType FixedType { get; set; }
    public DateTime DueDate { get; set; }
    public IndexerType IndexerType { get; set; }
    public float Rate { get; set; }
}