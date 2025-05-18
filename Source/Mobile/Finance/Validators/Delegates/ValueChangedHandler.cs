namespace Finance.Validators.Delegates;

/// <summary>
/// Representa um delegado que é chamado quando o valor de uma propriedade é alterado.
/// </summary>
/// <param name="propertyName">Nome da propriedade cujo valor foi alterado.</param>
public delegate void ValueChangedHandler(string propertyName);