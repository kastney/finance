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
}