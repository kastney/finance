using SQLite;

namespace Finance.Models;

internal abstract class Asset {

    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public int WalletId { get; set; }
}