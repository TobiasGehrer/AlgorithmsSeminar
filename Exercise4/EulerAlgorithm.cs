using System.IO;

namespace Exercise04;

public class EulerAlgorithm
{
    public static EulerType Check(Graph g)
    {
        if (g.Nodes.Count == 0 || !IsConnected(g))
        {
            return EulerType.None;
        }

        int odd = g.Nodes.Keys.Count(n => g.GetDegree(n) % 2 != 0);

        switch (odd)
        {
            case 0:
                return EulerType.Circuit;
            case 2:
                return EulerType.Path;
            default:
                return EulerType.None;
        }           
    }

    public static List<string>? FindPath(Graph g)
    {
        var type = Check(g);

        if (type == EulerType.None)
        {
            return null;
        }

        string start = type == EulerType.Path ? g.Nodes.Keys.First(n => g.GetDegree(n) % 2 != 0) : g.Nodes.Keys.First();

        // Hierholzer's Algorithmus
        var adj = g.Nodes.Keys.ToDictionary(n => n, n => new List<string>(g.GetNeighbors(n)));
        var stack = new Stack<string>();
        var path = new List<string>();
        stack.Push(start);

        while (stack.Count > 0)
        {
            var v = stack.Peek();

            if (adj[v].Count > 0)
            {
                var u = adj[v][0];
                adj[v].Remove(u);
                adj[u].Remove(v);
                stack.Push(u);
            }
            else
            {
                path.Add(stack.Pop());
            }
        }

        path.Reverse();
        return path;
    }

    private static bool IsConnected(Graph g)
    {
        var start = g.Nodes.Keys.FirstOrDefault();

        if (start == null)
        {
            return true;
        }

        var visited = new HashSet<string> { start };
        var queue = new Queue<string>(new[] { start });

        while (queue.Count > 0)
        {
            foreach (var n in g.GetNeighbors(queue.Dequeue()))
            {
                if (visited.Add(n))
                {
                    queue.Enqueue(n);
                }
            }
        }

        return visited.Count == g.Nodes.Count;
    }
}
