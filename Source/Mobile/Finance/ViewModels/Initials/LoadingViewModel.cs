namespace Finance.ViewModels;

internal class LoadingViewModel {

    internal static async void Initialization() {
        await Task.Delay(1000);
        await Shell.Current.GoToAsync("///presentation");
    }
}