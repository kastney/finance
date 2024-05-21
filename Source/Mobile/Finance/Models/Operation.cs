using Finance.Enumerations;
using Finance.Structures;

namespace Finance.Models;

internal class Operation {
    public string Title { get; set; }
    public string Text { get; set; }
    public AssetType AssetType { get; set; }
    public DetailStruct Detail { get; set; }
    public DateTime AppliedDate { get; set; }
    public bool IsBuy { get; set; }
    public string Icon { get; set; }
}