namespace Finance.Services.Navigation;

/// <summary>
/// Define os métodos de navegação utilizados no aplicativo,
/// permitindo a transição entre páginas de forma assíncrona.
/// </summary>
internal interface INavigationService {

    #region Methods

    /// <summary>
    /// Navega para a rota especificada.
    /// </summary>
    /// <param name="route">A rota para a qual navegar.</param>
    /// <param name="animate">Define se a navegação deve ser animada. O padrão é <c>true</c>.</param>
    /// <returns>A página de destino, encapsulada em uma <see cref="Task{Page}"/>.</returns>
    Task<Page> NavigateTo(string route, bool animate = true);

    /// <summary>
    /// Retorna para a página anterior na pilha de navegação.
    /// </summary>
    /// <param name="animate">Define se a navegação de retorno deve ser animada. O padrão é <c>true</c>.</param>
    Task NavigateToBack(bool animate = true);

    #endregion Methods
}