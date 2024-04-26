namespace Finance.ViewModels;

internal class LoadingViewModel {

    internal async void Initialization() {
        await Task.Delay(5000);
        await Shell.Current.GoToAsync("///main");
    }
}