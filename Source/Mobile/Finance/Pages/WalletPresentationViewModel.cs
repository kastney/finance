﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Finance.Services;

namespace Finance.Pages;

internal partial class WalletPresentationViewModel : ObservableObject {
    private readonly INavigationService navigationService;

    [ObservableProperty]
    private bool isRunning;

    public WalletPresentationViewModel() {
        navigationService = Service.Get<INavigationService>();
    }

    [RelayCommand]
    private async Task NewWallet() {
        IsRunning = true;

        await navigationService.NavigateTo("create");
        await Task.Delay(500);

        IsRunning = false;
    }
}