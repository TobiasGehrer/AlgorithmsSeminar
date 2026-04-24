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
}