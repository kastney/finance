namespace Finance.Utilities;

/// <summary>
/// Utilitário para acessar recursos estáticos definidos na aplicação de forma segura e tipada.
/// </summary>
internal static class StaticResourceUtility {

    #region Methods

    /// <summary>
    /// Obtém um recurso estático definido na coleção de recursos da aplicação, convertendo-o para o tipo especificado.
    /// </summary>
    /// <typeparam name="T">Tipo esperado do recurso a ser retornado.</typeparam>
    /// <param name="resourceName">Chave do recurso dentro da coleção de recursos da aplicação.</param>
    /// <returns>O recurso convertido para o tipo especificado ou o valor padrão se não encontrado ou incompatível.</returns>
    public static T Get<T>(string resourceName) {
        try {
            // Tenta obter o recurso a partir da coleção global de recursos da aplicação.
            var success = Application.Current.Resources.TryGetValue(resourceName, out var outValue);
            // Se o recurso foi encontrado e é do tipo esperado, retorna o valor convertido.
            // Caso contrário, retorna o valor padrão do tipo (null para referência, zero/default para valor).
            return success && outValue is T t ? t : default;
        } catch {
            // Em caso de erro (como Application.Current ser null), retorna o valor padrão.
            return default;
        }
    }

    #endregion Methods
}