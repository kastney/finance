namespace Finance.Services;

internal interface INavigationService {

    Task NavigateTo(string route, bool animate = true);

    Task NavigateTo<TEntity>(string route, TEntity entity, bool animate = true);
}