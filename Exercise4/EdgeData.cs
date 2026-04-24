namespace Exercise04;

public class EdgeData(string from, string to, object? value = null)
{
    public string From { get; } = from;
    public string To { get; } = to;
    public object? Value { get; set; } = value;
}
