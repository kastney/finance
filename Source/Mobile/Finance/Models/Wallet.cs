using Finance.Models.Operations;

namespace Finance.Models;

internal class Wallet {
    public Guid Id { get; set; }
    public string Name { get; set; }
    public SortedDictionary<int, SortedDictionary<int, SortedDictionary<int, SortedDictionary<DateTime, Operation>>>> Operations { get; set; }

    public Wallet() {
        Operations = [];
    }
}