namespace Finance.Validators.Interfaces;

public interface IValidatable<T> {
    List<IValidationRule<T>> Validations { get; }
    T Value { get; set; }
    bool HasError { get; }
}