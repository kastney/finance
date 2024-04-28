using Finance.Validators.Delegates;
using Finance.Validators.Interfaces;

namespace Finance.Validators.Objects;

public class ValidatableObject<T>(string propertyName, ValueChangedHandler valueChanged) : IValidatable<T> {
    private readonly string propertyName = propertyName;
    private readonly ValueChangedHandler valueChanged = valueChanged;

    private T value;

    public T Value {
        get => value;
        set {
            this.value = value;
            Validate();
        }
    }

    public bool HasError { get; set; }
    public string ErrorText { get; set; }
    public List<IValidationRule<T>> Validations { get; } = [];

    public virtual bool Validate() {
        if(Validations.FirstOrDefault(v => !v.Check(Value)) is IValidationRule<T> error) {
            ErrorText = error.Message;
            HasError = true;
        } else {
            ErrorText = string.Empty;
            HasError = false;
        }
        valueChanged?.Invoke(propertyName);

        return !HasError;
    }

    public virtual void AddError(string message) {
        ErrorText = message;
        HasError = true;
        valueChanged?.Invoke(propertyName);
    }

    public virtual void Reset() {
        Value = default;
        HasError = false;
        valueChanged?.Invoke(propertyName);
    }
}