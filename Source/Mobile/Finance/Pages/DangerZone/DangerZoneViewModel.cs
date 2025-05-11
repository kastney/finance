namespace Finance.Pages.DangerZone;

internal partial class DangerZoneViewModel : ViewModel {

    [RelayCommand]
    private async Task DeleteWallet() {
        IsRunning = true;

        await navigationService.NavigateTo("delete");
        await Task.Delay(100);

        IsRunning = false;
    }
}