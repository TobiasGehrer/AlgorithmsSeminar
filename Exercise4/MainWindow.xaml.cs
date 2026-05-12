using Exercise04;
using Microsoft.Msagl.Drawing;
using Microsoft.Msagl.GraphViewerGdi;
using MsaglGraph = Microsoft.Msagl.Drawing.Graph;
using System.Windows;

namespace Exercise4;

public partial class MainWindow : Window
{
    private Exercise04.Graph _graph = new();
    private List<string>? _eulerPath;
    private readonly GViewer _viewer = new() { Dock = System.Windows.Forms.DockStyle.Fill };

    public MainWindow()
    {
        InitializeComponent();
        GraphHost.Child = _viewer;
    }

    private void LoadFile_Click(object sender, RoutedEventArgs e)
    {
        var dlg = new Microsoft.Win32.OpenFileDialog { Filter = "Graph Files (*.txt)|*.txt" };
        
        if (dlg.ShowDialog() != true) 
        { 
            return; 
        }

        _graph = new Exercise04.Graph();
        _graph.LoadFromFile(dlg.FileName);
        _eulerPath = null;
        RenderGraph();
        StatusText.Text = $"Geladen: {_graph.Nodes.Count} Knoten, {_graph.Edges.Count} Kanten";
    }

    private void CheckEuler_Click(object sender, RoutedEventArgs e)
    {
        StatusText.Text = EulerAlgorithm.Check(_graph) switch
        {
            EulerType.Circuit => "✔ Eulerscher Kreis vorhanden (alle Knoten geraden Grades)",
            EulerType.Path => "✔ Offener Eulerpfad vorhanden (genau 2 Knoten mit ungeradem Grad)",
            EulerType.None => "✘ Kein Eulerpfad vorhanden"
        };
    }

    private void ShowPath_Click(object sender, RoutedEventArgs e)
    {
        _eulerPath = EulerAlgorithm.FindPath(_graph);
        
        if (_eulerPath == null) 
        { 
            StatusText.Text = "Kein Eulerpfad gefunden."; return; 
        }

        RenderGraph(_eulerPath);
        StatusText.Text = "Eulerpfad: " + string.Join(" → ", _eulerPath);
    }

    private void RenderGraph(List<string>? path = null)
    {
        var mg = new MsaglGraph();

        var eulerEdges = new HashSet<(string, string)>();
        if (path != null)
            for (int i = 0; i < path.Count - 1; i++)
                eulerEdges.Add((path[i], path[i + 1]));

        foreach (var edge in _graph.Edges)
        {
            var e = mg.AddEdge(edge.From, edge.To);
            e.Attr.ArrowheadAtTarget = ArrowStyle.None;
            bool isEuler = eulerEdges.Contains((edge.From, edge.To))
                        || eulerEdges.Contains((edge.To, edge.From));
            e.Attr.Color = isEuler ? Microsoft.Msagl.Drawing.Color.Red : Microsoft.Msagl.Drawing.Color.Black;
            e.Attr.LineWidth = isEuler ? 3 : 1;
            if (edge.Value != null) e.LabelText = edge.Value.ToString();
        }

        if (path is { Count: > 0 })
        {
            mg.FindNode(path[0]).Attr.FillColor = Microsoft.Msagl.Drawing.Color.LightGreen;
            mg.FindNode(path[^1]).Attr.FillColor = Microsoft.Msagl.Drawing.Color.LightCoral;
        }

        _viewer.Graph = mg;
    }

    // Exercise05

    private void DfsRec_Click(object sender, RoutedEventArgs e)
    {
        RunTraversal(TraversalMode.DfsRecursive);
    }

    private void DfsIter_Click(object sender, RoutedEventArgs e)
    {
        RunTraversal(TraversalMode.DfsIterative);
    }

    private void Bfs_Click(object sender, RoutedEventArgs e)
    {
        RunTraversal(TraversalMode.Bfs);
    }

    private void RunTraversal(TraversalMode mode)
    {
        string? target = string.IsNullOrWhiteSpace(SearchInput.Text) ? null : SearchInput.Text.Trim();
        var result = GraphTraversal.Search(_graph, target, mode);

        StatusText.Text = result.Found
            ? $"✔ '{result.FoundNode}' gefunden | Reihenfolge: {string.Join(" → ", result.VisitedNodes)}"
            : target != null
                ? $"✘ '{target}' nicht gefunden | Traversiert: {string.Join(" → ", result.VisitedNodes)}"
                : $"Traversiert: {string.Join(" → ", result.VisitedNodes)}";

        MetricsText.Text = $"Expandiert: {result.ExpandedNodes} | " +
                           $"Max. Tiefe: {result.MaxDepth} | " +
                           $"Max. OpenList: {result.MaxOpenListSize}";
    }

    private void Dijkstra_Click(object sender, RoutedEventArgs e) =>
    RunSpanningTree(new DijkstraRelaxation(), "Dijkstra");

    private void Prim_Click(object sender, RoutedEventArgs e) =>
        RunSpanningTree(new PrimRelaxation(), "Prim");

    private void RunSpanningTree(IRelaxation relaxation, string label)
    {
        string start = string.IsNullOrWhiteSpace(StartNodeInput.Text)
            ? _graph.Nodes.Keys.FirstOrDefault() ?? ""
            : StartNodeInput.Text.Trim();

        if (!_graph.Nodes.ContainsKey(start))
        {
            StatusText.Text = $"Knoten '{start}' nicht gefunden.";
            return;
        }

        var result = SpanningTreeAlgorithm.Run(_graph, start, relaxation);
        RenderSpanningTree(result);

        var info = string.Join(", ", result.NodeInfos
            .Select(kv => $"{kv.Key}={kv.Value.Distance:0.#}"));
        StatusText.Text = $"{label} ab '{start}': {info}";
    }

    private void RenderSpanningTree(SpanningTreeResult result)
    {
        var mg = new MsaglGraph();
        var treeEdgeSet = result.TreeEdges
            .Select(e => (e.From, e.To))
            .ToHashSet();

        foreach (var edge in _graph.Edges)
        {
            var e = mg.AddEdge(edge.From, edge.To);
            e.Attr.ArrowheadAtTarget = ArrowStyle.None;
            bool isTree = treeEdgeSet.Contains((edge.From, edge.To))
                       || treeEdgeSet.Contains((edge.To, edge.From));
            e.Attr.Color = isTree ? Microsoft.Msagl.Drawing.Color.Blue : Microsoft.Msagl.Drawing.Color.LightGray;
            e.Attr.LineWidth = isTree ? 3 : 1;
            e.LabelText = edge.Weight.ToString("0.#");
        }

        foreach (var (node, info) in result.NodeInfos)
        {
            var n = mg.FindNode(node);
            if (n != null)
            {
                n.LabelText = $"{node}\n{info.Distance:0.#}";
                n.Attr.FillColor = info.Predecessor == null
                    ? Microsoft.Msagl.Drawing.Color.LightGreen
                    : Microsoft.Msagl.Drawing.Color.LightBlue;
            }
        }

        _viewer.Graph = mg;
    }
}