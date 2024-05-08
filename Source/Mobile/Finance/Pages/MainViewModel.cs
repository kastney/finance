﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Finance.Models;

namespace Finance.Pages;

internal partial class MainViewModel : ViewModel {

    [ObservableProperty]
    private Wallet wallet;

    internal void Initialization() {
        Wallet = walletService.Wallet;
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
    private async Task Extract() {
        IsRunning = true;

        await navigationService.NavigateTo("extract");
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