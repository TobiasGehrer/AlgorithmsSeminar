namespace Exercise04;

public record NodeInfo(double Distance, string? Predecessor)
{
    public static readonly NodeInfo Infinity = new(double.PositiveInfinity, null);
}

public interface IRelaxation
{
    bool ShouldRelax(NodeInfo current, NodeInfo neighbor, double edgeWeight);
    NodeInfo Relax(NodeInfo current, string predecessor, double edgeWeight);
}

public class DijkstraRelaxation : IRelaxation
{
    public bool ShouldRelax(NodeInfo current, NodeInfo neighbor, double edgeWeight)
        => current.Distance + edgeWeight < neighbor.Distance;

    public NodeInfo Relax(NodeInfo current, string predecessor, double edgeWeight)
        => new(current.Distance + edgeWeight, predecessor);
}

public class PrimRelaxation : IRelaxation
{
    public bool ShouldRelax(NodeInfo current, NodeInfo neighbor, double edgeWeight)
        => edgeWeight < neighbor.Distance;

    public NodeInfo Relax(NodeInfo current, string predecessor, double edgeWeight)
        => new(edgeWeight, predecessor);
}