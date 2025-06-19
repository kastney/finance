using Finance.Utilities;

namespace Finance.Services.Navigation;

/// <summary>
/// Serviço responsável por gerenciar a navegação entre páginas no aplicativo,
/// incluindo a exibição de uma tela de carregamento durante as transições.
/// </summary>
internal class NavigationService : INavigationService {

    #region Methods

    /// <summary>
    /// Navega para uma rota específica utilizando o Shell, exibindo uma página de carregamento temporária durante a transição.
    /// </summary>
    /// <param name="route">Rota de destino no formato definido pelo Shell.</param>
    /// <param name="animate">Define se a navegação será animada.</param>
    /// <returns>A página atual após a conclusão da navegação.</returns>
    public async Task<Page> NavigateTo(string route, bool animate = true) {
        // Cria e exibe a página de carregamento.
        var loadingPage = new LoadingPage();
        // Adiciona a página de carregamento à pilha de navegação.
        await Shell.Current.Navigation.PushAsync(loadingPage, animate);
        // Pequeno atraso para garantir a renderização da página de carregamento.
        await Task.Delay(10);

        // Realiza a navegação para a rota desejada sem animação (já que a animação inicial foi aplicada ao LoadingPage)
        await Shell.Current.GoToAsync(route, false);
        // Remove a página de carregamento da pilha de navegação.
        Shell.Current.Navigation.RemovePage(loadingPage);
        // Pequeno atraso para garantir a estabilidade da pilha de navegação.
        await Task.Delay(10);

        // Obtém a página atual após a navegação, considerando se é a última da pilha ou não.
        var page = Shell.Current.Navigation.NavigationStack.Count <= 1 ? Shell.Current.CurrentPage : Shell.Current.Navigation.NavigationStack[Shell.Current.Navigation.NavigationStack.Count - 1];
        // Se a página atual possui um contexto de dados do tipo ViewModel, chama o método Update.
        if(page is not null && page.BindingContext is ViewModel viewModel) { viewModel.Update(); }

        // Retorna a página atualmente visível após a navegação.
        return page;
    }

    /// <summary>
    /// Navega para a página anterior na pilha de navegação.
    /// </summary>
    /// <param name="animate">Define se a navegação será animada.</param>
    public async Task NavigateToBack(bool animate = true) {
        // Realiza o pop da página atual.
        await Shell.Current.Navigation.PopAsync(animate);
    }

    #endregion Methods
}