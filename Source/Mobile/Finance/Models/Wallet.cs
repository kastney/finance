using SQLite;

namespace Finance.Models;

[Table("wallets")]
internal class Wallet {

    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    [MaxLength(50), Unique]
    public string Name { get; set; }
}