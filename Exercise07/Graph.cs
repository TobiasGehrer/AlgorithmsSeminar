namespace Exercise07;

public record Edge(string From, string To, double Weight);

public class Graph
{
    public HashSet<string> Nodes { get; } = new();
    public List<Edge> Edges { get; } = new();

    public void AddEdge(string from, string to, double weight)
    {
        Nodes.Add(from);
        Nodes.Add(to);
        Edges.Add(new Edge(from, to, weight));
    }

    public void LoadFromFile(string path)
    {
        Nodes.Clear(); Edges.Clear();
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
                Nodes.Add(line.Split(' ')[0]); 
            }
            else if (section == "EDGES")
            {
                var p = line.Split(' ');
                if (p.Length >= 3 && double.TryParse(p[2], System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out var w))
                {
                    AddEdge(p[0], p[1], w);
                }
            }
        }
    }
}
