using Finance.Services.Navigation;

namespace Finance.Services;

internal class NavigationService : INavigationService {

    public async Task<Page> NavigateTo(string route, bool animate = true) {
        var loadingPage = new LoadingPage();
        await Shell.Current.Navigation.PushAsync(loadingPage, animate);
        await Task.Delay(10);

        await Shell.Current.GoToAsync(route, false);
        Shell.Current.Navigation.RemovePage(loadingPage);
        await Task.Delay(10);

        return Shell.Current.Navigation.NavigationStack.Count == 1 ? Shell.Current.CurrentPage : Shell.Current.Navigation.NavigationStack[Shell.Current.Navigation.NavigationStack.Count - 1];
    }
}