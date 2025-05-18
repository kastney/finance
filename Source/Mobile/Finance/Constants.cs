namespace Finance;

/// <summary>
/// Classe contendo constantes globais utilizadas na aplicação,
/// especialmente relacionadas à configuração e localização do banco de dados.
/// </summary>
internal static class Constants {

    #region Properties

    /// <summary>
    /// Nome padrão do arquivo de banco de dados SQLite utilizado pela aplicação.
    /// </summary>
    public const string DatabaseFilename = "Finance.db3";

    /// <summary>
    /// Conjunto de flags utilizadas na abertura do banco de dados SQLite.
    /// Permite leitura, escrita, criação e uso de cache compartilhado.
    /// </summary>
    public const SQLiteOpenFlags Flags = SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.SharedCache;

    /// <summary>
    /// Caminho completo do banco de dados dentro do diretório de dados da aplicação.
    /// </summary>
    public static string DatabasePath => Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename);

    #endregion Properties
}