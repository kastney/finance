namespace Finance;

internal class PieData(string label, double value) {
    public string Label { get; } = label;
    public double Value { get; } = value;
}