using Finance.Validators.Delegates;
using Finance.Validators.Interfaces;

namespace Finance.Validators.Objects;

/// <summary>
/// Representa um objeto validável que encapsula um valor e suas regras de validação associadas.
/// </summary>
/// <typeparam name="T">Tipo do valor a ser validado.</typeparam>
/// <param name="propertyName">Nome da propriedade associada ao valor.</param>
/// <param name="valueChanged">Delegado chamado quando o valor for alterado.</param>
public class ValidatableObject<T>(string propertyName, ValueChangedHandler valueChanged) : IValidatable<T> {

    #region Fields

    /// <summary>
    /// Nome da propriedade associada ao valor.
    /// </summary>
    private readonly string propertyName = propertyName;

    /// <summary>
    /// Delegado acionado quando o valor for alterado.
    /// </summary>
    private readonly ValueChangedHandler valueChanged = valueChanged;

    /// <summary>
    /// Valor interno armazenado.
    /// </summary>
    private T value;

    #endregion Fields

    #region Properties

    /// <summary>
    /// Obtém ou define o valor atual. Ao definir, executa a validação automaticamente.
    /// </summary>
    public T Value {
        get => value;
        set {
            // Armazena o novo valor.
            this.value = value;
            // Valida o novo valor.
            Validate();
        }
    }

    /// <summary>
    /// Indica se a propriedade possui erro de validação.
    /// </summary>
    public bool HasError { get; set; }

    /// <summary>
    /// Texto da mensagem de erro atual, se houver.
    /// </summary>
    public string ErrorText { get; set; }

    /// <summary>
    /// Lista de regras de validação aplicáveis ao valor.
    /// </summary>
    public List<IValidationRule<T>> Validations { get; } = [];

    #endregion Properties

    #region Methods

    /// <summary>
    /// Executa a validação com base nas regras definidas.
    /// </summary>
    /// <returns><c>true</c> se o valor for válido; caso contrário, <c>false</c>.</returns>
    public virtual bool Validate() {
        // Verifica se há alguma regra de validação que falhe para o valor atual.
        if(Validations.FirstOrDefault(v => !v.Check(Value)) is IValidationRule<T> error) {
            // Define a mensagem de erro correspondente.
            ErrorText = error.Message;
            // Indica que há erro.
            HasError = true;
        } else {
            // Limpa a mensagem de erro.
            ErrorText = string.Empty;
            // Indica que não há erro.
            HasError = false;
        }
        // Notifica que o valor foi alterado.
        valueChanged?.Invoke(propertyName);

        // Retorna verdadeiro se não houver erro.
        return !HasError;
    }

    /// <summary>
    /// Adiciona um erro manualmente ao objeto validável.
    /// </summary>
    /// <param name="message">Mensagem de erro a ser atribuída.</param>
    public virtual void AddError(string message) {
        // Define a mensagem de erro.
        ErrorText = message;
        // Indica que há erro.
        HasError = true;
        // Notifica que o valor foi alterado.
        valueChanged?.Invoke(propertyName);
    }

    /// <summary>
    /// Redefine o valor, remove erros e notifica alterações.
    /// </summary>
    public virtual void Reset() {
        // Redefine o valor para o valor padrão do tipo.
        Value = default;
        // Indica que não há erro.
        HasError = false;
        // Notifica que o valor foi alterado.
        valueChanged?.Invoke(propertyName);
    }

    #endregion Methods
}