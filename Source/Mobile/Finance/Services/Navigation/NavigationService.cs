namespace Finance.Services;

internal class NavigationService : INavigationService {

    public async Task NavigateTo(string route, bool animate = true) {
        await Shell.Current.GoToAsync(route, animate);
    }

    public async Task NavigateTo<TEntity>(string route, TEntity entity, bool animate = true) {
        if(entity is null) {
            await NavigateTo(route, animate);
        } else {
            await Shell.Current.GoToAsync(route, animate, new ShellNavigationQueryParameters { { "Entity", entity } });
        }
    }

    public async Task NavigateToModal<TPage>(bool animate = true) where TPage : Page {
        if(Shell.Current.Navigation.ModalStack.Count == 0) {
            var page = Activator.CreateInstance<TPage>();
            var navigation = new NavigationPage(page);
            await Shell.Current.Navigation.PushModalAsync(navigation, animate);
        } else {
            var page = Activator.CreateInstance<TPage>();
            var navigation = Shell.Current.Navigation.ModalStack[0].Navigation;
            await navigation.PushAsync(page, animate);
        }
    }
}