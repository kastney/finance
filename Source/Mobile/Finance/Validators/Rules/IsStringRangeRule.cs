using Finance.Validators.Interfaces;

namespace Finance.Validators.Rules;

internal class IsStringRangeRule(int min, int max) : IValidationRule<string> {
    private readonly int min = min;
    private readonly int max = max;

    public string Message { get; set; }

    public bool Check(string value) {
        if(value is null) { return false; }
        return value.Length >= min && value.Length <= max;
    }
}