namespace Finance.Validators.Interfaces;

public interface IValidationRule<T> {
    string Message { get; }

    bool Check(T value);
}