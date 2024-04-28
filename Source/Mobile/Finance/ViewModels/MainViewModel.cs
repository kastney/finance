using CommunityToolkit.Mvvm.ComponentModel;
using Finance.Models;

namespace Finance.ViewModels;

[QueryProperty(nameof(Wallet), "Entity")]
internal partial class MainViewModel : ObservableObject {

    [ObservableProperty]
    private Wallet wallet;
}