namespace Exercise04;

public class SpanningTreeResult
{
    public Dictionary<string, NodeInfo> NodeInfos { get; } = new();
    public List<(string From, string To, double Weight)> TreeEdges { get; } = new();
}

public static class SpanningTreeAlgorithm
{
    public static SpanningTreeResult Run(Graph g, string start, IRelaxation relaxation)
    {
        var result = new SpanningTreeResult();

        // Init
        foreach (var node in g.Nodes.Keys)
            result.NodeInfos[node] = NodeInfo.Infinity;
        result.NodeInfos[start] = new NodeInfo(0.0, null);

        // OpenList als Priority Queue (MinHeap)
        var open = new PriorityQueue<string, double>();
        open.Enqueue(start, 0.0);

        var closed = new HashSet<string>();

        // Pseudocode-Struktur klar ersichtlich:
        while (open.Count > 0)
        {
            // Knoten mit kleinster Distanz wählen
            var u = open.Dequeue();
            if (!closed.Add(u)) continue;

            // Alle Nachbarn relaxieren
            foreach (var edge in g.Edges.Where(e => e.From == u || e.To == u))
            {
                var v = edge.From == u ? edge.To : edge.From;
                if (closed.Contains(v)) continue;

                var currentU = result.NodeInfos[u];
                var currentV = result.NodeInfos[v];

                if (relaxation.ShouldRelax(currentU, currentV, edge.Weight))
                {
                    result.NodeInfos[v] = relaxation.Relax(currentU, u, edge.Weight);
                    open.Enqueue(v, result.NodeInfos[v].Distance);
                }
            }
        }

        // Spannbaum-Kanten rekonstruieren
        foreach (var (node, info) in result.NodeInfos)
            if (info.Predecessor != null)
                result.TreeEdges.Add((info.Predecessor, node, info.Distance));

        return result;
    }
}