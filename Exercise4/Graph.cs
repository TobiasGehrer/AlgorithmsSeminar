using System.IO;

namespace Exercise04;

public class Graph
{
    public Dictionary<string, NodeData> Nodes { get; } = new();
    public List<EdgeData> Edges { get; } = new();

    private readonly Dictionary<string, List<string>> _adj = new();

    public void AddNode(string id, object? value = null)
    {
        if (!Nodes.ContainsKey(id))
        {
            Nodes[id] = new NodeData(id, value);
            _adj[id] = [];
        }
    }

    public void AddEdge(string from, string to, object? value = null)
    {
        AddNode(from);
        AddNode(to);
        Edges.Add(new EdgeData(from, to, value));
        _adj[from].Add(to);
        _adj[to].Add(from);
    }

    public List<string> GetNeighbors(string id) => _adj.GetValueOrDefault(id, []);
    public int GetDegree(string id) => _adj.GetValueOrDefault(id, []).Count();

    public void LoadFromFile(string path)
    {
        Nodes.Clear();
        Edges.Clear();
        _adj.Clear();

        string section = "";

        foreach (var raw in File.ReadAllLines(path))
        {
            var line = raw.Trim();

            if (string.IsNullOrEmpty(line) || line.StartsWith('#'))
            {
                continue;
            }

            if (line is "NODES" or "EDGES")
            {
                section = line;
                continue;
            }

            if (section == "NODES")
            {
                var p = line.Split(' ', 2);
                AddNode(p[0], p.Length > 1 ? p[1] : null);
            }
            else if (section == "EDGES")
            {
                var p = line.Split(' ', 3);

                if (p.Length >= 2)
                {
                    AddEdge(p[0], p[1], p.Length > 2 ? p[2] : null);
                }
            }
        }
    }
}
