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

    public void AddEdge(string from, string to, double weight = 1.0, object? value = null)
    {
        AddNode(from);
        AddNode(to);
        Edges.Add(new EdgeData(from, to, weight, value));
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
                    double weight = p.Length >= 3 && double.TryParse(p[2], System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out var w) ? w : 1.0;
                    object? val = p.Length >= 4 ? p[3] : null;
                    AddEdge(p[0], p[1], weight, val);
                }
            }
        }
    }
}
