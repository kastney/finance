namespace Finance.Services;

internal interface INavigationService {

    Task<Page> NavigateTo(string route, bool animate = true);

    Task NavigateToBack(bool animate = true);
}