namespace Exercise04;

public class NodeData(string id, object? value = null)
{
    public string Id { get; } = id;
    public object? Value { get; set; } = value;
}
