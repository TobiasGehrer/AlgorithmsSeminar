namespace Exercise04;

public class EdgeData(string from, string to, double weight = 1.0, object? value = null)
{
    public string From { get; } = from;
    public string To { get; } = to;
    public double Weight { get; set; } = weight;
    public object? Value { get; set; } = value;
}
