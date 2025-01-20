using SQLite;

namespace Finance.Models;

[Table("wallets")]
internal class Wallet {

    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    [MaxLength(50), Unique]
    public string Name { get; set; }

    #region BrazilStocks

    [NotNull]
    public bool BrazilStocksEnabled { get; set; }

    [Ignore]
    public string BrazilStocksName { get; } = "Ações Brasil";

    [Ignore]
    public Color BrazilStocksColor { get; } = new(194, 98, 179);

    [Ignore]
    public int BrazilStocksCount { get; } = 0;

    [Ignore]
    public int BrazilStocksPrice { get; } = 0;

    [Ignore]
    public int BrazilStocksVariation { get; } = 0;

    [Ignore]
    public int BrazilStocksPerformance { get; } = 0;

    #endregion BrazilStocks

    #region FIIs

    [NotNull]
    public bool FIIsEnabled { get; set; }

    [Ignore]
    public string FIIsName { get; } = "FIIs";

    [Ignore]
    public Color FIIsColor { get; } = new(45, 152, 218);

    [Ignore]
    public int FIIsCount { get; } = 0;

    [Ignore]
    public float FIIsPrice { get; } = 0;

    [Ignore]
    public float FIIsVariation { get; } = 0;

    [Ignore]
    public float FIIsPerformance { get; } = 0;

    #endregion FIIs

    #region Initialization

    public Wallet() {
        FIIsEnabled = false;
        BrazilStocksEnabled = false;
    }

    #endregion Initialization

    #region Methods

    #region Graphics

    internal Color[] GetPalette() {
        var palette = new List<Color>();
        if(BrazilStocksCount != 0) { palette.Add(BrazilStocksColor); }
        if(FIIsCount != 0) { palette.Add(FIIsColor); }
        if(palette.Count == 0) { palette.Add(new Color(0, 0, 0, 50)); }
        return [.. palette];
    }

    internal List<PieData> GetWalletPosition() {
        var palette = new List<PieData>();
        if(BrazilStocksCount != 0) { palette.Add(new PieData(BrazilStocksName, 1)); }
        if(FIIsCount != 0) { palette.Add(new PieData(FIIsName, 1)); }
        if(palette.Count == 0) { palette.Add(new PieData("Vazio", 1)); }
        return palette;
    }

    #endregion Graphics

    #region Has Methods

    internal bool HasBrazilStock() => BrazilStocksEnabled || BrazilStocksCount != 0;

    internal bool HasFIIs() => FIIsEnabled || FIIsCount != 0;

    internal bool HasAsset() => FIIsCount != 0 || BrazilStocksCount != 0;

    #endregion Has Methods

    #endregion Methods
}