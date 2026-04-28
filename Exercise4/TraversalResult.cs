namespace Exercise04;

public class TraversalResult
{
    public List<string> VisitedNodes { get; } = new();
    public int ExpandedNodes { get; set; }
    public int MaxDepth { get; set; }
    public int MaxOpenListSize { get; set; }
    public string? FoundNode { get; set; }
    public bool Found => FoundNode != null;
}
