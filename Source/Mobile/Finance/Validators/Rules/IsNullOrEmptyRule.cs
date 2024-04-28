using Finance.Validators.Interfaces;

namespace Finance.Validators.Rules;

public class IsNullOrEmptyRule : IValidationRule<string> {
    public string Message { get; set; }

    public bool Check(string value) {
        if(value is null) { return false; }
        return !string.IsNullOrWhiteSpace(value.ToString());
    }
}