namespace Exercise04;

public enum TraversalMode
{
    DfsRecursive,
    DfsIterative,
    Bfs
}

public static class GraphTraversal
{
    public static TraversalResult Search(Graph g, string? target, TraversalMode mode)
    {
        var result = new TraversalResult();
        var closed = new HashSet<string>();

        if (g.Nodes.Count == 0)
        {
            return result;
        }

        string start = g.Nodes.Keys.First();

        switch (mode)
        {
            case TraversalMode.DfsRecursive:
                DfsRecursive(g, start, target, closed, result, 0);
                break;
            case TraversalMode.DfsIterative:
                DfsIterative(g, start, target, closed, result);
                break;
            case TraversalMode.Bfs:
                Bfs(g, start, target, closed, result);
                break;
        }

        return result;
    }

    private static bool DfsRecursive(Graph g, string node, string? target, HashSet<string> closed, TraversalResult result, int depth)
    {
        if (!closed.Add(node))
        {
            return false;
        }

        result.VisitedNodes.Add(node);
        result.ExpandedNodes++;
        result.MaxDepth = Math.Max(result.MaxDepth, depth);

        if (target != null && node == target)
        {
            result.FoundNode = node;
            return true;
        }

        foreach (var neighbor in g.GetNeighbors(node))
        {
            if (DfsRecursive(g, neighbor, target, closed, result, depth + 1))
            {
                return true;
            }
        }

        return false;
    }

    private static void DfsIterative(Graph g, string start, string? target, HashSet<string> closed, TraversalResult result)
    {
        // Stack speichert (node, depth)
        var open = new Stack<(string node, int depth)>();
        open.Push((start, 0));

        while (open.Count > 0)
        {
            result.MaxOpenListSize = Math.Max(result.MaxOpenListSize, open.Count);
            var (node, depth) = open.Pop();

            if (!closed.Add(node))
            {
                continue;
            }

            result.VisitedNodes.Add(node);
            result.ExpandedNodes++;
            result.MaxDepth = Math.Max(result.MaxDepth, depth);

            if (target != null && node == target)
            {
                result.FoundNode = node;
                return;
            }

            foreach (var neighbor in g.GetNeighbors(node))
            {
                if (!closed.Contains(neighbor))
                {
                    open.Push((neighbor, depth + 1));
                }
            }
        }
    }

    private static void Bfs(Graph g, string start, string? target, HashSet<string> closed, TraversalResult result)
    {
        var open = new Queue<(string node, int depth)>();
        open.Enqueue((start, 0));
        closed.Add(start);

        while (open.Count > 0)
        {
            result.MaxOpenListSize = Math.Max(result.MaxOpenListSize, open.Count);
            var (node, depth) = open.Dequeue();

            result.VisitedNodes.Add(node);
            result.ExpandedNodes++;
            result.MaxDepth = Math.Max(result.MaxDepth, depth);

            if (target != null && node == target)
            {
                result.FoundNode = node;
                return;
            }

            foreach (var neighbor in g.GetNeighbors(node))
            {
                if (closed.Add(neighbor))
                {
                    open.Enqueue((neighbor, depth + 1));
                }
            }
        }
    }
}
