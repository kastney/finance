using Finance.Enumerations;

namespace Finance.Models;

internal class Operation {
    public DateTime ApplicationDate { get; set; }
    public DateTime? DueDate { get; set; }
    public AssetType Type { get; set; }
}