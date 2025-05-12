namespace Finance.Models;

/// <summary>
/// Representa uma carteira de investimentos armazenada no banco de dados local.
/// </summary>
[Table("wallets")]
internal class Wallet {

    /// <summary>
    /// Identificador único da carteira (chave primária autoincrementada).
    /// </summary>
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    /// <summary>
    /// Nome da carteira. Deve ser único e ter no máximo 50 caracteres.
    /// </summary>
    [MaxLength(50), Unique]
    public string Name { get; set; }
}