namespace Exercise07;

public static class WolverineSearch
{
    public static List<T>? Bfs<T>(T start) where T : IGameState<T>
    {
        var queue = new Queue<List<T>>();
        var visited = new HashSet<int> { start.GetHashCode() };
        queue.Enqueue([start]);

        while (queue.Count > 0)
        {
            var path = queue.Dequeue();
            var current = path[^1];

            if (current.IsTerminationState()) return path;

            foreach (var neighbor in current.GetNeighbors())
            {
                if (visited.Add(neighbor.GetHashCode()))
                    queue.Enqueue([.. path, neighbor]);
            }
        }
        return null;
    }

    public static List<T>? Dfs<T>(T start) where T : IGameState<T>
    {
        var stack = new Stack<List<T>>();
        var visited = new HashSet<int> { start.GetHashCode() };
        stack.Push([start]);

        while (stack.Count > 0)
        {
            var path = stack.Pop();
            var current = path[^1];

            if (current.IsTerminationState()) return path;

            foreach (var neighbor in current.GetNeighbors())
            {
                if (visited.Add(neighbor.GetHashCode()))
                    stack.Push([.. path, neighbor]);
            }
        }
        return null;
    }

    public static void PrintSolution<T>(List<T>? path, string label) where T : IGameState<T>
    {
        Console.WriteLine($"\n=== {label} ===");
        if (path == null) { Console.WriteLine("Keine Lösung gefunden."); return; }
        for (int i = 0; i < path.Count; i++)
            Console.WriteLine($"Schritt {i}: {path[i]}");
    }
}
