namespace Exercise07;

public static class Kruskal
{
    public static List<Edge> FindMST(Graph g)
    {
        var mst = new List<Edge>();
        var ds = new DisjointSet();

        foreach (var node in g.Nodes)
        {
            ds.Add(node);
        }

        // Kanten nach Gewicht sortieren (PriorityQueue)
        var pq = new PriorityQueue<Edge, double>();
        
        foreach (var edge in g.Edges)
        {
            pq.Enqueue(edge, edge.Weight);
        }

        while (pq.Count > 0 && mst.Count < g.Nodes.Count - 1)
        {
            var edge = pq.Dequeue();

            if (!ds.Connected(edge.From, edge.To))
            {
                ds.Union(edge.From, edge.To);
                mst.Add(edge);
            }
        }

        return mst;
    }

    public static void PrintMST(List<Edge> mst)
    {
        Console.WriteLine("\n=== Kruskal MST ===");
        double total = 0;

        foreach (var e in mst)
        {
            Console.WriteLine($"  {e.From} -- {e.To}  (Gewicht: {e.Weight})");
            total += e.Weight;
        }

        Console.WriteLine($"Gesamtgewicht: {total}");
    }
}
