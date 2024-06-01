using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Finance.Models;

namespace Finance.Pages;

internal partial class MainViewModel : ViewModel {

    [ObservableProperty]
    private Wallet wallet;

    [ObservableProperty]
    private List<PieData> securitiesByRisk;

    [ObservableProperty]
    private Color[] palette;

    public MainViewModel() {
        Palette = [];
    }

    internal void Initialization() {
        Wallet = walletService.Wallet;
        SecuritiesByRisk = new List<PieData>() {
           new PieData("Ações", 132826.00),
           new PieData("FIIs", 208816.0),
           new PieData("BDRs", 24700.00),
           new PieData("CDBs", 80114.00),
           new PieData("FIAGROS", 80114.00)
       };
        Palette = new Color[] {
            Color.FromHex("#b04972"),
            Color.FromHex("#9c5ba0"),
            Color.FromHex("#7145a8"),
            Color.FromHex("#1c7ed6"),
            Color.FromHex("#1db2f5")
        };
    }

    [RelayCommand]
    private async Task SelectWallet() {
        if(!IsRunning) {
            IsRunning = true;

            await navigationService.NavigateTo("select");
            await Task.Delay(500);

            IsRunning = false;
        }
    }

    [RelayCommand]
    private async Task Historic() {
        IsRunning = true;

        await navigationService.NavigateTo("historic");
        await Task.Delay(500);

        IsRunning = false;
    }

    [RelayCommand]
    private async Task DangerZone() {
        IsRunning = true;

        await navigationService.NavigateTo("dangerZone");
        await Task.Delay(500);

        IsRunning = false;
    }
}

public class PieData {
    public string Label { get; }
    public double Value { get; }

    public PieData(string label, double value) {
        Label = label;
        Value = value;
    }
}

internal static class PaletteLoader {

    public static Color[] LoadPalette(params string[] values) {
        Color[] colors = new Color[values.Length];
        for(int i = 0; i < values.Length; i++)
            colors[i] = Color.FromArgb(values[i]);
        return colors;
    }
}