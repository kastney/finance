using System.Globalization;

namespace Finance.Models;

internal class OperationGroup(DateTime date, List<Operation> operations) : List<Operation>(operations) {
    public DateTime Date { get; private set; } = date;
    public string Title { get; private set; } = date.ToString("D", new CultureInfo("pt-BR"));
}