namespace Finance.Validators.Interfaces;

/// <summary>
/// Interface que define a estrutura de um objeto validável com regras de validação.
/// </summary>
/// <typeparam name="T">Tipo de valor a ser validado.</typeparam>
public interface IValidatable<T> {

    #region Properties

    /// <summary>
    /// Obtém a lista de regras de validação aplicadas ao valor.
    /// </summary>
    List<IValidationRule<T>> Validations { get; }

    /// <summary>
    /// Obtém ou define o valor que será validado.
    /// </summary>
    T Value { get; set; }

    /// <summary>
    /// Indica se o valor atual possui erros de validação.
    /// </summary>
    bool HasError { get; }

    #endregion Properties
}