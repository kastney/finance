using Finance.Services.Navigation;

namespace Finance.Services;

internal class NavigationService : INavigationService {

    public async Task<Page> NavigateTo(string route, bool animate = true) {
        await Shell.Current.GoToAsync(route, animate);
        return Shell.Current.Navigation.NavigationStack.Count == 1 ? Shell.Current.CurrentPage : Shell.Current.Navigation.NavigationStack[Shell.Current.Navigation.NavigationStack.Count - 1];
    }

    public async Task<Page> NavigateTo<TEntity>(string route, TEntity entity, bool animate = true) {
        if(entity is null) {
            await NavigateTo(route, animate);
        } else {
            await Shell.Current.GoToAsync(route, animate, new ShellNavigationQueryParameters { { "Entity", entity } });
        }
        return Shell.Current.Navigation.NavigationStack.Count == 1 ? Shell.Current.CurrentPage : Shell.Current.Navigation.NavigationStack[Shell.Current.Navigation.NavigationStack.Count - 1];
    }

    public async Task<TPage> NavigateToModal<TPage>(bool animate = true) where TPage : Page {
        var loadingPage = new LoadingPage();
        TPage page;

        if(Shell.Current.Navigation.ModalStack.Count == 0) {
            var navigation = new NavigationPage(loadingPage);
            await Shell.Current.Navigation.PushModalAsync(navigation, animate);
            await Task.Delay(10);

            page = Activator.CreateInstance<TPage>();
            await navigation.PushAsync(page, false);
            Shell.Current.Navigation.ModalStack[0].Navigation.RemovePage(loadingPage);
        } else {
            var navigation = Shell.Current.Navigation.ModalStack[0].Navigation;
            await navigation.PushAsync(loadingPage, animate);
            await Task.Delay(10);

            page = Activator.CreateInstance<TPage>();
            await navigation.PushAsync(page, false);
            navigation.RemovePage(loadingPage);
        }

        return page;
    }

    public async Task NavigateToBackModal(bool animate = true) {
        if(Shell.Current.Navigation.ModalStack.Count == 1) {
            var modal = Shell.Current.Navigation.ModalStack[0];
            if(modal.Navigation.NavigationStack.Count != 1) {
                await modal.Navigation.PopAsync(animate);
            } else {
                await Shell.Current.Navigation.PopModalAsync(animate);
            }
        }
    }
}