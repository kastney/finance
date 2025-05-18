namespace Finance.Utilities;

/// <summary>
/// Classe utilitária para acesso centralizado aos serviços registrados via injeção de dependência.
/// </summary>
internal static class Service {

    #region Properties

    /// <summary>
    /// Obtém o provedor de serviços atual utilizado para resolver dependências.
    /// </summary>
    public static IServiceProvider Current {
        get {
#if ANDROID
            // Em plataformas Android, obtém os serviços a partir da aplicação MAUI atual.
            return IPlatformApplication.Current.Services;
#else
             // Para outras plataformas, ainda não implementado (retorna nulo).
            return null;
#endif
        }
    }

    #endregion Properties

    #region Methods

    /// <summary>
    /// Obtém uma instância do serviço solicitado, resolvendo-o a partir do provedor de serviços atual.
    /// </summary>
    /// <typeparam name="TService">Tipo do serviço a ser resolvido.</typeparam>
    /// <returns>Instância do serviço solicitado, ou <c>null</c> se não for encontrado.</returns>
    public static TService Get<TService>() {
        // Resolve e retorna o serviço do tipo especificado a partir do provedor de serviços atual.
        return Current.GetService<TService>();
    }

    #endregion Methods
}