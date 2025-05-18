namespace Finance.Validators.Interfaces;

/// <summary>
/// Interface que define uma regra de validação para um determinado tipo de valor.
/// </summary>
/// <typeparam name="T">Tipo de valor ao qual a regra será aplicada.</typeparam>
public interface IValidationRule<T> {

    #region Properties

    /// <summary>
    /// Obtém a mensagem de erro associada à falha da validação.
    /// </summary>
    string Message { get; }

    #endregion Properties

    #region Methods

    /// <summary>
    /// Verifica se o valor fornecido atende à regra de validação.
    /// </summary>
    /// <param name="value">Valor a ser validado.</param>
    /// <returns><c>true</c> se o valor for válido; caso contrário, <c>false</c>.</returns>
    bool Check(T value);

    #endregion Methods
}