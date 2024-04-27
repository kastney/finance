using CommunityToolkit.Mvvm.ComponentModel;
using Finance.Models;

namespace Finance.ViewModels;

[QueryProperty(nameof(Wallet), "Entity")]
internal partial class DashboardViewModel : ObservableObject {

    [ObservableProperty]
    private Wallet wallet;
}