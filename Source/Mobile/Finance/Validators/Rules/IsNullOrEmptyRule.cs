using Finance.Validators.Interfaces;

namespace Finance.Validators.Rules;

/// <summary>
/// Regra de validação que verifica se uma string está nula ou vazia.
/// </summary>
public class IsNullOrEmptyRule : IValidationRule<string> {

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
    /// <returns><c>true</c> se o valor for válido; caso contrário, <c>false</c>.</returns>
    public bool Check(string value) {
        // Retorna false se o valor for nulo.
        if(value is null) { return false; }
        // Retorna true se a string não for nula nem composta apenas por espaços em branco.
        return !string.IsNullOrWhiteSpace(value.ToString());
    }

    #endregion Methods
}