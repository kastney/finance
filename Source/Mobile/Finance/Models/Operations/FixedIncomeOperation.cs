namespace Finance.Models;

internal sealed class FixedIncomeOperation : Operation {
    public DateTime DueDate { get; set; }
    public string Issuer { get; set; }
    public TitleType TitleType { get; set; }
    public FixedType FixedType { get; set; }
    public float Value { get; set; }
    public IndexerType IndexerType { get; set; }
    public float Rate { get; set; }
}