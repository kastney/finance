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
        walletService.AddOperation(
            new BrazilStockOperation {
                AppliedDate = new DateTime(2024, 5, 3, 16, 20, 0),
                Ticket = "BBAS3",
                Issuer = "BRASIL ON NM",
                Logo = "https://s3-symbol-logo.tradingview.com/banco-do-brasil--big.svg",
                Price = 28.33f,
                Count = 2,
                IsBuy = true
            },
            new BrazilStockOperation {
                AppliedDate = new DateTime(2024, 5, 3, 16, 21, 0),
                Ticket = "ITSA3",
                Issuer = "ITAUSA ON N1",
                Logo = "https://s3-symbol-logo.tradingview.com/itausa--big.svg",
                Price = 9.87f,
                Count = 5,
                IsBuy = true
            }
        );
        walletService.AddOperation(
            new CDBOperation {
                Issuer = "BANCO INTER S.A.",
                FixedType = FixedType.Postfixed,
                Price = 100,
                AppliedDate = new DateTime(2023, 10, 3, 10, 23, 0),
                DueDate = new DateTime(2025, 9, 23),
                IndexerType = IndexerType.CDI,
                Rate = 100,
                IsBuy = true
            },
            new CDBOperation {
                Issuer = "PICPAY BANK",
                FixedType = FixedType.Postfixed,
                Price = 1400,
                AppliedDate = new DateTime(2024, 5, 14, 13, 34, 0),
                DueDate = new DateTime(2027, 5, 14),
                IndexerType = IndexerType.CDI,
                Rate = 102,
                IsBuy = true
            }
        );

        MinDate = walletService.MinData();
        MaxDate = walletService.MaxData();
        DisplayDate = walletService.MaxData();
        SelectedDate = walletService.MaxData();
    }
}