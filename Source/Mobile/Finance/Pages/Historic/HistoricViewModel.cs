using CommunityToolkit.Mvvm.ComponentModel;
using Finance.Enumerations;
using Finance.Models.Operations;

namespace Finance.Pages.Historic;

internal partial class HistoricViewModel : ViewModel {

    [ObservableProperty]
    private DateTime minDate;

    [ObservableProperty]
    private DateTime maxDate;

    [ObservableProperty]
    private DateTime displayDate;

    [ObservableProperty]
    private DateTime selectedDate;

    public HistoricViewModel() {

        #region BDR

        walletService.AddOperation(new BDROperation {
            AppliedDate = new DateTime(2024, 5, 20, 12, 45, 0),
            Ticket = "ROXO34",
            Issuer = "NU HOLDINGS DRN",
            Logo = "https://s3-symbol-logo.tradingview.com/nu-holdings--big.svg",
            Price = 9.84f,
            Count = 4,
            IsBuy = true
        });

        #endregion BDR

        #region Brazil Stock

        walletService.AddOperation(new BrazilStockOperation {
            AppliedDate = new DateTime(2024, 5, 3, 16, 20, 0),
            Ticket = "BBAS3",
            Issuer = "BRASIL ON NM",
            Logo = "https://s3-symbol-logo.tradingview.com/banco-do-brasil--big.svg",
            Price = 28.33f,
            Count = 2,
            IsBuy = true
        });

        walletService.AddOperation(new BrazilStockOperation {
            AppliedDate = new DateTime(2024, 5, 3, 16, 21, 0),
            Ticket = "ITSA3",
            Issuer = "ITAUSA ON N1",
            Logo = "https://s3-symbol-logo.tradingview.com/itausa--big.svg",
            Price = 9.87f,
            Count = 5,
            IsBuy = true
        });

        walletService.AddOperation(new BrazilStockOperation {
            AppliedDate = new DateTime(2024, 5, 13, 15, 38, 0),
            Ticket = "TAEE3",
            Issuer = "TAESA ON ED N2",
            Logo = "https://s3-symbol-logo.tradingview.com/taesa--big.svg",
            Price = 11.53f,
            Count = 5,
            IsBuy = true
        });

        walletService.AddOperation(new BrazilStockOperation {
            AppliedDate = new DateTime(2024, 5, 15, 16, 19, 0),
            Ticket = "PETR4",
            Issuer = "PETROBRAS PN EDR N2",
            Logo = "https://s3-symbol-logo.tradingview.com/brasileiro-petrobras--big.svg",
            Price = 38.45f,
            Count = 2,
            IsBuy = true
        });

        walletService.AddOperation(new BrazilStockOperation {
            AppliedDate = new DateTime(2024, 5, 17, 11, 43, 0),
            Ticket = "BBAS3",
            Issuer = "BRASIL ON NM",
            Logo = "https://s3-symbol-logo.tradingview.com/banco-do-brasil--big.svg",
            Price = 27.69f,
            Count = 2,
            IsBuy = true
        });

        walletService.AddOperation(new BrazilStockOperation {
            AppliedDate = new DateTime(2024, 5, 17, 16, 51, 0),
            Ticket = "SAPR4",
            Issuer = "SANEPAR PN N2",
            Logo = "https://s3-symbol-logo.tradingview.com/sanepar--big.svg",
            Price = 5.8f,
            Count = 4,
            IsBuy = true
        });

        walletService.AddOperation(new BrazilStockOperation {
            AppliedDate = new DateTime(2024, 5, 21, 13, 14, 0),
            Ticket = "PETR4",
            Issuer = "PETROBRAS PN EDR N2",
            Logo = "https://s3-symbol-logo.tradingview.com/brasileiro-petrobras--big.svg",
            Price = 37,
            Count = 2,
            IsBuy = true
        });

        walletService.AddOperation(new BrazilStockOperation {
            AppliedDate = new DateTime(2024, 5, 28, 11, 51, 0),
            Ticket = "SAPR4",
            Issuer = "SANEPAR PN N2",
            Logo = "https://s3-symbol-logo.tradingview.com/sanepar--big.svg",
            Price = 5.65f,
            Count = 6,
            IsBuy = true
        });

        #endregion Brazil Stock

        #region FII

        walletService.AddOperation(new FIIOperation {
            AppliedDate = new DateTime(2024, 4, 12, 14, 0, 0),
            Ticket = "BTCI11",
            Issuer = "FII BTG CRI CI ER",
            Count = 2,
            Price = 10.24f,
            IsBuy = true
        });

        walletService.AddOperation(new FIIOperation {
            AppliedDate = new DateTime(2024, 4, 12, 14, 5, 0),
            Ticket = "VGHF11",
            Issuer = "FII VALOR HECI ER",
            Count = 2,
            Price = 9.18f,
            IsBuy = true
        });

        walletService.AddOperation(new FIIOperation {
            AppliedDate = new DateTime(2024, 4, 12, 14, 6, 0),
            Ticket = "XPCA11",
            Issuer = "FIAGRO XP CACI ER",
            Count = 2,
            Price = 8.62f,
            IsBuy = true
        });

        walletService.AddOperation(new FIIOperation {
            AppliedDate = new DateTime(2024, 4, 12, 14, 16, 0),
            Ticket = "VINO11",
            Issuer = "FII VINCI OFCI ER",
            Count = 2,
            Price = 8.16f,
            IsBuy = true
        });

        walletService.AddOperation(new FIIOperation {
            AppliedDate = new DateTime(2024, 4, 12, 14, 24, 0),
            Ticket = "GARE11",
            Issuer = "FII GUARDIANCI ER",
            Count = 2,
            Price = 9.13f,
            IsBuy = true
        });

        walletService.AddOperation(new FIIOperation {
            AppliedDate = new DateTime(2024, 5, 3, 16, 24, 0),
            Ticket = "GARE11",
            Issuer = "FII GUARDIANCI ER",
            Count = 4,
            Price = 9.07f,
            IsBuy = true
        });

        walletService.AddOperation(new FIIOperation {
            AppliedDate = new DateTime(2024, 5, 3, 16, 25, 0),
            Ticket = "XPCA11",
            Issuer = "FIAGRO XP CACI ER",
            Count = 4,
            Price = 8.57f,
            IsBuy = true
        });

        walletService.AddOperation(new FIIOperation {
            AppliedDate = new DateTime(2024, 5, 3, 16, 26, 0),
            Ticket = "BTCI11",
            Issuer = "FII BTG CRI CI ER",
            Count = 3,
            Price = 10.21f,
            IsBuy = true
        });

        #endregion FII

        #region CDB

        walletService.AddOperation(new CDBOperation {
            Issuer = "BANCO INTER S.A.",
            FixedType = FixedType.Postfixed,
            Price = 100,
            AppliedDate = new DateTime(2023, 10, 3, 10, 23, 0),
            DueDate = new DateTime(2025, 9, 23),
            IndexerType = IndexerType.CDI,
            Rate = 100,
            IsBuy = true
        });

        walletService.AddOperation(new CDBOperation {
            Issuer = "PICPAY BANK",
            FixedType = FixedType.Postfixed,
            Price = 1400,
            AppliedDate = new DateTime(2024, 5, 14, 13, 34, 0),
            DueDate = new DateTime(2027, 5, 14),
            IndexerType = IndexerType.CDI,
            Rate = 102,
            IsBuy = true
        });

        #endregion CDB

        MinDate = walletService.MinData();
        MaxDate = walletService.MaxData();
        DisplayDate = MaxDate;
        SelectedDate = MaxDate;
    }
}