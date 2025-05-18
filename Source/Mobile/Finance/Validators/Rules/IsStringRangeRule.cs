using Finance.Validators.Interfaces;

namespace Finance.Validators.Rules;

/// <summary>
/// Regra de validação que verifica se o comprimento de uma string está dentro de um intervalo especificado.
/// </summary>
/// <param name="min">Valor mínimo permitido para o comprimento da string.</param>
/// <param name="max">Valor máximo permitido para o comprimento da string.</param>
internal class IsStringRangeRule(int min, int max) : IValidationRule<string> {

    #region Fields

    /// <summary>
    /// Valor mínimo permitido para o comprimento da string.
    /// </summary>
    private readonly int min = min;

    /// <summary>
    /// Valor máximo permitido para o comprimento da string.
    /// </summary>
    private readonly int max = max;

    #endregion Fields

    #region Properties

    /// <summary>
    /// Mensagem de erro exibida quando a validação falha.
    /// </summary>
    public string Message { get; set; }

    #endregion Properties

    #region Methods

    /// <summary>
    /// Executa a verificação da regra para o valor informado.
    /// </summary>
    /// <param name="value">Valor da string a ser validado.</param>
    /// <returns><c>true</c> se o comprimento da string estiver dentro do intervalo permitido; caso contrário, <c>false</c>.</returns>
    public bool Check(string value) {
        // Retorna false se o valor for nulo.
        if(value is null) { return false; }
        // Verifica se o comprimento da string está entre os limites definidos.
        return value.Length >= min && value.Length <= max;
    }

    #endregion Methods
}